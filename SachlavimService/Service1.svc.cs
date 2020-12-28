using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SachlavimService.Utilities;
using SachlavimService.Entities;

using System.Data;


namespace SachlavimService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        #region User

        public User UserLogin(string nvUserName, string nvPassword, string nvMail)
        {
            return User.UserLogin(nvUserName, nvPassword, nvMail);
        }

        public List<User> GetUsers()
        {
            return User.GetUsers();
        }

        public User AddUpdateUser(User oUser)
        {
            return User.AddUpdateUser(oUser);
        }

        public User UserReset(string nvMail)
        {
            return User.UserReset(nvMail);
        }

        #endregion User

        #region SysTable

        public SysTableContent[] SysTableListGet()
        {
            return SysTableContent.SysTableListGet();
        }

        public List<Dictionary_IntString> GetSysTableContent(int id)
        {
            return SysTableContent.GetSysTableContent(id);
        }

        public Dictionary_IntString AddUpdateSysTableRow(Dictionary_IntString SysTableRow, int iSysTableId, int iUserId)
        {
            return SysTableContent.AddUpdateSysTableRow(SysTableRow, iSysTableId, iUserId);
        }

        public int? SysRowCheck(int iSysTableRowId)
        {
            return SysTableContent.SysRowCheck(iSysTableRowId);
        }

        public bool? SysRowDelete(int iSysTableRowId, int? iUserId)
        {
            return SysTableContent.SysRowDelete(iSysTableRowId, iUserId);
        }

        public void UpdateNewJob()
        {
            SysTableContent.UpdateNewJob();
        }
        #endregion SysTable

        #region Settings

        public List<Settings> SettingsGet()
        {
            return Settings.SettingsGet();
        }

        public int? GetSettingByPhone(string nvPhone)
        {
            return Settings.GetSettingByPhone(nvPhone);
        }

        public List<Settings> DistanceSettings(List<Settings> lSettings)
        {
            return Settings.DistanceSettings(lSettings);
        }

        public int SettingInsertUpdate(Settings oSetting, int? iUserId)
        {
            return Settings.SettingInsertUpdate(oSetting, iUserId);
        }

        public List<Settings> GetSettingsSchedule(int? iProgramId, int? iSettingId)
        {
            return Settings.GetSettingsSchedule(iProgramId, iSettingId);
        }

        public string CreateSettingsSchedulePDF(int? iProgramId)
        {
            return Settings.CreateSettingsSchedulePDF(iProgramId);
        }

        #endregion Settings

        #region SettingsJoint

        public List<SettingsJoint> GetSettingsJoint()
        {
            return SettingsJoint.GetSettingsJoint();
        }

        public List<SettingsJoint> SettingsJointInsertUpdate(int? iSettingJointId, int? iSettingId1, int? iSettingId2, int? iUserId)
        {
            return SettingsJoint.SettingsJointInsertUpdate(iSettingJointId, iSettingId1, iSettingId2, iUserId);

        }

        #endregion SettingsJoint

        #region SettingsClusters

        public List<SettingsClusters> SettingsClustersSelect()
        {
            return SettingsClusters.SettingsClustersSelect();
        }

        public List<SettingsClusters> SettingsClustersInsertUpdate(SettingsClusters oSettingsClusters, int? iUserId)
        {
            return SettingsClusters.SettingsClustersInsertUpdate(oSettingsClusters, iUserId);
        }

        #endregion SettingsClusters

        #region Coordinator

        public List<Coordinator> CoordinatorsGet()
        {
            return Coordinator.CoordinatorsGet();
        }

        public List<Coordinator> CoordinatorInsertUpdt(Coordinator oCoordinator, int? iUserId)
        {
            return Coordinator.CoordinatorInsertUpdt(oCoordinator, iUserId);
        }

        #endregion Coordinators

        #region Operator

        public Operator GetOperator(int iOperatorId)
        {
            return Operator.GetOperator(iOperatorId);
        }
        
        public List<Operator> GetOperators()
        {
            return Operator.GetOperators();
        }

        public List<Operator> GetOperatorsByDay(DateTime? dDate)
        {
            return Operator.GetOperatorsByDay(dDate);
        }

        public List<Operator> GetOperatorMissing(int? iProgramId)
        {
            return Operator.GetOperatorMissing(iProgramId);
        }

        public int UpdateOperator(Operator oOperator)
        {
            return Operator.UpdateOperator(oOperator);
        }

        public int AddOperator(Operator oOperator)
        {
            return Operator.AddOperator(oOperator);
        }

        public List<Operator> DeleteOperator(int iOperatorId, int? iUserId)
        {
            return Operator.DeleteOperator(iOperatorId, iUserId);
        }

        public bool deleteFile(string nvFilePath, string nvFileName)
        {
            return Operator.deleteFile(nvFilePath, nvFileName);
        }
        #endregion Operator

        #region Activity

        public List<Activity> ActivityInsertUpdate(Activity oActivity, int? iUserId)
        {
            return Activity.ActivityInsertUpdate(oActivity, iUserId);
        }

        #endregion Activity

        #region OperatorsAvailability

        public List<OperatorsAvailability> OperatorsAvailabilityGet(int iOperatorId)
        {
            return OperatorsAvailability.OperatorsAvailabilityGet(iOperatorId);
        }

        public bool OperatorsAvailabilityUpdt(int iOperatorId, List<OperatorsAvailability> lOperatorsAvailability, int? iUserId)
        {
            return OperatorsAvailability.OperatorsAvailabilityUpdt(iOperatorId, lOperatorsAvailability, iUserId);
        }

        #endregion OperatorsAvailability

        #region Program

        public List<Program> ProgramsGet(bool? bProgramAfternoon)
        {
            return Program.ProgramsGet(bProgramAfternoon);
        }

        public int ProgramInsertUpdate(Program oProgram, int? iUserId)
        {
            return Program.ProgramInsertUpdate(oProgram, iUserId);
        }

        public bool ProgramSettingsInsertUpdate(int iProgramId, List<int> lProgramSettings, List<int> lSettingMorning, List<int> lSettingNoon, int? iUserId)
        {
            return Program.ProgramSettingsInsertUpdate(iProgramId, lProgramSettings, lSettingMorning, lSettingNoon, iUserId);
        }

        public List<int> ProgramSettingOrderUpdate(int iProgramId, List<Settings> lSettings)
        {
            return Program.ProgramSettingOrderUpdate(iProgramId, lSettings);
        }

        public List<OperatorActivityDate> ActivityNoSchedulingGet(int? iProgramId)
        {
            return Program.ActivityNoSchedulingGet(iProgramId);
        }

        #endregion Program

        #region Schedule

        public List<Schedule> SchedulesGet(int? iOperatorId, int? iSettingId, int? iProgramId, DateTime? dDate)
        {
            return Schedule.SchedulesGet(iOperatorId, iSettingId, iProgramId, dDate);
        }

        public List<Schedule> ScheduleInsertUpdate(int? iProgramId, int? iUserId)
        {
            return Schedule.ScheduleInsertUpdate(iProgramId, iUserId);
        }

        public List<Schedule> SceduleCloseDayInsertUpdate(int? iOperatorId, DateTime? dDate, bool? bIsActive, bool? bLongDay, int? iUserId)
        {
            return Schedule.SceduleCloseDayInsertUpdate(iOperatorId, dDate, bIsActive, bLongDay, iUserId);
        }

        public bool ScheduleUpdate(int? iScheduleId, int? iOperatorId, int? iActivityId, int? iSettingId, int? iProgramId, DateTime? dtStartTime, bool? bCopyAllWeeks, int? iUserId)
        {
            return Schedule.ScheduleUpdate(iScheduleId, iOperatorId, iActivityId, iSettingId, iProgramId, dtStartTime, bCopyAllWeeks, iUserId);
        }

        public bool ScheduleReplace(int? iScheduleSource, DateTime? dtStartTime, int? iScheduleReplace, bool? bCopyAllWeeks, int? iUserId)
        {
            return Schedule.ScheduleReplace(iScheduleSource, dtStartTime, iScheduleReplace, bCopyAllWeeks, iUserId);
        }

        public bool ScheduleDelete(int? iScheduleId, bool? bCopyAllWeeks, int? iUserId)
        {
            return Schedule.ScheduleDelete(iScheduleId, bCopyAllWeeks, iUserId);
        }

        public List<Schedule> ScheduleAfternoonInsert(int? iProgramId, int? iUserId)
        {
            return Schedule.ScheduleAfternoonInsert(iProgramId, iUserId);
        }

        public List<Schedule> SchedulingByInsert(int? iProgramId, int? iUserId)
        {
            return Schedule.SchedulingByInsert(iProgramId, iUserId);
        }

        #endregion Schedule

        #region IVR

        public int ScheduleCheck(int? iOperatorId, int? iSettingId)
        {
            return IVR.ScheduleCheck(iOperatorId, iSettingId);
        }

        public bool ScheduleTimeUpdate(int? iScheduleId, int? iUserId)
        {
            return IVR.ScheduleTimeUpdate(iScheduleId, iUserId);
        }


        #endregion IVR

        #region ValidSchedule

        public ValidSchedule CheckIsValiidSchedule(int? iScheduleId, int? iOperatorId, DateTime? dtStartTime)
        {
            return ValidSchedule.CheckIsValiidSchedule(iScheduleId, iOperatorId, dtStartTime);
        }

        #endregion ValidSchedule

        #region Message

        public string SendMailsMessage(string nvSubject, string nvBody, List<string> emailAddressesList, string filePath)
        {
            return Message.SendMailsMessage(nvSubject, nvBody, emailAddressesList, filePath);
        }

        public bool SendMultipleSMS(string nvContent, List<string> phoneNumberList)
        {
            return Message.SendMultipleSMS(nvContent, phoneNumberList);
        }

        public string CreatePdfFromContent(string nvHtmlContent, string filePath, string fileName, string style = null)
        {
            return Message.CreatePdfFromContent(nvHtmlContent, filePath, fileName, style);
        }

        #endregion Message

        #region Messages

        public int InsertMessage(List<Messages> lMessage)
        {
            return Messages.InsertMessage(lMessage);
        }

        public List<Messages> GetMessages(int iOperatorId, int iSettingId)
        {
            return Messages.GetMessages(iOperatorId, iSettingId);
        }

        #endregion Messages

        #region Review

        public List<Review> GetReviews(int iOperatorId)
        {
            return Review.GetReviews(iOperatorId);
        }

        public Review InsertReview(Review oReview, int iUserId)
        {
            return Review.InsertReview(oReview, iUserId);
        }

        public string UpdtReview(Review oReview, int iUserId)
        {
            return Review.UpdtReview(oReview, iUserId);
        }

        #endregion Review

        #region Credit

        public List<Credit> GetCredits(int iOperatorId)
        {
            return Credit.GetCredits(iOperatorId);
        }

        public List<ActivityReport> GetActivityReportsByCredit(int iCreditId)
        {
            return Credit.GetActivityReportsByCredit(iCreditId);
        }

        public string UpdateCredit(int iCreditId, string nvFilePathInvoice)
        {
            return Credit.UpdateCredit(iCreditId, nvFilePathInvoice);
        }

        public void InsertCreditsToSummerCamp()
        {
            Credit.InsertCreditsToSummerCamp();
        }

        public void InsertCreditsToAfternoon()
        {
            Credit.InsertCreditsToAfternoon();
        }

        public string CreateReceiptPdf(Credit oCredit)
        {
            return Credit.CreateReceiptPdf(oCredit);
        }


        #endregion Credit

        #region ActivityReport

        public List<ActivityReport> GetActivityRepors(int iOperatorId)
        {
            return ActivityReport.GetActivityRepors(iOperatorId);
        }

        public int ActivityReportUpdt(int iStatusType, int iScheduleId)
        {
            return ActivityReport.ActivityReportUpdt(iStatusType, iScheduleId);
        }

        #endregion ActivityReport

        #region ExcelHandler

        public string ExportToExcel(DataTable dt, string sFileName, string[] lColumns)
        {
            return ExcelHendler.ExportToExcel(dt, sFileName, lColumns);
        }

        #endregion ExcelHandler
    }
}
