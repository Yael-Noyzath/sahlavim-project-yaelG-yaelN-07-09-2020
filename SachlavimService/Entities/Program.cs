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


namespace SachlavimService.Entities
{
    [DataContract]
    public class Program
    {
        #region Members

        [DataMember]
        public int? iProgramId { get; set; }
        [DataMember]
        public int? iProgramType { get; set; }
        [DataMember]
        public string nvProgramName { get; set; }
        [DataMember]
        public string nvBudgetItem { get; set; }
        [DataMember]
        public DateTime? dFromDate { get; set; }
        [DataMember]
        public DateTime? dToDate { get; set; }
        [DataMember]
        public int? iNumActivityMorning { get; set; }
        [DataMember]
        public int? iNumActivityAfternoon { get; set; }
        [DataMember]
        public int? iActivityPreferenceCount { get; set; }
        [DataMember]
        public int? iActivityPreferenceInWeekCount { get; set; }
        [DataMember]
        public DateTime? tFromTimeMorning { get; set; }
        [DataMember]
        public DateTime? tToTimeMorning { get; set; }
        [DataMember]
        public DateTime? tFromTimeAfternoon { get; set; }
        [DataMember]
        public DateTime? tToTimeAfternoon { get; set; }
        [DataMember]
        public bool? bTwoActivitiesThatDay { get; set; }
        [DataMember]
        public int? iStatusType { get; set; }
        [DataMember]
        public int iYearType { get; set; }
        [DataMember]
        public int? iNumActivityInWeek { get; set; }
        [DataMember]
        public Boolean? bProgramAfternoon { get; set; }
        [DataMember]
        public int? iSemesterType { get; set; }
        [DataMember]
        public DateTime? tFirstActivity { get; set; }
        [DataMember]
        public DateTime? tSecondActivity { get; set; }
        [DataMember]
        [NoSendToSQL]
        public DateTime? dateFirstWeek { get; set; }
        [DataMember]
        [NoSendToSQL]
        public DateTime? dateLastWeek { get; set; }
        [DataMember]
        [NoSendToSQL]
        public List<int> lProgramAgegroups { get; set; }
        [DataMember]
        [NoSendToSQL]
        public List<int> lProgramSettings { get; set; }

        #endregion Members

        #region Methods

        public static List<Program> ProgramsGet(bool? bProgramAfternoon)
        {
            try
            {
                List<Program> lProgram = new List<Program>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TProgram_Slct",new SqlParameter("bProgramAfternoon", bProgramAfternoon));
                if (ds.Tables.Count > 0)
                    lProgram = ObjectGenerator<Program>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                foreach (Program program in lProgram)
                {
                    program.lProgramAgegroups = new List<int>();
                    program.lProgramSettings = new List<int>();
                }
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Program program = lProgram.Where(p => p.iProgramId == Convert.ToInt16(dr["iProgramId"].ToString())).FirstOrDefault();
                    program.lProgramAgegroups.Add(Convert.ToInt16(dr["iAgegroupType"].ToString()));
                }
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Program program = lProgram.Where(p => p.iProgramId == Convert.ToInt16(dr["iProgramId"].ToString())).FirstOrDefault();
                    program.lProgramSettings.Add(Convert.ToInt16(dr["iSettingId"].ToString()));
                }
              
                return lProgram;
            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("ProgramsGet", ex);
                return null;
            }

        }
        
        public static int ProgramInsertUpdate(Program oProgram, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams = ObjectGenerator<Program>.GetSqlParametersFromObject(oProgram);
                lParams.Add(ObjectGenerator<int>.GenerateSimpleDataTableFromList(oProgram.lProgramAgegroups, "id", "dtProgramAgegroups"));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TProgram_InsUpdt", lParams);
                int iProgramId = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());

                return iProgramId;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ProgramInsertUpdate", ex);
                return 0;
            }
        }

        public static bool ProgramSettingsInsertUpdate(int iProgramId, List<int> lProgramSettings, List<int> lSettingMorning, List<int> lSettingNoon, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iProgramId", iProgramId));
                lParams.Add(ObjectGenerator<int>.GenerateSimpleDataTableFromList(lProgramSettings, "id", "dtProgramSettings"));
                lParams.Add(ObjectGenerator<int>.GenerateSimpleDataTableFromList(lSettingMorning, "id", "dtSettingMorning"));
                lParams.Add(ObjectGenerator<int>.GenerateSimpleDataTableFromList(lSettingNoon, "id", "dtSettingNoon"));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TProgramSettings_InsUpdt", lParams);

                return true;
            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("ProgramSettingsInsertUpdate", ex);
                return false;
            }
        }

        public static List<int> ProgramSettingOrderUpdate(int iProgramId, List<Settings> lSettings)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                List<Settings> ls = new List<Settings>();
                ls = Settings.DistanceSettings(lSettings);
                List<int> lSettingId = new List<int>();
                foreach(Settings setting in ls)
                {
                    lSettingId.Add(Convert.ToInt16(setting.iSettingId));
                }
                lParams.Add(new SqlParameter("iProgramId", iProgramId));
                lParams.Add(ObjectGenerator<int>.GenerateSimpleDataTableFromList(lSettingId, "id", "dtProgramSettings"));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TProgramSettings_Updt", lParams);

                return lSettingId;
            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("ProgramSettingUpdate", ex);
                return null;
            }
        }

        public static List<OperatorActivityDate> ActivityNoSchedulingGet(int? iProgramId)
        {
            try
            {
                List<OperatorActivityDate> lOperatorActivityDate = new List<OperatorActivityDate>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TActivityNoSchedulingCamp_Slct", new SqlParameter("iProgramId",iProgramId));
                if(ds.Tables.Count > 0)
                {
                    lOperatorActivityDate = ObjectGenerator<OperatorActivityDate>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                }
                return lOperatorActivityDate;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ActivityNoSchedulingGet", ex);
                return null;
            }
        }

        #endregion Methods

    }
}