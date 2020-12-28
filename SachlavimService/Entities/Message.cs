using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using NReco.PdfGenerator;
using SachlavimService.Utilities;

namespace SachlavimService.Entities
{
    public class Message
    {
        public static string SendMailsMessage(string nvSubject, string nvBody, List<string> emailAddressesList, string filePath)
        {
            try
            {
                int i = 0; int j = 0;
                int minutesToSleep = 3 * 60 * 1000;
                string file = "";
                bool isSuccess = true;
                if (filePath != null && filePath!="")
                {
                    string timeStmp = DateTime.Now.Ticks.ToString();
                    file = Global.SaveFile("Send" + timeStmp, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] +
                       "Send\\", filePath.Substring(0, filePath.IndexOf("+++**")), filePath.Substring(filePath.IndexOf("+++**") + 5, filePath.Length - (filePath.IndexOf("+++**") + 5)));
                }
                while (i < emailAddressesList.Count)
                {
                    for (j = 0; j < 100 && i < emailAddressesList.Count; j++, i++)
                        isSuccess = isSuccess && SendMailsWithAttachedFiles(nvSubject, nvBody, emailAddressesList[i], file);
                    if (i < emailAddressesList.Count)
                        Thread.Sleep(minutesToSleep);
                }
                if (isSuccess && file=="")
                    return "true";
                if (isSuccess)
                    return file;
                return "";
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SendMailsMessage Emails", ex);
                return "";
            }
        }

