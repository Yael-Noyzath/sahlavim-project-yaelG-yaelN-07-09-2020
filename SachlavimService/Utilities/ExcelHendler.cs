using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SachlavimService.Utilities;

namespace SachlavimService
{
    public class ExcelHendler
    {
        public static string ExportToExcel(DataTable dt, string sFileName, string[] lColumns)
        {
            try
            {
                sFileName = sFileName.Replace("\"", "").Replace("\\", "");
                sFileName = sFileName + "_" + DateTime.Now.ToFileTime();
                ExportDataSet(dt, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Excel" + "\\" + sFileName + ".xlsx", lColumns);
                return sFileName + ".xlsx";
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("ExportToExcel", ex);
                return null;
            }
        }

        private static void ExportDataSet(System.Data.DataTable table, string destination, string[] lColumns)
        {
            bool isLocal = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName() == "Default Web Site";

            using (var workbook = SpreadsheetDocument.Create(destination, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = workbook.AddWorkbookPart();

                workbook.WorkbookPart.Workbook = new Workbook();

                workbook.WorkbookPart.Workbook.Sheets = new Sheets();

                var WorkbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                WorkbookStylesPart.Stylesheet = GetStylesheet();
                WorkbookStylesPart.Stylesheet.Save();

                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                sheetPart.Worksheet = new Worksheet(sheetData);


                Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                uint sheetId = 1;
                if (sheets.Elements<Sheet>().Count() > 0)
                {
                    sheetId =
                        sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }

                Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = table.TableName };
                sheets.Append(sheet);

                Row headerRow = new Row();

                List<String> columns = new List<string>();
                var index = 0;
                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(lColumns[index++]);
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);
                int num;
                string date;
                foreach (System.Data.DataRow dsrow in table.Rows)
                {
                    Row newRow = new Row();
                    foreach (String col in columns)
                    {
                        Cell cell = new Cell();
                        //if (DateTime.TryParse(dsrow[col].ToString(), out date))
                        if (dsrow[col].ToString().Length == 10 && dsrow[col].ToString()[2] == '/' && dsrow[col].ToString()[5] == '/')
                        {
                            cell.DataType = CellValues.Date;
                            cell.StyleIndex = 2;
                            if (isLocal)
                                date = dsrow[col].ToString();
                            else date = DateTime.ParseExact(dsrow[col].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                            cell.CellValue = new CellValue(Convert.ToDateTime(date).ToOADate().ToString());
                        }
                        else
                        {
                            if (int.TryParse(dsrow[col].ToString(), out num))
                            { cell.DataType = CellValues.Number; cell.StyleIndex = 1; }
                            else
                            { cell.DataType = CellValues.String; cell.StyleIndex = 0; }
                            cell.CellValue = new CellValue(fixData(dsrow[col].ToString()));
                        }
                        newRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(newRow);
                }
            }
        }

        private static Stylesheet GetStylesheet()
        {
            var StyleSheet = new Stylesheet();

            // Create "fonts" node.
            var Fonts = new Fonts();
            Fonts.Append(new Font()
            {
                FontName = new FontName() { Val = "Calibri" },
                FontSize = new FontSize() { Val = 11 },
                FontFamilyNumbering = new FontFamilyNumbering() { Val = 2 },
            });

            Fonts.Count = (uint)Fonts.ChildElements.Count;

            // Create "fills" node.
            var Fills = new Fills();
            Fills.Append(new Fill()
            {
                PatternFill = new PatternFill() { PatternType = PatternValues.None }
            });
            Fills.Append(new Fill()
            {
                PatternFill = new PatternFill() { PatternType = PatternValues.Gray125 }
            });

            Fills.Count = (uint)Fills.ChildElements.Count;

            // Create "borders" node.
            var Borders = new Borders();
            Borders.Append(new Border()
            {
                LeftBorder = new LeftBorder(),
                RightBorder = new RightBorder(),
                TopBorder = new TopBorder(),
                BottomBorder = new BottomBorder(),
                DiagonalBorder = new DiagonalBorder()
            });

            Borders.Count = (uint)Borders.ChildElements.Count;

            // Create "cellStyleXfs" node.
            var CellStyleFormats = new CellStyleFormats();
            CellStyleFormats.Append(new CellFormat()
            {
                NumberFormatId = 0,
                FontId = 0,
                FillId = 0,
                BorderId = 0
            });

            CellStyleFormats.Count = (uint)CellStyleFormats.ChildElements.Count;

            // Create "cellXfs" node.
            var CellFormats = new CellFormats();
            CellFormats.Append(new CellFormat(),
                               new CellFormat()
                               {
                                   BorderId = 0,
                                   FillId = 0,
                                   FontId = 0,
                                   NumberFormatId = 1,
                                   FormatId = 0,
                                   ApplyNumberFormat = true
                               },
                              new CellFormat()
                              {
                                  BorderId = 0,
                                  FillId = 0,
                                  FontId = 0,
                                  NumberFormatId = 14,
                                  FormatId = 0,
                                  ApplyNumberFormat = true
                              });

            CellFormats.Count = (uint)CellFormats.ChildElements.Count;

            // Create "cellStyles" node.
            var CellStyles = new CellStyles();
            CellStyles.Append(new CellStyle()
            {
                Name = "Normal",
                FormatId = 0,
                BuiltinId = 0
            });
            CellStyles.Count = (uint)CellStyles.ChildElements.Count;

            // Append all nodes in order.
            StyleSheet.Append(Fonts);
            StyleSheet.Append(Fills);
            StyleSheet.Append(Borders);
            StyleSheet.Append(CellStyleFormats);
            StyleSheet.Append(CellFormats);
            StyleSheet.Append(CellStyles);

            return StyleSheet;
        }

        private static string fixData(string cell)
        {
            switch (cell)
            {
                case "null": return "";
                case "undefined": return "";
                case "true": return "V";
                case "false": return "";
                case "-1": return "";
            }
            return cell;
        }

        public static string GeneratePdf(string title, string html, string css, string name)
        {
            try
            {

                NReco.PdfGenerator.HtmlToPdfConverter pdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdf.Margins = new NReco.PdfGenerator.PageMargins();
                pdf.Margins.Top = 15;
                pdf.Margins.Bottom = 5;
                pdf.Margins.Right = 5;
                pdf.Margins.Left = 5;
                pdf.Orientation = PageOrientation.Portrait;
                pdf.Size = PageSize.A4;
                string hostName = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.Host + System.Web.Hosting.HostingEnvironment.ApplicationHost.GetVirtualPath();
                if (css != null)
                    css = css.Replace("../", hostName + "/");
                pdf.PageFooterHtml = "<div style='text-align:center;'><span class='page' style='border-left:1px solid #ddbfd3;padding-left:10px'></span> / <span style='border-right:1px solid #ddbfd3;padding-right:10px' class='topage'></span></div>";
                pdf.PageHeaderHtml = "<div><div style='text-align:center; font-size:20px; color: #06416D; font-family: Arial'>" + title + "</div></div>";
                html = "<html><head><meta charset='utf-8' /><style type='text/css'>" + css + "</style></head>" +
                    "<body><div style='text-align:right;' dir='rtl'>" + html + "</div></body></html>";
                string fileName = name.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "") + "_" + DateTime.Now.ToFileTime();
                byte[] Bytes = Encoding.UTF8.GetBytes(html);

                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\Files\\pdfGenerator.html", Bytes);
                //Log.ExceptionLog("file path: ", AppDomain.CurrentDomain.BaseDirectory + "Files\\" + fileName + ".html");
                var pdfBytes = pdf.GeneratePdfFromFile(hostName + "\\Files\\pdfGenerator.html", null);

                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "Files\\" + fileName + ".pdf", pdfBytes);

                DeleteFile(fileName + ".html");//delete html file                              

                return fileName + ".pdf";
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GeneratePdf", ex);
                return null;
            }
        }

        public static bool DeleteFile(string fileName)
        {
            if (System.IO.File.Exists(@AppDomain.CurrentDomain.BaseDirectory + "Files\\" + fileName))
            {
                try
                {
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Files\\" + fileName);
                    return true;
                }
                catch (System.IO.IOException ex)
                {
                    LogWriter.WriteLog("DeleteFile", ex);
                    return false;
                }
            }
            return false;

        }

    }
}




