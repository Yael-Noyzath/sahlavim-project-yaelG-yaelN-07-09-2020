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
    public class User
    {
        #region Members

        [DataMember]
        public int? iUserId { get; set; }
        [DataMember]
        public string nvUserName { get; set; }
        [DataMember]
        public string nvPassword { get; set; }
        [DataMember]
        public int? iUserType { get; set; }
        [DataMember]
        public int? iUserStatus { get; set; }
        [DataMember]
        public string nvFirstName { get; set; }
        [DataMember]
        public string nvLastName { get; set; }
        [DataMember]
        public string nvMobile { get; set; }
        [DataMember]
        public string nvMail { get; set; }

        #endregion Members

        #region Methods

        public static User UserLogin(string nvUserName, string nvPassword, string nvMail)
        {
            try
            {
                User oUser = new User();
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("nvUserName", nvUserName));
                lParams.Add(new SqlParameter("nvPassword", nvPassword));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TUser_Login_Slct", lParams);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    oUser = ObjectGenerator<User>.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                }
                return oUser;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("UserLogin", ex);
                return null;
            }
        }

        public static User UserReset(string nvMail)
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TUser_Reset_Slct", new SqlParameter("nvMail", nvMail));
                User oUser = new User();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    oUser = ObjectGenerator<User>.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                }
                if (oUser.iUserId != null && oUser.iUserId != -1)
                {
                    string[] lEmail = new string[1];
                    string[] lCopies = new string[0];
                    lEmail[0] = nvMail;
                    NotificationHandler.SendMail(lEmail, lCopies, ConfigurationManager.AppSettings["mailFrom"].ToString(),
                        "סחלבים - סיסמא", oUser.nvPassword);
                }
                return oUser;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("UserReset", ex);
                return null;
            }
        }

        public static List<User> GetUsers()
        {
            try
            {
                List<User> lUser = new List<User>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TUsers_Slct");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lUser = ObjectGenerator<User>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                }
                return lUser;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetUsers", ex);
                return null;
            }
        }

        public static User AddUpdateUser(User oUser)
        {
            try
            {
                User user = new User();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters = ObjectGenerator<User>.GetSqlParametersFromObject(oUser);
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TUsers_InsUpdt", parameters);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    user = ObjectGenerator<User>.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                }
                return user;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("AddUpdateUser", ex);
                return null;
            }
        }

        #endregion Methods


    }
}