using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SachlavimService.Utilities
{
    public class NotificationHandler
    {

        public static bool SendMail(string[] lRecipients, string[] lCopies, string nvSenderEmail, string nvSubject, string nvContent)
        {
            using (SmtpClient smtpServer = new SmtpClient())
            {
                try
                {
                    MailMessage oMail = new MailMessage();
                    oMail.From = new MailAddress(nvSenderEmail);
                    foreach (var item in lRecipients)
                        oMail.To.Add(item);
                    foreach (var item in lCopies)
                        oMail.CC.Add(item);
                    oMail.Subject = nvSubject;
                    oMail.BodyEncoding = Encoding.UTF8;
                    oMail.IsBodyHtml = true;
                    oMail.Body = "<html><head><meta charset='utf-8' /></head>" +
                    "<body><div style='text-align:right;' dir='rtl'>" + nvContent + "</div></body></html>"; ;
                    smtpServer.Send(oMail);
                    return true;
                }
                catch (Exception ex)
                {
                    LogWriter.WriteLog("SendMail", ex);
                    return false;
                }
            }
        }

        public static void SendEmailOrFax(string sFrom, string[] sTo, string sSubject, string sBody, List<Attachment> lAttach)
        {
            using (SmtpClient smtpServer = new SmtpClient())
            {
                try
                {
                    MailMessage oMail = new MailMessage();
                    oMail.From = new MailAddress(sFrom);
                    foreach (var item in sTo)
                        oMail.To.Add(item);
                    oMail.Subject = sSubject;
                    oMail.IsBodyHtml = true;
                    if (lAttach != null)
                        foreach (var item in lAttach)
                            oMail.Attachments.Add(item);
                    oMail.Body = "<div style='direction:rtl;text-align:right'>" + sBody + "</div>";
                    //. smtpServer.SendAsync(mailObj,null);
                    smtpServer.Send(oMail);
                }
                catch (Exception ex)
                {
                    LogWriter.WriteLog("SendEmailOrFax", ex);
                }
            }
        }

        public static string SendSMS(string sRecipientNumber, string sMessageText, string sSenderNumber)
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<Inforu>");
            sbXml.Append("<User>");
            sbXml.Append("<Username>" + "webit" + "</Username>");
            sbXml.Append("<Password>" + "2222953 " + "</Password>");
            sbXml.Append("</User>");
            sbXml.Append("<Content Type=\"sms\">");
            sbXml.Append("<Message>" + "<![CDATA[" + sMessageText + "]]>" + "</Message>");
            sbXml.Append("</Content>");
            sbXml.Append("<Recipients>");
            sbXml.Append("<PhoneNumber>" + sRecipientNumber + "</PhoneNumber>");
            sbXml.Append("</Recipients>");
            sbXml.Append("<Settings>");
            sbXml.Append("<SenderNumber>" + sSenderNumber + "</SenderNumber>");
            sbXml.Append("<MessageInterval>" + 0 + "</MessageInterval>");
            sbXml.Append("</Settings>");
            sbXml.Append("</Inforu>");
            string strXML = HttpUtility.UrlEncode(sbXml.ToString(), System.Text.Encoding.UTF8);
            string result = PostDataToURL("http://api.inforu.co.il/SendMessageXml.ashx", "InforuXML=" + strXML);
            return result;
        }

        private static string PostDataToURL(string szUrl, string szData)
        {
            string szResult = string.Empty;
            WebRequest Request = WebRequest.Create(szUrl);
            Request.Timeout = 30000;
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            byte[] PostBuffer;
            try
            {
                szData = szData.Replace(" ", "+");
                PostBuffer = Encoding.UTF8.GetBytes(szData);
                Request.ContentLength = PostBuffer.Length;
                Stream RequestStream = Request.GetRequestStream();
                RequestStream.Write(PostBuffer, 0, PostBuffer.Length);
                RequestStream.Close();
                WebResponse Response;
                Response = Request.GetResponse();
                StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
                szResult = sr.ReadToEnd();
                sr.Close();
                Response.Close();
                return szResult;
            }
            catch (Exception)
            {
                return szResult;
            }
        }
    }
}