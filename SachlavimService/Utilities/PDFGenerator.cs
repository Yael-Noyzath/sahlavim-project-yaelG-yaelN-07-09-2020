
using SachlavimService.Utilities;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Net.Mail;

using System.Configuration;


namespace SachlavimService.Utilities
{
    public class PDFGenerator
    {
        public static string CreatePdfToFax(string html, string title, string iCode)
        {
            try
            {

                NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdf.Margins = new NReco.PdfGenerator.PageMargins();
                pdf.Margins.Top = 15;
                pdf.Margins.Bottom = 5;
                pdf.Margins.Right = 1;
                pdf.Margins.Left = 1;
                pdf.Size = PageSize.A4;
                pdf.PageFooterHtml = "<div style='text-align:center;'><span class='page' style='border-left:1px solid #ddbfd3;padding-left:10px'></span> / <span style='border-right:1px solid #ddbfd3;padding-right:10px' class='topage'></span></div>";
                pdf.PageHeaderHtml = "<div ><div style='text-align:right;'>בסיעתא דשמיא</div><div style='text-align:center;font-size:40px;'>" + title + "</div></div>";
                html = "<html><head><meta charset='utf-8' /><style type='text/css'>table tr {width:100%;text-align:center;}table {width:100%}    table th { text-align: center;font-size: large; }</style>" +
                   "</head><body><div  style='text-align:right;' dir='rtl'>" + html + "</div></body></html>";
                byte[] Bytes = Encoding.UTF8.GetBytes(html);
                string dirPdfPages = AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + "PdfFax\\";

                if (!Directory.Exists(dirPdfPages))
                {
                    Directory.CreateDirectory(dirPdfPages);
                }


                string html1 = iCode.ToString() + DateTime.Now.Ticks.ToString() + ".html";


                File.WriteAllBytes(dirPdfPages + html1, Bytes);

                var pdfBytes = pdf.GeneratePdfFromFile(dirPdfPages + html1, null);
                File.WriteAllBytes(dirPdfPages + html1, pdfBytes);

                return dirPdfPages + html1;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("CreatePdfToFax", ex);
                return null;
            }

        }
        //public static string CreatePDF()
        //{
        //    string sFileName = "חוזה";
        //    try
        //    {
        //        NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
        //        pdf.Margins = new NReco.PdfGenerator.PageMargins();
        //        pdf.Margins.Top = 1;
        //        pdf.Margins.Bottom = 1;
        //        pdf.Margins.Right = 1;
        //        pdf.Margins.Left = 1;
        //        pdf.Size = PageSize.A4;
        //        var pdfBytes = pdf.GeneratePdf(body, null);
        //        sFileName += DateTime.Now.ToFileTime().ToString() + ".pdf";
        //        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + sFileName, pdfBytes);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public static string GeneratePdf(string title, string html, string css)
        {
            try
            {
                NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdf.Margins = new NReco.PdfGenerator.PageMargins();
                pdf.Margins.Top = 15;
                pdf.Margins.Bottom = 5;
                pdf.Margins.Right = 5;
                pdf.Margins.Left = 5;
                pdf.Size = PageSize.A4;
                pdf.PageFooterHtml = "<div style='text-align:center;'><span class='page' style='border-left:1px solid #ddbfd3;padding-left:10px'></span> / <span style='border-right:1px solid #ddbfd3;padding-right:10px' class='topage'></span></div>";
                pdf.PageHeaderHtml = "<div><div style='text-align:center; font-size:20px;'>" + title + "</div></div>";
                html = "<html><head><meta charset='utf-8' /><style type='text/css'>" + css + "</style></head>" +
                    "<body><div style='text-align:right;' dir='rtl'>" + html + "</div></body></html>";

                string sFileName = "חוזה" + DateTime.Now.ToFileTime().ToString() + ".html";

                byte[] htmlBytes = Encoding.UTF8.GetBytes(html);
                // File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + ConfigSettings.ReadSetting("FilesFolderPath") + "pdfGenerator.html", htmlBytes);
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + "pdfGenerator.html", htmlBytes);
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + sFileName, htmlBytes);
                // byte[] pdfBytes = pdf.GeneratePdfFromFile(AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + sFileName, null);
                byte[] pdfBytes = pdf.GeneratePdfFromFile(AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + "pdfGenerator.html", null);

