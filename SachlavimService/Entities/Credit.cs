using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Web;
using SachlavimService.Utilities;

namespace SachlavimService.Entities
{
    public class Credit
    {
        #region Members

        [DataMember]
        public int? iCreditId { get; set; }
        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public int? iProgramId { get; set; }
        [DataMember]
        public DateTime? dCreditDate { get; set; }
        [DataMember]
        public int? nNumActivity { get; set; }
        [DataMember]
        public int? iPaymentNum { get; set; }
        [DataMember]
        public int? iPaymentMethodId { get; set; }
        [DataMember]
        public string nvFilePathReceipt { get; set; }
        [DataMember]
        public string nvFilePathInvoice { get; set; }
        [DataMember]
        [NoSendToSQL]
        public string nvProgramName { get; set; }
        [DataMember]
        [NoSendToSQL]
        public string nvBudgetItem { get; set; }

        #endregion Members

        #region Methods

        public static void InsertCreditsToSummerCamp()
        {
            LogWriter.WriteLog("InsertCreditsToSummerCamp", null);
            var th = new Thread(InsertCreditsToSummerCampThread);
            th.Start();
            Thread.Sleep(5000);
        }

        private static void InsertCreditsToSummerCampThread()
        {
            try
            {
                List<Program> lProgram = Program.ProgramsGet(false);
                for (int i = 0; i < lProgram.Count; i++)
                {
                    if (lProgram[i].dToDate.Value.Date == DateTime.Now.Date)
                    {
                        DataSet ds = SqlDataAccess.ExecuteDatasetSP("TCredit_INS", new SqlParameter("iProgramId", lProgram[i].iProgramId));
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("InsertCreditsToSummerCampThread", ex);
            }
        }

        public static void InsertCreditsToAfternoon()
        {
            LogWriter.WriteLog("InsertCreditsToAfternoon", null);
            var th = new Thread(InsertCreditsToAfternoonThread);
            th.Start();
            Thread.Sleep(5000);
        }

        private static void InsertCreditsToAfternoonThread()
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TCreditAfternoon_INS");
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("InsertCreditsToAfternoonThread", ex);
            }
        }

        public static List<Credit> GetCredits(int iOperatorId)
        {
            try
            {
                List<Credit> lCredit = new List<Credit>();
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TCredits_Slct" ,new SqlParameter("iOperatorId",iOperatorId));
                if (ds.Tables.Count > 0)
                    lCredit = ObjectGenerator<Credit>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                
                return lCredit;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetCredits", ex);
                return null;
            }
        }

        public static List<ActivityReport> GetActivityReportsByCredit(int iCreditId)
        {
            try
            {
                List<ActivityReport> lActivityReport = new List<ActivityReport>();
                List<SqlParameter> lParam = new List<SqlParameter>();
                lParam.Add(new SqlParameter("iCreditId", iCreditId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TActivityReportsByCredit_Slct", lParam);
                if (ds.Tables.Count > 0)
                    lActivityReport = ObjectGenerator<ActivityReport>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                return lActivityReport;
            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("GetActivityReportsByCredit", ex);
                return null;
            }
        }
                                                                         
        public static string UpdateCredit(int iCreditId, string nvFilePathInvoice)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                if (nvFilePathInvoice != null && nvFilePathInvoice != "" && nvFilePathInvoice.IndexOf("+++**") != -1)
                {
                    string file = nvFilePathInvoice;
                    string timeStmp = DateTime.Now.Ticks.ToString();
                    Global.SaveFile("Invoice" + iCreditId + "_" + timeStmp, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Invoice\\", file.Substring(0, file.IndexOf("+++**")), file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5)));
                    nvFilePathInvoice = "Invoice" + iCreditId + "_" + timeStmp + "." + file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5));
                }
                parameters.Add(new SqlParameter("iCreditId", iCreditId));
                parameters.Add(new SqlParameter("nvFilePathInvoice", nvFilePathInvoice));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TCredit_UPD", parameters);
                return nvFilePathInvoice;
            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("UpdateCredit", ex);
                return "";
            }
        }

