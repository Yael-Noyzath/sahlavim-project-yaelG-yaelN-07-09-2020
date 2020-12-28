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
    public class Activity
    {
        #region Members

        [DataMember]
        public int? iActivityId { get; set; }
        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public string nvActivityName { get; set; }
        [DataMember]
        public int? iCategoryType { get; set; }
        [DataMember]
        public int? iStatusType { get; set; }
        [DataMember]
        public string nvActivityProduct { get; set; }
        [DataMember]
        public decimal nPrice { get; set; }
        [DataMember]
        public int? nShortBreak { get; set; }
        [DataMember]
        public int? nLongBreak { get; set; }
        [DataMember]
        public bool? bActivityPreference { get; set; }
        [DataMember]
        public bool? bActivityMorning { get; set; }
        [DataMember]
        public bool? bActivityNoon { get; set; }
        [DataMember]
        [NoSendToSQL]
        public List<int> lActivityAgegroups { get; set; }
        [DataMember]
        [NoSendToSQL]
        public string nvValue { get; set; }

        #endregion Members

        #region Methods

        public static List<Activity> ActivityInsertUpdate(Activity oActivity, int? iUserId)
        {
            try
            {
                List<Activity> lActivity = new List<Activity>();
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams = ObjectGenerator<Activity>.GetSqlParametersFromObject(oActivity);
                lParams.Add(ObjectGenerator<int>.GenerateSimpleDataTableFromList(oActivity.lActivityAgegroups, "id", "dtActivityAgegroups"));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TActivity_InsUpdt", lParams);
                if (ds.Tables.Count > 0)
                    lActivity = ObjectGenerator<Activity>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                foreach (Activity activity in lActivity)
                {
                    activity.lActivityAgegroups = new List<int>();
                }
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Activity activity = lActivity.Where(a => a.iActivityId == Convert.ToInt16(dr["iActivityId"].ToString())).FirstOrDefault();
                    activity.lActivityAgegroups.Add(Convert.ToInt16(dr["iAgegroupType"].ToString()));
                }
                return lActivity;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ActivityInsertUpdate", ex);
                return null;
            }
        }
        #endregion Methods

    }

    [DataContract]
    public class OperatorActivityDate
    {
        [DataMember] public int? iOperatorId { get; set; }
        [DataMember] public string nvOperatorName { get; set; }
        [DataMember] public int? iActivityId { get; set; }
        [DataMember] public string nvActivityName { get; set; }
        [DataMember] public int? iNumLeaders { get; set; }
        [DataMember] public string nvAgegroup { get; set; }
        [DataMember] public DateTime? dDate { get; set; }
    }
}