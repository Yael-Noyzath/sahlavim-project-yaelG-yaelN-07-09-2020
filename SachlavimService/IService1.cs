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
    [ServiceContract]
    public interface IService1
    {
        #region User

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "UserLogin",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        User UserLogin(string nvUserName, string nvPassword, string nvMail);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetUsers",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<User> GetUsers();

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "AddUpdateUser",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        User AddUpdateUser(User oUser);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "UserReset",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        User UserReset(string nvMail);

        #endregion User

        #region SysTable

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SysTableListGet",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        SysTableContent[] SysTableListGet();

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetSysTableContent",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Dictionary_IntString> GetSysTableContent(int id);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "AddUpdateSysTableRow",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        Dictionary_IntString AddUpdateSysTableRow(Dictionary_IntString SysTableRow, int iSysTableId, int iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SysRowCheck",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int? SysRowCheck(int iSysTableRowId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SysRowDelete",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool? SysRowDelete(int iSysTableRowId, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "UpdateNewJob",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        void UpdateNewJob();

        #endregion SysTable

        #region Settings

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SettingsGet",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Settings> SettingsGet();


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetSettingByPhone",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int? GetSettingByPhone(string nvPhone);


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "DistanceSettings",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Settings> DistanceSettings(List<Settings> lSettings);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SettingInsertUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int SettingInsertUpdate(Settings oSetting, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetSettingsSchedule",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Settings> GetSettingsSchedule(int? iProgramId, int? iSettingId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "CreateSettingsSchedulePDF",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        string CreateSettingsSchedulePDF(int? iProgramId);

        #endregion Settings

        #region SettingsJoint

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetSettingsJoint",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<SettingsJoint> GetSettingsJoint();

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SettingsJointInsertUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<SettingsJoint> SettingsJointInsertUpdate(int? iSettingJointId, int? iSettingId1, int? iSettingId2, int? iUserId);

        #endregion SettingsJoint

        #region SettingsClusters

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SettingsClustersSelect",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<SettingsClusters> SettingsClustersSelect();


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SettingsClustersInsertUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<SettingsClusters> SettingsClustersInsertUpdate(SettingsClusters oSettingsClusters, int? iUserId);


        #endregion SettingsClusters

        #region Coordinator

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "CoordinatorsGet",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Coordinator> CoordinatorsGet();

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "CoordinatorInsertUpdt",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Coordinator> CoordinatorInsertUpdt(Coordinator oCoordinator, int? iUserId);

        #endregion Coordinator

        #region Operator

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetOperator",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        Operator GetOperator(int iOperatorId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetOperators",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Operator> GetOperators();

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetOperatorsByDay",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Operator> GetOperatorsByDay(DateTime? dDate);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetOperatorMissing",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Operator> GetOperatorMissing(int? iProgramId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "UpdateOperator",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int UpdateOperator(Operator oOperator);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "AddOperator",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int AddOperator(Operator oOperator);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "DeleteOperator",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Operator> DeleteOperator(int iOperatorId, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "deleteFile",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool deleteFile(string nvFilePath, string nvFileName);

        #endregion Operator

        #region Activity

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ActivityInsertUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Activity> ActivityInsertUpdate(Activity oActivity, int? iUserId);

        #endregion Activity

        #region OperatorsAvailability


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "OperatorsAvailabilityGet",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<OperatorsAvailability> OperatorsAvailabilityGet(int iOperatorId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "OperatorsAvailabilityUpdt",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool OperatorsAvailabilityUpdt(int iOperatorId, List<OperatorsAvailability> lOperatorsAvailability, int? iUserId);

        #endregion OperatorsAvailability

        #region Program

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ProgramsGet",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Program> ProgramsGet(bool? bProgramAfternoon);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ProgramInsertUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int ProgramInsertUpdate(Program oProgram, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ProgramSettingsInsertUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool ProgramSettingsInsertUpdate(int iProgramId, List<int> lProgramSettings, List<int> lSettingMorning, List<int> lSettingNoon, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ProgramSettingOrderUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<int> ProgramSettingOrderUpdate(int iProgramId, List<Settings> lSettings);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ActivityNoSchedulingGet",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<OperatorActivityDate> ActivityNoSchedulingGet(int? iProgramId);

        #endregion Program

        #region Message

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SendMailsMessage",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        string SendMailsMessage(string nvSubject, string nvBody, List<string> emailAddressesList, string filePath);


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SendMultipleSMS",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool SendMultipleSMS(string nvContent, List<string> phoneNumberList);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "CreatePdfFromContent",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        string CreatePdfFromContent(string nvHtmlContent, string filePath, string fileName, string style = null);

        #endregion Message

        #region Schedule

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SchedulesGet",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Schedule> SchedulesGet(int? iOperatorId, int? iSettingId, int? iProgramId, DateTime? dDate);


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ScheduleInsertUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Schedule> ScheduleInsertUpdate(int? iProgramId, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SceduleCloseDayInsertUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Schedule> SceduleCloseDayInsertUpdate(int? iOperatorId, DateTime? dDate, bool? bIsActive, bool? bLongDay, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ScheduleUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool ScheduleUpdate(int? iScheduleId, int? iOperatorId, int? iActivityId, int? iSettingId, int? iProgramId, DateTime? dtStartTime, bool? bCopyAllWeeks, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ScheduleReplace",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool ScheduleReplace(int? iScheduleSource, DateTime? dtStartTime, int? iScheduleReplace, bool? bCopyAllWeeks, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ScheduleDelete",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool ScheduleDelete(int? iScheduleId, bool? bCopyAllWeeks, int? iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ScheduleAfternoonInsert",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Schedule> ScheduleAfternoonInsert(int? iProgramId, int? iUserId);


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "SchedulingByInsert",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Schedule> SchedulingByInsert(int? iProgramId, int? iUserId);

        #endregion Schedule

        #region IVR

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ScheduleCheck",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int ScheduleCheck(int? iOperatorId, int? iSettingId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ScheduleTimeUpdate",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        bool ScheduleTimeUpdate(int? iScheduleId, int? iUserId);

        #endregion IVR

        #region ValidSchedule

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "CheckIsValiidSchedule",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        ValidSchedule CheckIsValiidSchedule(int? iScheduleId, int? iOperatorId, DateTime? dtStartTime);

        #endregion ValidSchedule

        #region Messages

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "InsertMessage",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int InsertMessage(List<Messages> lMessage);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetMessages",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Messages> GetMessages(int iOperatorId, int iSettingId);

        #endregion Messages

        #region Review

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetReviews",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Review> GetReviews(int iOperatorId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "InsertReview",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        Review InsertReview(Review oReview, int iUserId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "UpdtReview",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        string UpdtReview(Review oReview, int iUserId);

        #endregion Review

        #region Credit

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetCredits",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<Credit> GetCredits(int iOperatorId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetActivityReportsByCredit",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<ActivityReport> GetActivityReportsByCredit(int iCreditId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "UpdateCredit",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        string UpdateCredit(int iCreditId, string nvFilePathInvoice);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "InsertCreditsToSummerCamp",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        void InsertCreditsToSummerCamp();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "InsertCreditsToAfternoon",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        void InsertCreditsToAfternoon();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "CreateReceiptPdf",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        string CreateReceiptPdf(Credit oCredit);

        #endregion Credit

        #region ActivityReport

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "GetActivityRepors",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        List<ActivityReport> GetActivityRepors(int iOperatorId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ActivityReportUpdt",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        int ActivityReportUpdt(int iStatusType, int iScheduleId);

        #endregion ActivityReport

        #region ExcelHandler

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "ExportToExcel",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json)]
        string ExportToExcel(DataTable dt, string sFileName, string[] lColumns);

        #endregion ExcelHandler

    }
}
