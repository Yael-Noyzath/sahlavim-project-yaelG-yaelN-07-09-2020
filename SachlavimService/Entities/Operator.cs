using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SachlavimService.Utilities;

namespace SachlavimService.Entities
{
    [DataContract]
    public class Operator
    {
        #region Members

        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public string nvOperatorName { get; set; }
        [DataMember]
        public string nvCompanyName { get; set; }
        [DataMember]
        public string nvOperatorNumber { get; set; }
        [DataMember]
        public int? iOperatorType { get; set; }
        [DataMember]
        public int? iOperatorPaymentType { get; set; }
        [DataMember]
        public string nvIdentity { get; set; }
        [DataMember]
        public string nvContactPerson { get; set; }
        [DataMember]
        public string nvContactPersonMail { get; set; }
        [DataMember]
        public string nvContactPersonPhone { get; set; }
        [DataMember]
        public bool? bInProgramPool { get; set; }
        [DataMember]
        public bool? bTalan { get; set; }
        [DataMember]
        public int? iNumOperationsDay { get; set; }
        [DataMember]
        public int? iNumOperationsWeek { get; set; }
        [DataMember]
        public string nvFilePathTax { get; set; }
        [DataMember]
        public string nvFilePathBooks { get; set; }
        [DataMember]
        public string nvFilePathContract { get; set; }
        [DataMember]
        public int iNumBookkeeping { get; set; }
        [DataMember]
        public bool bActiveAfternoon { get; set; }
        [DataMember]
        public bool? bActiveChanukahCamp { get; set; }
        [DataMember]
        public bool? bActivePassoverCamp { get; set; }
        [DataMember]
        public bool? bActiveSummerCamp { get; set; }
        [DataMember]
        public bool? bActivityPriority { get; set; }
        [DataMember]
        public int? iNumLeaders { get; set; }
        [DataMember]
        [NoSendToSQL]
        public bool bEnableDelete { get; set; }
        [DataMember]
        [NoSendToSQL]
        public List<int> lSchools { get; set; }
        [DataMember]
        [NoSendToSQL]
        public List<int> lNeighborhoods { get; set; }
        [DataMember]
        [NoSendToSQL]
        public List<int> lSchoolsExcude { get; set; }
        [DataMember]
        [NoGetFromSQL]
        [NoSendToSQL]
        public string nvActivityies { get; set; }
        [DataMember]
        [NoSendToSQL]
        [NoGetFromSQL]
        public List<Activity> lActivity { get; set; }
        [NoSendToSQL]
        [DataMember]
        public int? weekday { get; set; }
        [NoSendToSQL]
        [DataMember]
        public int? numactivities { get; set; }
        [NoSendToSQL]
        [DataMember]
        public int? missing { get; set; }

        #endregion Members

        #region Methods
        public static Operator GetOperator(int iOperatorId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iOperatorId", iOperatorId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperator_Slct", lParams);
                Operator Operator = new Operator();
                if (ds.Tables.Count > 0)
                    Operator = ObjectGenerator<Operator>.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                if (ds.Tables.Count > 1)
                {
                    Operator.lNeighborhoods = new List<int>();
                    Operator.lNeighborhoods = ObjectGenerator<int>.GenerateSimpleIntList(ds.Tables[1].Rows, "iAreasType");
                }
                if (ds.Tables.Count > 2)
                {
                    Operator.lSchools = new List<int>();
                    Operator.lSchools = ObjectGenerator<int>.GenerateSimpleIntList(ds.Tables[2].Rows, "iSettingId");
                }
                if (ds.Tables.Count > 3)
                {
                    Operator.lSchoolsExcude = new List<int>();
                    Operator.lSchoolsExcude = ObjectGenerator<int>.GenerateSimpleIntList(ds.Tables[3].Rows, "iSettingId");
                }
                if (ds.Tables.Count > 4)
                {
                    Operator.lActivity = new List<Activity>();
                    Operator.lActivity = ObjectGenerator<Activity>.GeneratListFromDataRowCollection(ds.Tables[4].Rows);
                }
                foreach (Activity activity in Operator.lActivity)
                {
                    activity.lActivityAgegroups = new List<int>();
                }
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    Activity activity = Operator.lActivity.Where(a => a.iActivityId == Convert.ToInt16(dr["iActivityId"].ToString())).FirstOrDefault();
                    activity.lActivityAgegroups.Add(Convert.ToInt16(dr["iAgegroupType"].ToString()));
                }

