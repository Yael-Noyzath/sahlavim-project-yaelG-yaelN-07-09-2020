using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using SachlavimService.Utilities;

namespace SachlavimService.Entities
{
    public class Messages
    {
        #region Members
        [DataMember]
        public int? iMessageId { get; set; }
        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public int? iSettingId { get; set; }
        [DataMember]
        public int? iMessageType { get; set; }
        [DataMember]
        public DateTime? dSendDate { get; set; }
        [DataMember]
        public string nvTopic { get; set; }
        [DataMember]
        public string nvContent { get; set; }
        [DataMember]
        public string nvFilePathAttached { get; set; }
        [DataMember]
        public int? iSendUserId { get; set; }

        #endregion Members

        #region Methods

        public static int InsertMessage(List<Messages> lMessage)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                DataTable dtMessage = new DataTable();
                dtMessage.Columns.Add("iOperatorId", typeof(int));
                dtMessage.Columns.Add("iSettingId", typeof(int));
                dtMessage.Columns.Add("iMessageType", typeof(int));
                dtMessage.Columns.Add("dSendDate", typeof(DateTime));
                dtMessage.Columns.Add("nvTopic", typeof(string));
                dtMessage.Columns.Add("nvContent", typeof(string));
                dtMessage.Columns.Add("nvFilePathAttached", typeof(string));
                dtMessage.Columns.Add("iSendUserId", typeof(int));

                foreach (Messages Message in lMessage)
                {
                    DataRow drow = dtMessage.NewRow();
                    if (Message.iOperatorId == null)
                        drow["iOperatorId"] = DBNull.Value;
                    else
                        drow["iOperatorId"] = Message.iOperatorId;
                    if (Message.iSettingId == null)
                        drow["iSettingId"] = DBNull.Value;
                    else
                        drow["iSettingId"] = Message.iSettingId;
                    drow["iMessageType"] = Message.iMessageType;
                    drow["dSendDate"] = Message.dSendDate;
                    drow["nvTopic"] = Message.nvTopic;
                    drow["nvContent"] = Message.nvContent;
                    drow["nvFilePathAttached"] = Message.nvFilePathAttached;
                    drow["iSendUserId"] = Message.iSendUserId;
                    dtMessage.Rows.Add(drow);
                }
                parameters.Add(new SqlParameter("lMessages", dtMessage));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TMessage_INS", parameters);
                int iMessageId = Convert.ToInt32(ds.Tables[0].Rows[0]["iMessageId"]);
                return iMessageId;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("InsertMessage", ex);
                return -1;
            }
        }

        public static List<Messages> GetMessages(int iOperatorId, int iSettingId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iOperatorId", iOperatorId));
                lParams.Add(new SqlParameter("iSettingId", iSettingId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TMessages_Slct",lParams);
                List <Messages> lMessages = new List<Messages>();
                if (ds.Tables.Count > 0)
                    lMessages = ObjectGenerator<Messages>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                return lMessages;
            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("GetMessages", ex);
                return null;
            }
        }

        #endregion Methods
    }
}