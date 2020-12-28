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
//using SahlavimScheduleAlgorithm.shibutzAlgorithm;

namespace SachlavimService.Entities
{
    [DataContract]
    public class Schedule
    {
        #region Members

        [DataMember]
        public int? iScheduleId { get; set; }
        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public int? iActivityId { get; set; }
        [DataMember]
        public string nvActivityName { get; set; }
        [DataMember]
        public int? iSettingId { get; set; }
        [DataMember]
        public string nvSettingName { get; set; }
        [DataMember]
        public int? iClusterId { get; set; }
        [DataMember]
        public string nvOperatingLocation { get; set; }
        [DataMember]
        public string nvAddress { get; set; }
        [DataMember]
        public string nvPhone { get; set; }
        [DataMember]
        public int? iProgramId { get; set; }
        [DataMember]
        public string nvProgramValue { get; set; }
        [DataMember]
        public string nvOperatorName { get; set; }
        [DataMember]
        public string nvCompanyName { get; set; }
        [DataMember]
        public DateTime? dtStartTime { get; set; }
        [DataMember]
        public bool? bClosedDay { get; set; }
        [DataMember]
        public bool? bLongDay { get; set; }
        [DataMember]
        public string nvComment { get; set; }
        [DataMember]
        public DateTime? dtDoneDate { get; set; }
        [DataMember]
        public int? iActivityDetailsId { get; set; }
        [DataMember]
        public int? iLeaderNumber { get; set; }
        [DataMember]
        public int? iCategoryType { get; set; }
        [DataMember]
        [NoSendToSQL]
        public string nvCategoryValue { get; set; }
        [DataMember]
        [NoSendToSQL]
        public int? iDayInWeek { get; set; }
        #endregion Members

        #region Methods

        public static List<Schedule> SchedulesGet(int? iOperatorId, int? iSettingId, int? iProgramId, DateTime? dDate)
        {
            try
            {
                List<Schedule> lSchedule = new List<Schedule>();
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iOperatorId", iOperatorId));
                lParams.Add(new SqlParameter("iSettingId", iSettingId));
                lParams.Add(new SqlParameter("iProgramId", iProgramId));
                lParams.Add(new SqlParameter("dDate", dDate));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSchedule_Slct", lParams);
                if (ds.Tables.Count > 0)
                    lSchedule = ObjectGenerator<Schedule>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);

                return lSchedule;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SchedulesGet", ex);
                return null;
            }
        }