        public static bool SendMultipleSMS(string nvContent, List<string> phoneNumberList)
        {
            try
            {
                string nvSenderNumber = ConfigurationManager.AppSettings["NumberFrom"].ToString();
                bool isSuccess = true;
                foreach (string nvPhoneNumber in phoneNumberList)
                {
                    string result = SendSMS(nvSenderNumber, nvContent, nvPhoneNumber);
                    isSuccess = isSuccess && result != null;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SendMultipleSMS", ex);
                return false;
            }
        }

        public static string SendSMS(string nvSenderNumber, string nvContent, string nvRecipientNumber)
        {
            try
            {
                if (nvRecipientNumber.Contains('-'))
                    nvRecipientNumber = nvRecipientNumber.Replace("-", string.Empty);
                if (nvSenderNumber.Contains('-'))
                    nvSenderNumber = nvSenderNumber.Replace("-", string.Empty);

                StringBuilder sbXml = new StringBuilder();
                sbXml.Append("<Inforu>");
                sbXml.Append("<User>");
                sbXml.Append("<Username>" + "webit" + "</Username>");
                sbXml.Append("<Password>" + "2222953 " + "</Password>");
                sbXml.Append("</User>");
                sbXml.Append("<Content Type=\"sms\">");
                sbXml.Append("<Message>" + "<![CDATA[" + nvContent + "]]>" + "</Message>");
                sbXml.Append("</Content>");
                sbXml.Append("<Recipients>");
                sbXml.Append("<PhoneNumber>" + nvRecipientNumber + "</PhoneNumber>");
                sbXml.Append("</Recipients>");
                sbXml.Append("<Settings>");
                sbXml.Append("<SenderNumber>" + nvSenderNumber + "</SenderNumber>");
                sbXml.Append("<MessageInterval>" + 0 + "</MessageInterval>");
                sbXml.Append("</Settings>");
                sbXml.Append("</Inforu>");
                string strXML = HttpUtility.UrlEncode(sbXml.ToString(), System.Text.Encoding.UTF8);
                string result = PostDataToURL("http://api.inforu.co.il/SendMessageXml.ashx", "InforuXML=" + strXML);
                return result;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SendSMS", ex);
                //throw;
                return null;
            }
        }

        //public static int InsertMessage(string nvSenderNumber, Message message, int iUserId)
        public static int InsertMessage(string nvSenderNumber, string nvContent, string nvRecipientNumber, int iUserId)
        {
            try
            {
                SqlParameter[] param = {new SqlParameter("nvRecipientNumber",nvRecipientNumber),
                                        new SqlParameter("nvSenderNumber",nvSenderNumber),
                                        new SqlParameter("nvContent", nvContent),
                                        new SqlParameter("iUserId",iUserId) };

                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TMessage_INS", param);
                int iMessageId = Convert.ToInt32(ds.Tables[0].Rows[0]["iMessageId"]);
                return iMessageId;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("InsertMessage", ex);
                return -1;
            }
        }

        public static bool SendMailWithAttachedPdf(string subject, string body, string nvRequestUserEmail, string filePath)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["mailFrom"].ToString());
                message.Bcc.Add("yafi.p@webit-sys.com");
                string[] address = nvRequestUserEmail.Split(',');
                foreach (string item in address)
                {
                    if (item != null && item != "")
                        //   message.To.Add(new MailAddress(item));
                        message.Bcc.Add(new MailAddress(item));
                }
                message.Subject = subject != "" && subject != null ? subject : "בר אילן רישום מנויים";
                message.IsBodyHtml = true;
                message.Body = body;
                //message.Attachments.Add(new Attachment(AppDomain.CurrentDomain.BaseDirectory + "Files\\" + fileName));
                if (filePath != "")
                    message.Attachments.Add(new Attachment(filePath));
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Send(message);
                }
                //File.Delete(System.Configuration.ConfigurationManager.AppSettings["PdfFile"] + sFileName);
                message.Attachments.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SendMailWithAttachedPdf", ex);
                return false;
            }
        }

        public static string PostDataToURL(string szUrl, string szData)
        {
            //Setup the web request
            string szResult = string.Empty;
            WebRequest Request = WebRequest.Create(szUrl);
            Request.Timeout = 20000;
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            //Set the POST data in a buffer
            byte[] PostBuffer;
            try
            {
                // replacing " " with "+" according to Http post RPC
                szData = szData.Replace(" ", "+");
                //Specify the length of the buffer
                PostBuffer = Encoding.UTF8.GetBytes(szData);
                Request.ContentLength = PostBuffer.Length;
                //Open up a request stream
                Stream RequestStream = Request.GetRequestStream();
                //Write the POST data
                RequestStream.Write(PostBuffer, 0, PostBuffer.Length);
                //Close the stream
                RequestStream.Close();
                //Create the Response object
                WebResponse Response;
                Response = Request.GetResponse();
                //Create the reader for the response
                StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
                //Read the response
                szResult = sr.ReadToEnd();
                //Close the reader, and response
                sr.Close();
                Response.Close();
                return szResult;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog("PostDataToURL", e);
                return szResult;
            }
        }

        public static bool SendMailsWithAttachedFiles(string nvSubject, string nvBody, string nvRequestUserEmail, string filesPath)
        {
            try
            {
                MailMessage message = new MailMessage();
                //message.From = new MailAddress(ConfigurationManager.AppSettings["mailFrom"].ToString());
                message.From = new MailAddress( "sachlavimodiin08@gmail.com");
                //message.Bcc.Add(new MailAddress(nvRequestUserEmail));
                message.To.Add(nvRequestUserEmail);
                message.Body = nvBody;

                message.Subject = nvSubject != "" && nvSubject != null ? nvSubject : "סחלבים";
                message.IsBodyHtml = true;
                if (filesPath != null && filesPath != "")
                    message.Attachments.Add(new Attachment(ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Send\\" + filesPath));
                using (SmtpClient smtpClient = new SmtpClient())
                {
                                        smtpClient.UseDefaultCredentials = false;

                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    //Set the Email of who you sent the message from

                    smtpClient.Credentials = new System.Net.NetworkCredential("sachlavimodiin08@gmail.com", "yael0533");

                    smtpClient.Send(message);
                }
                message.Attachments.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SendMailsWithAttachedFiles", ex);
                throw;
            }
        }

        public static string CreatePdfFromContent(string nvHtmlContent, string filePath, string fileName, string style = null)
        {
            try
            {
                NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdf.Margins = new NReco.PdfGenerator.PageMargins();
                pdf.Margins.Top = 1;
                pdf.Margins.Bottom = 1;
                pdf.Margins.Right = 1;
                pdf.Margins.Left = 1;
                pdf.Size = PageSize.A4;
                fileName += ".pdf";
                string body = @"<html lang=""heb""><head>
                                               <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type""> 
                                               <style type=""text/css"">
                                                body {
                                                   font-family:Arial;
                                                   font-size:14px;
                                                   direction:rtl;
                                                    }";
                body += style;
                body += @"</style></head><body><div dir=""rtl"" style=""padding-right:25px;padding-top:40px;"">" + nvHtmlContent + "</div>";
                body += " </body></html>";

                var pdfBytes = pdf.GeneratePdf(body, null);
                File.WriteAllBytes(filePath + fileName, pdfBytes);

                return filePath + fileName;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("CreatePdfFromContent",ex);
                return null;
            }
        }
    }
}