                return Operator;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetOperator", ex);
                return null;
            }
        }


        public static List<Operator> GetOperators()
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperators_Slct");
                List<Operator> lOperator = new List<Operator>();
                List<Activity> lActivity = new List<Activity>();
                if (ds.Tables.Count > 0)
                    lOperator = ObjectGenerator<Operator>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                foreach (Operator operator1 in lOperator)
                {
                    operator1.lActivity = new List<Activity>();
                    operator1.lNeighborhoods = new List<int>();
                    operator1.lSchools = new List<int>();
                    operator1.lSchoolsExcude = new List<int>();
                }

                if (ds.Tables.Count > 1)
                {
                    lActivity = ObjectGenerator<Activity>.GeneratListFromDataRowCollection(ds.Tables[1].Rows);

                    foreach (Operator operator1 in lOperator)
                    {
                        operator1.lActivity = lActivity.Where(a => a.iOperatorId == operator1.iOperatorId).ToList();
                        foreach (Activity activity in operator1.lActivity)
                        {
                            activity.lActivityAgegroups = new List<int>();
                        }
                    }
                }
                if (ds.Tables.Count > 2)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        Operator operator1 = lOperator.Where(o => o.iOperatorId == Convert.ToInt16(dr["iOperatorId"].ToString())).FirstOrDefault();
                        Activity activity = operator1.lActivity.Where(a => a.iActivityId == Convert.ToInt16(dr["iActivityId"].ToString())).FirstOrDefault();

                        activity.lActivityAgegroups.Add(Convert.ToInt16(dr["iAgegroupType"].ToString()));
                    }
                }
                for (int i = 0; i < lOperator.Count; i++)
                {
                    //lOperator[i].nvActivityies 
                    //var count = lActivity.Where(x => x.iOperatorId == lOperator[i].iOperatorId).GroupBy(w => w.nvValue).Count();
                    var activitys = lActivity.Where(x => x.iOperatorId == lOperator[i].iOperatorId).GroupBy(w => w.nvValue).ToList();

                    for (int j = 0; j < activitys.Count; j++)
                    {
                        lOperator[i].nvActivityies = lOperator[i].nvActivityies + activitys[j].Key + ", ";
                    }

                }

                if (ds.Tables.Count > 3)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        Operator operator1 = lOperator.Where(o => o.iOperatorId == Convert.ToInt16(dr["iOperatorId"].ToString())).FirstOrDefault();
                        if(operator1!=null)
                        {
                        operator1.lNeighborhoods.Add(Convert.ToInt16(dr["iAreasType"].ToString()));
                        }
                    }
                }

                if (ds.Tables.Count > 4)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        Operator operator1 = lOperator.Where(o => o.iOperatorId == Convert.ToInt16(dr["iOperatorId"].ToString())).FirstOrDefault();
                        if(operator1!=null)
                        {
                        operator1.lSchoolsExcude.Add(Convert.ToInt16(dr["iSettingId"].ToString()));

                        }
                    }
                }


                //------------------------------
                
                return lOperator;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetOperators", ex);
                return null;
            }
        }

        public static List<Operator> GetOperatorsByDay(DateTime? dDate)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>() { new SqlParameter("dDate", dDate) };
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperatorsByDay_Slct", lParams);
                List<Operator> lOperator = GetOperators();

                List<Operator> lOperatorFiltered = new List<Operator>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Operator operator1 = lOperator.Where(o => o.iOperatorId == Convert.ToInt16(dr["iOperatorId"].ToString())).FirstOrDefault();
                    lOperatorFiltered.Add(operator1);
                }


                //List<Activity> lActivity = new List<Activity>();
                //if (ds.Tables.Count > 0)
                //    lOperator = ObjectGenerator<Operator>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);

                //foreach (Operator operator1 in lOperator)
                //{
                //    lActivity = ObjectGenerator<Activity>.GeneratListFromDataRowCollection(ds.Tables[1].Rows);
                //    if (ds.Tables.Count > 1)
                //    {
                //        operator1.lActivity = lActivity.Where(a => a.iOperatorId == operator1.iOperatorId).ToList();
                //        foreach (Activity activity in operator1.lActivity)
                //        {
                //            activity.lActivityAgegroups = new List<int>();
                //        }
                //    }
                //}
                //if (ds.Tables.Count > 2)
                //{
                //    foreach (DataRow dr in ds.Tables[2].Rows)
                //    {
                //        Operator operator1 = lOperator.Where(o => o.iOperatorId == Convert.ToInt16(dr["iOperatorId"].ToString())).FirstOrDefault();
                //        Activity activity = operator1.lActivity.Where(a => a.iActivityId == Convert.ToInt16(dr["iActivityId"].ToString())).FirstOrDefault();

                //        activity.lActivityAgegroups.Add(Convert.ToInt16(dr["iAgegroupType"].ToString()));
                //    }
                //}

                return lOperatorFiltered;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetOperatorsByDay", ex);
                return null;
            }

        }

        public static List<Operator> GetOperatorMissing(int? iProgramId)
        {
            try
            {
                List<Operator> lOperator = new List<Operator>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperatorMissing_Slct", new SqlParameter("iProgramId", iProgramId));
                if (ds.Tables.Count > 0)
                    lOperator = ObjectGenerator<Operator>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                return lOperator;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetOperatorMissing", ex);
                return null;
            }
        }

        public static List<SqlParameter> UpdateAddOperator(Operator oOperator)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                if (oOperator.nvFilePathBooks != null && oOperator.nvFilePathBooks != "" && oOperator.nvFilePathBooks.IndexOf("+++**") != -1)
                {
                    string file = oOperator.nvFilePathBooks;
                    string timeStmp = DateTime.Now.Ticks.ToString();
                    Global.SaveFile("Books" + oOperator.nvIdentity + "_" + timeStmp, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Books\\", file.Substring(0, file.IndexOf("+++**")), file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5)));
                    oOperator.nvFilePathBooks = "Books" + oOperator.nvIdentity + "_" + timeStmp + "." + file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5));
                }

                if (oOperator.nvFilePathContract != null && oOperator.nvFilePathContract != "" && oOperator.nvFilePathContract.IndexOf("+++**") != -1)
                {
                    string file = oOperator.nvFilePathContract;
                    string timeStmp = DateTime.Now.Ticks.ToString();
                    Global.SaveFile("Contract" + oOperator.nvIdentity + "_" + timeStmp, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Contract\\", file.Substring(0, file.IndexOf("+++**")), file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5)));
                    oOperator.nvFilePathContract = "Contract" + oOperator.nvIdentity + "_" + timeStmp + "." + file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5));
                }

                if (oOperator.nvFilePathTax != null && oOperator.nvFilePathTax != "" && oOperator.nvFilePathTax.IndexOf("+++**") != -1)
                {
                    string file = oOperator.nvFilePathTax;
                    string timeStmp = DateTime.Now.Ticks.ToString();
                    Global.SaveFile("Tax" + oOperator.nvIdentity + "_" + timeStmp, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Tax\\", file.Substring(0, file.IndexOf("+++**")), file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5)));
                    oOperator.nvFilePathTax = "Tax" + oOperator.nvIdentity + "_" + timeStmp + "." + file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5));
                }
                parameters = ObjectGenerator<Operator>.GetSqlParametersFromObject(oOperator);
                if (oOperator.lSchools == null)
                {
                    oOperator.lSchools = new List<int>();
                }
                if (oOperator.lNeighborhoods == null)
                {
                    oOperator.lNeighborhoods = new List<int>();
                }
                if (oOperator.lSchoolsExcude == null)
                {
                    oOperator.lSchoolsExcude = new List<int>();
                }

                DataTable dtNeighborhoods = new DataTable();
                dtNeighborhoods.Columns.Add("id", typeof(int));

                foreach (int NeighborhoodId in oOperator.lNeighborhoods)
                {
                    DataRow drow = dtNeighborhoods.NewRow();
                    drow["id"] = NeighborhoodId;
                    dtNeighborhoods.Rows.Add(drow);
                }

                DataTable dtSchools = new DataTable();
                dtSchools.Columns.Add("id", typeof(int));

                foreach (int SchoolId in oOperator.lSchools)
                {
                    DataRow drow = dtSchools.NewRow();
                    drow["id"] = SchoolId;
                    dtSchools.Rows.Add(drow);
                }

                DataTable dtSchoolsExcude = new DataTable();
                dtSchoolsExcude.Columns.Add("id", typeof(int));

                foreach (int SchoolExcudeId in oOperator.lSchoolsExcude)
                {
                    DataRow drow = dtSchoolsExcude.NewRow();
                    drow["id"] = SchoolExcudeId;
                    dtSchoolsExcude.Rows.Add(drow);
                }
                parameters.Add(new SqlParameter("lNeighborhoods", dtNeighborhoods));
                parameters.Add(new SqlParameter("lSchools", dtSchools));
                parameters.Add(new SqlParameter("lSchoolsExcude", dtSchoolsExcude));
                return parameters;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("UpdateOperator", ex);
                return new List<SqlParameter>();
            }
        }

        public static int UpdateOperator(Operator oOperator)
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperator_UPD", UpdateAddOperator(oOperator));
                DataRow dr = ds.Tables[0].Rows[0];
                if (int.Parse(dr[0].ToString()) > 0)
                    return int.Parse(dr[0].ToString());
                return -1;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("UpdateOperator", ex);
                return -1;
            }
        }

        public static int AddOperator(Operator oOperator)
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperator_INS", UpdateAddOperator(oOperator));
                DataRow dr = ds.Tables[0].Rows[0];
                if (int.Parse(dr[0].ToString()) > 0)
                    return int.Parse(dr[0].ToString());
                return -1;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("AddOperator", ex);
                return -1;
            }
        }

        public static List<Operator> DeleteOperator(int iOperatorId, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iOperatorId", iOperatorId));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperator_Del", lParams);
                return GetOperators();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("DeleteOperator", ex);
                return null;
            }
        }

        public static bool deleteFile(string nvFilePath, string nvFileName)
        {
            try
            {
                if (Global.DeleteFile(nvFileName, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + nvFilePath + "\\"))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("DeleteFile", ex);
                return false;
            }
        }

        #endregion Methods
    }
}