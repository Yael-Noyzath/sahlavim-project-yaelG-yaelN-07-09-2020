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
    public class IVR
    {

        #region Members

        //        מחפש פעילות שאמורה להתבצע במסגרת זו ע"י מפעיל זה
        //        בטווח של 15 דקות קדימה ואחורה
        public static int ScheduleCheck(int? iOperatorId, int? iSettingId)
        {
            try
            {
                List<SqlParameter> lParam = new List<SqlParameter>();
                lParam.Add(new SqlParameter("iOperatorId", iOperatorId));
                lParam.Add(new SqlParameter("iSettingId", iSettingId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSchedule_Check", lParam);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
                else
                    return -1;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ScheduleCheck", ex);
                return 0;
            }
        }

        //        עדכון לפעילות זמן כניסה וכן מעדכן סטאטוס בוצע
        public static bool ScheduleTimeUpdate(int? iScheduleId , int? iUserId)
        {
            try
            {
                List<SqlParameter> lParam = new List<SqlParameter>();
                lParam.Add(new SqlParameter("iScheduleId", iScheduleId));
                lParam.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TScheduleTime_Updt", lParam);

                return true;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ScheduleTimeUpdate", ex);
                return false;
            }
        }

        #endregion Members
    }
}