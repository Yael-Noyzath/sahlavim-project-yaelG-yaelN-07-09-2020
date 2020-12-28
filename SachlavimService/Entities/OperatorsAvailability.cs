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
    [DataContract]
    public class OperatorsAvailability
    {
        #region Members
        [DataMember]
        public int? iOperatorsAvailabilityId { get; set; }
        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public int? iOperatorAvailabilityType { get; set; }
        [DataMember]
        public bool bActive { get; set; }
        [DataMember]
        public int? iWeekDay { get; set; }
        [DataMember]
        public string tMorningFromTime { get; set; }
        [DataMember]
        public string tMorningToTime { get; set; }
        [DataMember]
        public string tAfternoonFromTime { get; set; }
        [DataMember]
        public string tAfternoonToTime { get; set; }
        [DataMember]
        public int? iNumLeaders { get; set; }
        #endregion Members

        #region Methods
        public static List<OperatorsAvailability> OperatorsAvailabilityGet(int iOperatorId)
        {
            try
            {
                List<OperatorsAvailability> lOperatorsAvailability = new List<OperatorsAvailability>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperatorsAvailability_Slct", new SqlParameter("iOperatorId", iOperatorId));
                if (ds.Tables.Count > 0)
                    lOperatorsAvailability = ObjectGenerator<OperatorsAvailability>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                return lOperatorsAvailability;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("OperatorsAvailabilityGet", ex);
                return null;
            }
        }

        public static bool OperatorsAvailabilityUpdt(int iOperatorId, List<OperatorsAvailability> lOperatorsAvailability, int? iUserId)
        {
            try
            {
                DataTable OperatorsAvailability = new DataTable();
                OperatorsAvailability.Columns.Add("iOperatorsAvailabilityId", typeof(int));
                OperatorsAvailability.Columns.Add("iOperatorId", typeof(int));
                OperatorsAvailability.Columns.Add("iOperatorAvailabilityType", typeof(int));
                OperatorsAvailability.Columns.Add("bActive", typeof(bool));
                OperatorsAvailability.Columns.Add("iWeekDay", typeof(int));
                OperatorsAvailability.Columns.Add("tMorningFromTime", typeof(string));
                OperatorsAvailability.Columns.Add("tMorningToTime", typeof(string));
                OperatorsAvailability.Columns.Add("tAfternoonFromTime", typeof(string));
                OperatorsAvailability.Columns.Add("tAfternoonToTime", typeof(string));
                OperatorsAvailability.Columns.Add("iNumLeaders");

                foreach (OperatorsAvailability oa in lOperatorsAvailability)
                {
                    DataRow drow = OperatorsAvailability.NewRow();
                    drow["iOperatorsAvailabilityId"] = oa.iOperatorsAvailabilityId;
                    drow["iOperatorId"] = oa.iOperatorId;
                    drow["iOperatorAvailabilityType"] = oa.iOperatorAvailabilityType;
                    drow["iWeekDay"] = oa.iWeekDay;
                    drow["bActive"] = oa.bActive;
                    drow["tMorningFromTime"] = oa.tMorningFromTime;
                    drow["tMorningToTime"] = oa.tMorningToTime;
                    drow["tAfternoonFromTime"] = oa.tAfternoonFromTime;
                    drow["tAfternoonToTime"] = oa.tAfternoonToTime;
                    drow["iNumLeaders"] = oa.iNumLeaders;
                    OperatorsAvailability.Rows.Add(drow);
                }
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iOperatorId", iOperatorId));
                lParams.Add(new SqlParameter("lOperatorsAvailability", OperatorsAvailability));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperatorsAvailability_INS_UPD", lParams);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("OperatorsAvailabilityUpdt", ex);
                return false;
            }
        }
        #endregion Methods
    }
}