        public static string CreateReceiptPdf(Credit oCredit)
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TDetailsForReceipt_SLCT", new SqlParameter("iCreditId", oCredit.iCreditId));
                string nvOperatorNumber = ds.Tables[0].Rows[0]["nvOperatorNumber"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["nvOperatorNumber"].ToString():"";
                string iNumBookkeeping = ds.Tables[0].Rows[0]["iNumBookkeeping"].ToString() != string.Empty ?ds.Tables[0].Rows[0]["iNumBookkeeping"].ToString(): "";
                string nvCompanyName = ds.Tables[0].Rows[0]["nvCompanyName"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["nvCompanyName"].ToString():"";
                string iPaymentNum = ds.Tables[0].Rows[0]["iPaymentNum"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["iPaymentNum"].ToString() : "";
                string nvBudgetItem = ds.Tables[0].Rows[0]["nvBudgetItem"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["nvBudgetItem"].ToString():"";
                string iReceiptNumber = ds.Tables[0].Rows[0]["iReceiptNumber"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["iReceiptNumber"].ToString() : "";

                string style = @" body {direction: rtl;font-family:Arial;}
        .clear{clear:both;}
        table {border-collapse: collapse;}
        table, th, td {border: 1px solid black;height: 24px;}
        .line {border-top: 1px solid black; padding-top: 0px;padding-bottom: 34px;display: inline-block;width: 232px;text-align: center;
            margin-right: 25px;}
        .line-bottom {border-bottom: 1px solid black;padding-bottom: 5px;display: inline-block;width: calc(100% - (72px));}
        table {border-bottom-color: white;border-right-color: white;}
        .border-white {border: white}";
                string nvHtmlContent = @"<div style='width: 80%;padding-right: 10%;'>
    <div style='float:left' ><img src='http://ws.webit-track.com/Sachlavim/Files/logo.png'/></div>
<div class='clear'></div>
        <div style='text-align: left;margin-bottom: 21px;'>
            <div>
                <span> פקודת תשלום מס'" + (Convert.ToInt16(iReceiptNumber)) + @"</span>
                <span style='width: 44%;display: inline-block;'> ע.ר 580470235</span>
            </div>
            <div>
                <span> מקור</span>
            </div>
        </div>
        <div style='width:40%;float:left'>
            <div style='width:266px;margin-bottom: 10px;'>
                <span> מספר ספק</span>
                <label class='line-bottom' style='width: 190px;'> " + nvOperatorNumber + @"</label>
            </div>
            <div style='width: 270px;'>
                <span> נרשם במנה הנ'ח מס'</span>
                <label class='line-bottom' style='width: 134px;'>" + iNumBookkeeping + @"</label>
            </div>
        </div>
        <div class='clear'></div>
        <div style='width:100%;float:right'>
            <div style='width: 297px;margin-bottom: 10px;'>
                <span> נא לשלם לפקודת</span>
                <label class='line-bottom' style='width: 173px;'>" + nvCompanyName + @"</label>
            </div>
            <div style='width: 283px;'>
                <span>כתובת</span>
                <label class='line-bottom' style='width: 229px;'></label>
            </div>
        </div>
        <div class='clear'></div>
        <div>
            <table style='width:100%;margin-top: 38px;'>
                <tr>
                    <td style='border-left:white'></td>
                    <td class='border-white'>פרטים</td>
                    <td style='border-right: white;'> </td>
                    <td class='border-white'></td>
                    <td class='border-white' style='text-align:right'>סכום</td>
                </tr>
                <tr>
                    <th>חשבונית מס'</th>
                    <th>תאריך</th>
                    <th>פרוט ההוצאות</th>
                    <th>אג'</th>
                    <th>שקלים</th>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>" + iPaymentNum + @"</td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class='border-white'></td>
                    <td class='border-white'></td>
                    <td class='border-white' style='text-align: left;padding-left:10px'>סה'כ</td>
                    <td></td>
                    <td>" + iPaymentNum + @"</td>
                </tr>
            </table>
        </div>
        <div style='width:100%'>
            <table style='width:60%;float:right;margin-top: 38px;'>
                <tr>
                    <td style='text-align:center'>לחובות סעיף תקציבי</td>
                    <td class='border-white'></td>
                    <td class='border-white' style='text-align:right'>סכום</td>
                </tr>
                <tr>
                    <th>מס' סעיף</th>
                    <th>אג'</th>
                    <th>שקלים</th>
                </tr>
                <tr>
                    <td>" + nvBudgetItem + @"</td>
                    <td></td>
                    <td>" + iPaymentNum + @"</td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class='border-white' style='text-align: left;padding-left:10px'>סה'כ</td>
                    <td></td>
                    <td>" + iPaymentNum + @"</td>
                </tr>
            </table>
            <div style='width:40%;float:left;margin-top: 60px;'>
                <div><span class='line'> חתימת הרכז/מנהל מח'</span></div>

                <div>
                    <span class='line'>חתימת מנהל כספים</span>
                </div>
                <div>
                    <span class='line'> חתימת מנהל העמותה</span>
                </div>
                <div> <span class='line'> שולם בצי'ק מספר</span></div>
            </div>
            <div class='clear'></div>
        </div>
    </div>";
                string timeStmp = DateTime.Now.Ticks.ToString();
                string sFileName = "Receipt" + oCredit.iCreditId+"_"+ timeStmp;
                string filePath = ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Receipt" + "\\";
                string linkPdf = Message.CreatePdfFromContent(nvHtmlContent, filePath, sFileName, style);
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("nvFilePathReceipt", sFileName + ".pdf"));
                param.Add(new SqlParameter("iCreditId", oCredit.iCreditId));
                SqlDataAccess.ExecuteDatasetSP("TnvFilePathReceipt_UPD", param);
                return sFileName + ".pdf";
            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("CreateReceiptPdf", ex);
                return "";
            }
        }

        #endregion Methods                                                
    }                                                                    
}