        // Scheduling the activities in Schedule Table
        public static List<Schedule> ScheduleInsertUpdate(int? iProgramId, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iProgramId", iProgramId));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP(0, "TScheduleCamp_INS", lParams.ToArray());

                ////  שליחה לאלגוריתם של השיבוץ

                //ShibutzApp shibutz = new ShibutzApp();

                //ShibutzApp.connectionString = SqlConnectDB.GetConnectionString(1, false);
                //shibutz.startShibutz(iProgramId.Value);

                ////------------------------------

                return SchedulesGet(-1, -1, iProgramId, null);
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ScheduleInsertUpdate", ex);
                return null;
            }
        }

        // Close day of operator / all system
        public static List<Schedule> SceduleCloseDayInsertUpdate(int? iOperatorId, DateTime? dDate, bool? bIsActive, bool? bLongDay, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iOperatorId", iOperatorId));
                lParams.Add(new SqlParameter("dDate", dDate));
                lParams.Add(new SqlParameter("bIsActive", bIsActive));
                lParams.Add(new SqlParameter("bLongDay", bLongDay));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TScheduleCloseDay_InsUpdt", lParams);
                List<Schedule> lSchedule = new List<Schedule>();
                if (ds.Tables.Count > 0)
                    lSchedule = ObjectGenerator<Schedule>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);

                return lSchedule;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SceduleCloseDayInsertUpdate", ex);
                return null;
            }
        }

        // Edit schedule datetime from calendar
        public static bool ScheduleUpdate(int? iScheduleId, int? iOperatorId, int? iActivityId, int? iSettingId, int? iProgramId, DateTime? dtStartTime, bool? bCopyAllWeeks, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iScheduleId", iScheduleId));
                lParams.Add(new SqlParameter("iOperatorId", iOperatorId));
                lParams.Add(new SqlParameter("iActivityId", iActivityId));
                lParams.Add(new SqlParameter("iSettingId", iSettingId));
                lParams.Add(new SqlParameter("iProgramId", iProgramId));
                lParams.Add(new SqlParameter("dtStartTime", dtStartTime));
                lParams.Add(new SqlParameter("bCopyAllWeeks", bCopyAllWeeks));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSchedule_InsUpdt", lParams);

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ScheduleUpdate", ex);
                return false;
            }
        }

        // Edit schedule datetime from calendar And replace with other schedule
        public static bool ScheduleReplace(int? iScheduleSource, DateTime? dtStartTime, int? iScheduleReplace, bool? bCopyAllWeeks, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iScheduleSource", iScheduleSource));
                lParams.Add(new SqlParameter("dtStartTime", dtStartTime));
                lParams.Add(new SqlParameter("iScheduleReplace", iScheduleReplace));
                lParams.Add(new SqlParameter("bCopyAllWeeks", bCopyAllWeeks));
                lParams.Add(new SqlParameter("iUserId", iUserId));

                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSchedule_RPLC", lParams);

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ScheduleReplace", ex);
                return false;
            }
        }

        // Delete schedule 
        public static bool ScheduleDelete(int? iScheduleId, bool? bCopyAllWeeks, int? iUserId)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iScheduleId", iScheduleId));
                lParams.Add(new SqlParameter("bCopyAllWeeks", bCopyAllWeeks));
                lParams.Add(new SqlParameter("iUserId", iUserId));

                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSchedule_Dlt", lParams);

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ScheduleDelete", ex);
                return false;
            }
        }

        // Scheduling schedule for Afternoon
        public static List<Schedule> ScheduleAfternoonInsert(int? iProgramId, int? iUserId)
        {
            try
            {
                List<Schedule> lSchedule = new List<Schedule>();
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iProgramId", iProgramId));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP(0, "TScheduleAfternoon_INS", lParams.ToArray());
                if (ds.Tables.Count > 0)
                    lSchedule = ObjectGenerator<Schedule>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);

                return lSchedule;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ScheduleAfternoonInsert", ex);
                return null;
            }
        }

        // Scheduling all week of semester
        public static List<Schedule> SchedulingByInsert(int? iProgramId, int? iUserId)
        {
            try
            {
                List<Schedule> lSchedule = new List<Schedule>();
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iProgramId", iProgramId));
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TSchedulingByWeek_Ins", lParams);
                if (ds.Tables.Count > 0)
                    lSchedule = ObjectGenerator<Schedule>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);

                return lSchedule;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SchedulingByInsert", ex);
                return null;
            }
        }

        #endregion Methods
    }

    [DataContract]
    public class ValidSchedule
    {
        #region Members

        [DataMember]
        public bool? isValidOperationsDay { get; set; }
        [DataMember]
        public bool? isValidOperationsWeek { get; set; }
        [DataMember]
        public bool? isValidAvailable { get; set; }
        [DataMember]
        public bool? isBusy { get; set; }
        [DataMember]
        public bool? isClosed { get; set; }

        #endregion Members

        #region Methods

        public static ValidSchedule CheckIsValiidSchedule(int? iScheduleId, int? iOperatorId, DateTime? dtStartTime)
        {
            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams.Add(new SqlParameter("iScheduleId", iScheduleId));
                lParams.Add(new SqlParameter("iOperatorId", iOperatorId));
                lParams.Add(new SqlParameter("dtStartTime", dtStartTime));
                ValidSchedule validSchedule = new ValidSchedule();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TOperatorCheckValidSchedule_Slct", lParams);
                if (ds.Tables.Count > 0)
                    validSchedule = ObjectGenerator<ValidSchedule>.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                return validSchedule;
            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("CheckIsValiidSchedule", ex);
                return null;
            }

        }

#endregion Methods
    }
}