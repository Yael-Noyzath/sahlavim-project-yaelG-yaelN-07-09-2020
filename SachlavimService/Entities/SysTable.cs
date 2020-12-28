using SachlavimService.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Threading;

namespace SachlavimService.Entities
{
    [DataContract]
    public class Int_Array
    {
        [DataMember]
        public int id { get; set; }
    }

    [DataContract]
    public class Dictionary_IntString
    {
        [DataMember]
        public int Key { get; set; }
        [DataMember]
        public string Value { get; set; }
    }

    [DataContract]
    public class SysTableContent
    {
        [DataMember]
        public int? iListId { get; set; }
        [DataMember]
        public bool bIsShow { get; set; }
        [DataMember]
        public string sListName { get; set; }
        [DataMember]
        public List<Dictionary_IntString> dParams { get; set; }

        #region Methods

        public static SysTableContent[] SysTableListGet()
        {
            try
            {
                List<SysTableContent> lGlobalParams = new List<SysTableContent>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSysTables_Slct");
                DataTableCollection dt = ds.Tables;//קישור לטבלאות במסד הנתונים
                
                // לולאה שעוברת על כל השורות של טבלת TSysTable שזוהי טבלה שמקבצת בתוכה חלק מהערכים במסד)
                //במסד sql
                //ועבור כל שורה יוצר אובייקט המכיל את נתוני השורה- טבלה  
                //והאובייקט ניכנס לאובייקט lGlobalParams
                for (int i = 0; i < dt[0].Rows.Count; i++)
                {
                    lGlobalParams.Add(new SysTableContent()
                    {
                        dParams = new List<Dictionary_IntString>(),//רשימת כל הערכים של טבלה ע"י id של הטבלה והערך של השדה
                        iListId = int.Parse(dt[0].Rows[i]["iSysTableId"].ToString()),// id של הטבלה
                        sListName = dt[0].Rows[i]["nvSysTableNameHeb"].ToString(),//שם הטבלה בעברית
                        bIsShow = bool.Parse(dt[0].Rows[i]["bIsShow"].ToString())//האם להציגה
                    });
                }
                for (int i = 0; i < dt[1].Rows.Count; i++)//מילוי הערכים ע"י לולאה שעוברת על טבלת SysTableRow
                {   //item שווה בכל איטרציה לאובייקט במקום ה i של lGlobalParams ולתוכו מוכנס ערכי הטבלה
                    var item = lGlobalParams.Where(g => g.iListId == int.Parse(dt[1].Rows[i]["iSysTableId"].ToString())).FirstOrDefault();
                    if (item != null)
                        item.dParams.Add(new Dictionary_IntString() { Key = int.Parse(dt[1].Rows[i]["iSysTableRowId"].ToString()), Value = dt[1].Rows[i]["nvValue"].ToString() });
                }
                                
                return lGlobalParams.ToArray();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SysTableListGet", ex);
                return null;
            }

        }

        public static List<Dictionary_IntString> GetSysTableContent(int id)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("iSysTableId", id) };

                DataTable dt = SqlDataAccess.ExecuteDatasetSP("TSysTableContent_Slct", param).Tables[0];
                List<Dictionary_IntString> dRes = new List<Dictionary_IntString>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dRes.Add(new Dictionary_IntString() { Key = int.Parse(dt.Rows[i]["iSysTableRowId"].ToString()),Value = dt.Rows[i]["nvValue"].ToString() });
                    }
                }
               
                return dRes;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetSysTableContent", ex);
                return null;
            }

        }
        
        public static Dictionary_IntString AddUpdateSysTableRow(Dictionary_IntString SysTableRow , int iSysTableId, int iUserId)
        {
            try
            {
                Dictionary_IntString dis = new Dictionary_IntString();
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iSysTableRowId", SysTableRow.Key));
                lParams.Add(new SqlParameter("nvValue", SysTableRow.Value));
                lParams.Add(new SqlParameter("iSysTableId", iSysTableId));
                lParams.Add(new SqlParameter("iUserId", iUserId));

                DataTable dt = SqlDataAccess.ExecuteDatasetSP("TSysTableContent_InsUpdt", lParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dis = (new Dictionary_IntString() { Key = int.Parse(dt.Rows[0]["iSysTableRowId"].ToString()), Value = dt.Rows[0]["nvValue"].ToString() });
                }
                return dis;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("AddUpdatesaveSysTableRow", ex);
                return null;
            }

        }

        public static int? SysRowCheck(int iSysTableRowId)
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSysRow_CHCK", new SqlParameter("iSysTableRowId", iSysTableRowId));
                int sumCheck = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
                if (sumCheck == 0) sumCheck = -2;// כדי שלא יחזיר שגיאה בלתי צפויה...
                return sumCheck;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SysRowCheck", ex);
                return null;
            }
        }

        public static bool? SysRowDelete(int iSysTableRowId, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iSysTableRowId", iSysTableRowId));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSysRow_DEL", lParams);

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SysRowDelete", ex);
                return false;

                throw;
            }
        }

        //------------------------------JOB-------------------------


        public static void UpdateNewJob()
        {
            LogWriter.WriteLog("UpdateNewJobThread", null);
            var th = new Thread(UpdateNewJobThread);
            th.Start();
            Thread.Sleep(5000);
        }

        private static void UpdateNewJobThread()
        {
            try
            {

                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSysLogJob_Ins");
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("UpdateNewJobThread", ex);
            }
        }

        #endregion
    }
}