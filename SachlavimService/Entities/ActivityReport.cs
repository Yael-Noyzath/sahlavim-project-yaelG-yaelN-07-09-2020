using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SachlavimService.Utilities;

namespace SachlavimService.Entities
{
    public class ActivityReport
    {
        #region Members

        [DataMember]
        public int? iProgramId { get; set; }
        [DataMember]
        public int? iScheduleId { get; set; }
        [DataMember]
        public DateTime? dtStartTime { get; set; }
        [DataMember]
        public string nvActivityName { get; set; }
        
        [DataMember]
        public int? iStatusType { get; set; }
        [DataMember]
        public DateTime? dActualStartTime { get; set; }
        [DataMember]
        public string nvSettingName { get; set; }

        #endregion Members

        #region Methods

        public static List<ActivityReport> GetActivityRepors(int iOperatorId)
        {
            try
            {
                List<ActivityReport> lActivityRepor = new List<ActivityReport>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TActivityReports_Slct", new SqlParameter("iOperatorId", iOperatorId));
                if (ds.Tables.Count > 0)
                    lActivityRepor = ObjectGenerator<ActivityReport>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                return lActivityRepor;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetActivityRepors", ex);
                return null;
            }
        }

        public static int ActivityReportUpdt(int iStatusType, int iScheduleId)
        {
            try
            {
                int StatusType=-1;
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iStatusType", iStatusType));
                lParams.Add(new SqlParameter("iScheduleId", iScheduleId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TActivityReport_Updt", lParams);
                DataRow dr = ds.Tables[0].Rows[0];
                if (int.Parse(dr[0].ToString()) > 0)
                    StatusType = int.Parse(dr[0].ToString());
                return StatusType;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ActivityReportUpdt", ex);
                return -1;
            }
        }

        #endregion Methods
    }
}