                // if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + sFileName))//delete html file
                // System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + sFileName);

                return Convert.ToBase64String(pdfBytes);
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GeneratePdf", ex);
                return null;
            }
        }

        public static string reportFilesFolder = ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] +
                   System.Configuration.ConfigurationManager.AppSettings["Reports"];

        public static string GeneratePdfFromHtml(string title, string html, string css, string sFileName)
        {
            try
            {
                //Log.ExceptionLog("1", "GeneratePdfFromHtml");
                NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdf.Margins = new NReco.PdfGenerator.PageMargins();
                pdf.Margins.Top = 10;
                pdf.Margins.Bottom = 10;
                pdf.Margins.Right = 10;
                pdf.Margins.Left = 10;
                pdf.Size = PageSize.A4;
                if (sFileName != "נוכחות")
                    pdf.Orientation = PageOrientation.Landscape;

                //pdf.CustomWkHtmlTocArgs = "--toc-disable-back-links";
                //pdf.CustomWkHtmlPageArgs = "--header-html";
                //pdf.Size = PageSize.A4;
                //Log.ExceptionLog("2", "GeneratePdfFromHtml");
                string hostName = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.Host + System.Web.Hosting.HostingEnvironment.ApplicationHost.GetVirtualPath();
                css = css.Replace("../", hostName + "/");
                pdf.PageFooterHtml = "<div style='text-align:center;'><span class='page' style='border-left:1px solid #ddbfd3 ;padding-left:10px'></span> / <span style='border-right:1px solid #ddbfd3 ;padding-right:10px' class='topage'></span></div>";
                pdf.PageHeaderHtml = "<div><div style='text-align:center; font-size:20px; color: #06416D ; font-family: Arial'>" + title + "</div></div>";
                html = "<html><head><meta charset='utf-8' /><style type='text/css'>" + css + "</style></head>" +
                    "<body><div style='text-align:right;' dir='rtl'>" + html + "</div></body></html>";
                string fileName = sFileName.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "") + "_" + DateTime.Now.ToFileTime();
                sFileName += "_" + DateTime.Now.ToFileTime().ToString() + ".pdf";
                byte[] Bytes = Encoding.UTF8.GetBytes(html);

                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\Files1\\pdfGenerator.html", Bytes);
                //Log.ExceptionLog("file path: ", AppDomain.CurrentDomain.BaseDirectory + "Files1\\" + fileName + ".html");
                var pdfBytes = pdf.GeneratePdfFromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Files1\\pdfGenerator.html", null);
                //Log.ExceptionLog("3", "GeneratePdfFromHtml");
                string dir = reportFilesFolder;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllBytes(dir + sFileName, pdfBytes);
                //Log.ExceptionLog("4", "GeneratePdfFromHtml");
                File.WriteAllBytes(dir + fileName + ".pdf", pdfBytes);

                return fileName + ".pdf";
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GeneratePdfFromHtml", ex);
                return null;
            }
        }
        public static bool SendFaxWithAttachment(string faxTo, string mailFrom, string subject, string body, string pdfFile)
        {
            try
            {
                MailAddress from = new MailAddress(mailFrom);
                MailAddress to = new MailAddress(faxTo + "@myfax.co.il");
                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;

                string fullPahth = pdfFile;
                System.IO.FileInfo filenamex = new System.IO.FileInfo(fullPahth);
                Attachment att = new Attachment(filenamex.FullName);
                att.Name = "Ganim.pdf";
                message.Attachments.Add(att);

                SmtpClient client = new SmtpClient();
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SendMailWithAttachment", ex);
                return false;
            }

        }
        public static string GetHtmlContent(string htmlUrl)
        {
            string res = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(htmlUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                res = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return res;
        }

        //public static string CreatePdf(string url, string sFileName, string directory = null)
        //{
        //    try
        //    {
        //        string dir = string.IsNullOrEmpty(directory) ? reportFilesFolder : directory;
        //        Log.ExceptionLog("PDF_URL", url);
        //        NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
        //        pdf.Margins = new NReco.PdfGenerator.PageMargins();
        //        pdf.Margins.Top = 1;
        //        pdf.Margins.Bottom = 5;
        //        pdf.Margins.Right = 1;
        //        pdf.Margins.Left = 1;
        //        pdf.Size = PageSize.A4;
        //        pdf.PageFooterHtml = "<div style='text-align:center;'><span class='page' style='border-left:1px solid #ddbfd3 ;padding-left:10px'></span> / <span style='border-right:1px solid #ddbfd3 ;padding-right:10px' class='topage'></span></div>";
        //        //var pdfBytes = pdf.GeneratePdf(GetHtmlContent(url));
        //        var pdfBytes = pdf.GeneratePdfFromFile(url, null);
        //        sFileName += "_" + DateTime.Now.Ticks.ToString() + ".pdf";
        //        if (!Directory.Exists(dir))
        //            Directory.CreateDirectory(dir);
        //        File.WriteAllBytes(dir + sFileName, pdfBytes);
        //        return sFileName;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ExceptionLog(ex.Message, "CreatePdf");
        //        return null;
        //    }

        //}
        public static string CreatePdf1(string url, string sFileName)
        {
            try
            {
                //Log.ExceptionLog("1", "CreatePdf1");
                string dir = ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "PDF\\";
                //Log.ExceptionLog(dir, "CreatePdf1");
                NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdf.Margins = new NReco.PdfGenerator.PageMargins();
                pdf.Margins.Top = 1;
                pdf.Margins.Bottom = 5;
                pdf.Margins.Right = 1;
                pdf.Margins.Left = 1;
                pdf.Size = PageSize.A4;
                var pdfBytes = pdf.GeneratePdfFromFile(url, null);
                sFileName += "_" + DateTime.Now.ToFileTime().ToString() + ".pdf";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllBytes(dir + sFileName, pdfBytes);
                return sFileName;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("CreatePdf1", ex);
                return null;
            }

        }
        public static string CreatePdf(string url, string sFileName, string directory = null)
        {
            try
            {

                NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdf.Margins = new NReco.PdfGenerator.PageMargins();
                pdf.Margins.Top = 15;
                pdf.Margins.Bottom = 5;
                pdf.Margins.Right = 1;
                pdf.Margins.Left = 1;
                pdf.Size = PageSize.A4;
                pdf.PageFooterHtml = "<div style='text-align:center;'><span class='page' style='border-left:1px solid #ddbfd3 ;padding-left:10px'></span> / <span style='border-right:1px solid #ddbfd3 ;padding-right:10px' class='topage'></span></div>";
                pdf.PageHeaderHtml = "<div ><div style='text-align:right;'>בסיעתא דשמיא</div><div style='text-align:center;font-size:40px;'>" + "</div></div>";
                url = "<html><head><meta charset='utf-8' /><style type='text/css'>table tr {width:100%;text-align:center;}table {width:100%}    table th { text-align: center;font-size: large; }</style>" +
                   "</head><body><div  style='text-align:right;' dir='rtl'>" + url + "</div></body></html>";
                byte[] Bytes = Encoding.UTF8.GetBytes(url);
                string dirPdfPages = ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("PdfPath")];
                string dirPdfPage = ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("PdfPath")];
                if (!Directory.Exists(dirPdfPages))
                {
                    Directory.CreateDirectory(dirPdfPages);
                }
                if (!Directory.Exists(dirPdfPage))
                {
                    Directory.CreateDirectory(dirPdfPage);
                }
                File.WriteAllBytes(dirPdfPages + "HtmlPage1.html", Bytes);

                var pdfBytes = pdf.GeneratePdfFromFile(dirPdfPages + "HtmlPage1.html", null);
                File.WriteAllBytes(dirPdfPages + "HtmlToPdf1.pdf", pdfBytes);

                return dirPdfPage + "HtmlToPdf1.pdf";

            }
            catch (Exception ex)
            {

                LogWriter.WriteLog("CreatePdf", ex);
                return null;
            }

        }


        //public static string SaveCalendarDataInFile(string json)
        //{
        //    try
        //    {
        //        string sFileName = DateTime.Now.Ticks.ToString();
        //        string sPath = System.Configuration.ConfigurationManager.AppSettings["ShareFiles"] + sFileName + ".js";
        //        File.AppendAllText(sPath, json);
        //        return sFileName;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }

}