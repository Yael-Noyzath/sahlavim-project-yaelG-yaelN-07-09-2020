using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.ServiceModel.Channels;
using SachlavimService.Utilities;
using NReco.PdfGenerator;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Text;



namespace SachlavimService.Entities
{
    [DataContract]
    public class Settings
    {
        #region Members
        [DataMember]
        public int? iSettingId { get; set; }
        [DataMember]
        public string nvSettingName { get; set; }
        [DataMember]
        public string nvSettingCode { get; set; }
        [DataMember]
        public int? iSettingType { get; set; }
        [DataMember]
        public int? iNeighborhoodType { get; set; }
        [DataMember]
        public string nvAddress { get; set; }
        [DataMember]
        [NoSendToSQL]
        [NoGetFromSQL]
        public string nvShortAddress { get; set; }
        [DataMember]
        [NoSendToSQL]
        [NoGetFromSQL]
        public string nvStreet { get; set; }
        [DataMember]
        public string nvLat { get; set; }
        [DataMember]
        public string nvLng { get; set; }
        [DataMember]
        [NoSendToSQL]
        public int? iClusterId { get; set; }
        [DataMember]
        public string nvOperatingLocation { get; set; }
        [DataMember]
        public string nvPhone { get; set; }
        [DataMember]
        public string nvContactPerson { get; set; }
        [DataMember]
        public string nvContactPersonMail { get; set; }
        [DataMember]
        public string nvContactPersonPhone { get; set; }
        [DataMember]
        public bool bSettingMorning { get; set; }
        [DataMember]
        public bool bSettingNoon { get; set; }
        //[DataMember]
        //public bool bAfternoonProgram { get; set; }
        [DataMember]
        public string iCoordinatorId { get; set; }
        [DataMember]
        public Boolean bActiveAfternoon { get; set; }
        [DataMember]
        [NoSendToSQL]
        public Coordinator oCoordinator { get; set; }
        [DataMember]
        [NoSendToSQL]
        public List<int> lSettingAgegroups { get; set; }
        [DataMember]
        [NoSendToSQL]
        public List<Schedule> lSchedule { get; set; }

        #endregion Members

        #region Methods

        public static List<Settings> SettingsGet()
        {
            try
            {
                List<Settings> lSettings = new List<Settings>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSettings_Slct");
                if (ds.Tables.Count > 0)
                    lSettings = ObjectGenerator<Settings>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                foreach (Settings setting in lSettings)
                {
                    setting.lSettingAgegroups = new List<int>();
                }
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Settings setting = lSettings.Where(s => s.iSettingId == Convert.ToInt16(dr["iSettingId"].ToString())).FirstOrDefault();
                    setting.lSettingAgegroups.Add(Convert.ToInt16(dr["iAgegroupType"].ToString()));
                }

                return lSettings;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SettingsGet", ex);
                return null;
            }
        }

        public static List<Settings> GetSettings(int iSettingType)
        {
            try
            {
                List<Settings> lSettings = new List<Settings>();
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iSettingType", iSettingType));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSettingsByType_Slct", lParams);
                if (ds.Tables.Count > 0)
                    lSettings = ObjectGenerator<Settings>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);

                return lSettings;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SettingsGet", ex);
                return null;
            }
        }

        public static int? GetSettingByPhone(string nvPhone)
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSettingByPhone_Slct", new SqlParameter("nvPhone", nvPhone));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
                else
                    return -1;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetSettingByPhone", ex);
                return 0;
            }
        }
        [NoSendToSQL]
        public static int countmin { get; set; }

        public static List<Settings> DistanceSettings(List<Settings> lSettings)
        {
            try
            {
                if (lSettings.Count <= 1)
                    return lSettings;

                List<Settings> lAddress = new List<Settings>();

                foreach (Settings setting in lSettings)
                {
                    var index = setting.nvAddress.IndexOf(',');
                    if (index == -1) index = setting.nvAddress.Length;
                    setting.nvShortAddress = setting.nvAddress.Substring(0, index);
                    setting.nvStreet = setting.nvShortAddress.Trim(new Char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

                    if (lAddress.Where(s => s.nvShortAddress == setting.nvShortAddress).Count() == 0)
                    {
                        lAddress.Add(new Settings() { nvAddress = setting.nvAddress, nvShortAddress = setting.nvShortAddress, nvStreet = setting.nvStreet, nvLat = setting.nvLat, nvLng = setting.nvLng });
                    }
                }
                lAddress.Sort((f, s) => f.nvAddress.CompareTo(s.nvAddress));

                Settings minSetting = new Settings();

                List<Settings> lAddressDistance = new List<Settings>();
                lAddressDistance.Add(lAddress[0]);
                lAddress.RemoveAt(0);

                while (lAddress.Count > 0)
                {
                    List<Settings> lSettingStreet = lAddress.Where(s => s.nvStreet == lAddressDistance.Last().nvStreet).ToList();
                    lAddress.RemoveRange(0, lSettingStreet.Count);
                    while (lSettingStreet.Count > 0)
                    {
                        if (lSettingStreet.Count == 1)
                            minSetting = lSettingStreet[0];
                        else
                            minSetting = minDistance(lAddressDistance.Last(), lSettingStreet);
                        // כל הרשימה אלו כתובות ללא נקודות אורך ורוחב
                        if (minSetting.iSettingId == -1)
                        {
                            lAddressDistance.AddRange(lSettingStreet);
                            lSettingStreet = new List<Settings>();
                        }
                        // כתובת הבאה - הכי קרובה
                        else if (minSetting != null && minSetting.nvAddress != null)
                        {
                            lAddressDistance.Add(minSetting);
                            lSettingStreet.Remove(minSetting);
                        }
                    }

                    if (lAddress.Count > 0)
                    {
                        if (lAddress.Count == 1)
                            minSetting = lAddress[0];
                        else
                            minSetting = minDistance(lAddressDistance.Last(), lAddress);
                        // כל הרשימה אלו כתובות ללא נקודות אורך ורוחב
                        if (minSetting.iSettingId == -1)
                        {
                            lAddressDistance.AddRange(lAddress);
                            lAddress = new List<Settings>();
                        }
                        // כתובת הבאה - הכי קרובה
                        else if (minSetting != null && minSetting.nvAddress != null)
                        {
                            lAddressDistance.Add(minSetting);
                            lAddress.Remove(minSetting);
                        }
                    }
                }
                List<Settings> lSettingsDistance = new List<Settings>();
                foreach (Settings setting in lAddressDistance)
                {
                    lSettingsDistance.AddRange(lSettings.Where(s => s.nvAddress == setting.nvAddress));
                }






                return lSettingsDistance;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("DistanceSettings", ex);
                return null;
            }

        }

        public static Settings minDistance(Settings source, List<Settings> lSetting)
        {
            try
            {
                double minDistance = 1000000;
                Settings minSetting = new Settings();
                int countDistance = 0;
                foreach (Settings setting in lSetting)
                {
                    if (setting.nvLat != null && setting.nvLng != null)
                    {
                        countDistance++;
                        var Distance = DistanceMatrix.GetDistanceMatrix(0,
                            source.nvLat + "," + source.nvLng,
                            setting.nvLat.ToString() + "," + setting.nvLng.ToString());
                        if (Distance.rows.Length > 0 && Distance.rows[0].elements[0].distance.value < minDistance)
                        {
                            minDistance = Distance.rows[0].elements[0].distance.value;
                            minSetting = setting;
                        }
                    }
                }
                if (countDistance == 0)
                    return new Settings() { iSettingId = -1 };
                else if (minSetting.nvAddress == null)
                    minSetting = lSetting[0];
                return minSetting;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("minDistance", ex);
                return null;
            }
        }

        public static List<Settings> DistanceSettingsOLD(List<Settings> lSettings)
        {
            try
            {
                if (lSettings.Count <= 1)
                    return lSettings;
                foreach (Settings setting in lSettings)
                {
                    var index = setting.nvAddress.IndexOf(',');
                    if (index == -1) index = setting.nvAddress.Length;
                    setting.nvShortAddress = setting.nvAddress.Substring(0, index);
                    setting.nvStreet = setting.nvShortAddress.Trim(new Char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                }

                List<Settings> lSettingDistance = new List<Settings>();

                Settings minSetting = lSettings[0];
                lSettingDistance.Add(minSetting);
                lSettings.Remove(minSetting);
                while (lSettings.Count > 0)
                {
                    List<Settings> lSettingStreet = lSettings.Where(s => s.nvStreet == lSettingDistance.Last().nvStreet).ToList();
                    while (lSettingStreet.Count > 0)
                    {
                        // מסגרות שבאותו כתובת
                        List<Settings> lSettingAddress = lSettings.Where(s => s.nvShortAddress == lSettingDistance.Last().nvShortAddress).ToList();
                        if (lSettingAddress.Count > 0)
                        {
                            lSettingDistance.AddRange(lSettingAddress);
                            lSettings.Remove(lSettings.Single(s => s.nvShortAddress == lSettingDistance.Last().nvShortAddress));
                            lSettingStreet.Remove(lSettingStreet.Single(s => s.nvShortAddress == lSettingDistance.Last().nvShortAddress));
                        }
                        if (lSettingStreet.Count > 0)
                        {
                            minSetting = minDistance(lSettingDistance.Last(), lSettingStreet);
                            // כל הרשימה אלו כתובות ללא נקודות אורך ורוחב
                            if (minSetting.iSettingId == -1)
                            {
                                lSettingDistance.AddRange(lSettingStreet);
                                lSettings.Remove(lSettings.Single(s => s.nvStreet == lSettingDistance.Last().nvStreet));
                                lSettingStreet.Remove(lSettingStreet.Single(s => s.nvStreet == lSettingDistance.Last().nvStreet));
                            }
                            // כתובת הבאה - הכי קרובה
                            else
                            {
                                lSettingDistance.Add(minSetting);
                                lSettings.Remove(minSetting);
                                lSettingStreet.Remove(minSetting);
                            }
                        }
                    }
                    if (lSettings.Count > 0)
                    {
                        minSetting = minDistance(lSettingDistance.Last(), lSettings);
                        // כל הרשימה אלו כתובות ללא נקודות אורך ורוחב
                        if (minSetting.iSettingId == -1)
                        {
                            lSettingDistance.AddRange(lSettings);
                            lSettings = new List<Settings>();
                        }
                        // כתובת הבאה - הכי קרובה
                        else
                        {
                            lSettingDistance.Add(minSetting);
                            lSettings.Remove(minSetting);
                        }
                    }



                    //double minDistance = 1000000;
                    //Settings minSetting = new Settings();
                    //int countDistance = 0;
                    //foreach (Settings setting in lSettings)
                    //{
                    //    if (setting.nvLat != null && setting.nvLng != null)
                    //    {
                    //        countDistance++;
                    //        var Distance = DistanceMatrix.GetDistanceMatrix(0,
                    //            lSettingDistance.Last().nvLat + "," + lSettingDistance.Last().nvLng,
                    //            setting.nvLat.ToString() + "," + setting.nvLng.ToString());
                    //        if (Distance.rows[0].elements[0].distance.value < minDistance)
                    //        {
                    //            minDistance = Distance.rows[0].elements[0].distance.value;
                    //            minSetting = setting;
                    //        }
                    //    }
                    //}
                    //// אם כל רשימת המסגרות שנשארה מכילה מסגרות ללא נקודת אורך ורוחב
                    //if (countDistance == 0)
                    //{
                    //    lSettingDistance.AddRange(lSettings);
                    //    lSettings = new List<Settings>();
                    //}
                    //else
                    //{
                    //    lSettingDistance.Add(minSetting);
                    //    lSettings.Remove(minSetting);
                    //}

                }
                //var Distance = DistanceMatrix.GetDistanceMatrix(0, lSettingDistance[0].nvLat + "," + lSettingDistance[0].nvLng, lSettings[1].nvLat.ToString() + "," + lSettings[1].nvLng.ToString());
                //if (distance.rows[0].elements[0].duration.value / 60 > 15)//המרחק חוזר בשניות ואני רוצה לבדוק מקרה של יותר מרבע שעה
                //string duration = Distance.rows[0].elements[0].duration.value.ToString();
                //string distance = Distance.rows[0].elements[0].distance.value.ToString();

                return lSettingDistance;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("DistanceSettings", ex);
                return null;
            }
        }

        public static int SettingInsertUpdate(Settings oSetting, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams = ObjectGenerator<Settings>.GetSqlParametersFromObject(oSetting);
                lParams.Add(ObjectGenerator<int>.GenerateSimpleDataTableFromList(oSetting.lSettingAgegroups, "id", "dtSettingAgegroups"));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSetting_InsUpdt", lParams);
                int iSettingId = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());

                return iSettingId;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SettingInsertUpdate", ex);
                return 0;
            }
        }

        public static List<Settings> GetSettingsSchedule(int? iProgramId, int? iSettingId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iProgramId", iProgramId));
                lParams.Add(new SqlParameter("iSettingId", iSettingId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSchedulingWeekly_Slct", lParams);
                List<Settings> lSettings = new List<Settings>();
                if (ds.Tables.Count > 0)
                    lSettings = ObjectGenerator<Settings>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                foreach (Settings setting in lSettings)
                {
                    setting.lSettingAgegroups = new List<int>();
                    setting.lSchedule = new List<Schedule>();
                }


                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Settings setting = lSettings.Where(s => s.iSettingId == Convert.ToInt16(dr["iSettingId"].ToString())).FirstOrDefault();
                    setting.lSettingAgegroups.Add(Convert.ToInt16(dr["iAgegroupType"].ToString()));
                }

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Settings settings = lSettings.Where(s => s.iSettingId == Convert.ToInt16(dr["iSettingId"].ToString())).FirstOrDefault();
                    settings.lSchedule.Add(ObjectGenerator<Schedule>.GeneratFromDataRow(dr));
                }

                return lSettings;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetSettingsSchedule", ex);
                return null;
            }
        }

        public static string CreateSettingsSchedulePDF(int? iProgramId)
        {
            try
            {
                string url = ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("PdfPath")];

                return PDFGenerator.CreatePdf1(url, "PDF");
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("CreateReport", ex);
                return null;
            }
        }


        #endregion Methods
    }

    [DataContract]
    public class SettingsJoint
    {
        #region Members
        [DataMember]
        public int? iSettingJointId { get; set; }
        [DataMember]
        public int? iSettingId1 { get; set; }
        [DataMember]
        [NoSendToSQL]
        public string nvSettingName1 { get; set; }
        [DataMember]
        public int? iSettingId2 { get; set; }
        [DataMember]
        [NoSendToSQL]
        public string nvSettingName2 { get; set; }

        #endregion Members

        #region Methods

        public static List<SettingsJoint> GetSettingsJoint()
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSettingsJoint_Slct");
                List<SettingsJoint> lSettingsJoint = new List<SettingsJoint>();
                lSettingsJoint = ObjectGenerator<SettingsJoint>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                return lSettingsJoint;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetSettingsJoint", ex);
                return null;
            }
        }

        public static List<SettingsJoint> SettingsJointInsertUpdate(int? iSettingJointId, int? iSettingId1, int? iSettingId2, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParam = new List<SqlParameter>();
                lParam.Add(new SqlParameter("iSettingJointId", iSettingJointId));
                lParam.Add(new SqlParameter("iSettingId1", iSettingId1));
                lParam.Add(new SqlParameter("iSettingId2", iSettingId2));
                lParam.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSettingsJoint_InsUpdt", lParam);
                List<SettingsJoint> lSettingsJoint = new List<SettingsJoint>();
                lSettingsJoint = ObjectGenerator<SettingsJoint>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                return lSettingsJoint;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SettingsJointInsertUpdate", ex);
                return null;
            }
        }


        #endregion Methods
    }

    [DataContract]
    public class SettingsClusters
    {
        #region Members

        [DataMember]
        public int? iClusterId { get; set; }
        [DataMember]
        public List<int> lSettingId { get; set; }
        [DataMember]
        [NoSendToSQL]
        public string nvSetting { get; set; }

        #endregion Members

        #region Methods

        public static List<SettingsClusters> SettingsClustersSelect()
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSettingsClusters_Slct");
                List<SettingsClusters> lSettingsClusters = new List<SettingsClusters>();
                if (ds.Tables.Count > 0)
                {
                    lSettingsClusters = ObjectGenerator<SettingsClusters>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                    
                    foreach (SettingsClusters settingsClusters in lSettingsClusters)
                    {
                        settingsClusters.lSettingId = new List<int>();
                    }
                    if (ds.Tables.Count > 1)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            SettingsClusters settingsClusters = lSettingsClusters.Where(s => s.iClusterId == Convert.ToInt16(dr["iClusterId"].ToString())).FirstOrDefault();
                            settingsClusters.lSettingId.Add( Convert.ToInt16(dr["iSettingId"].ToString()) );
                        }
                    }
                }
                return lSettingsClusters;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SettingsClustersSelect", ex);
                return null;
            }
        }

        public static List<SettingsClusters> SettingsClustersInsertUpdate(SettingsClusters oSettingsClusters, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iClusterId", oSettingsClusters.iClusterId));
                lParams.Add(ObjectGenerator<int>.GenerateSimpleDataTableFromList(oSettingsClusters.lSettingId, "id", "dtSettingsClusters"));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSettingsClusters_InsUpdt", lParams);

                return SettingsClustersSelect();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SettingsClustersInsertUpdate", ex);
                return null;
            }

        }






        #endregion Methods

    }

}