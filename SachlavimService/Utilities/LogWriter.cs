using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace SachlavimService.Utilities
{
    public class LogWriter
    {
        public static void WriteLog(string functionName, Exception ex)
        {
            SqlParameter[] param = {   new SqlParameter("nvException",ex!=null?ex.Message:""),
                                       new SqlParameter("nvFunction", functionName)};
            SqlDataAccess.ExecuteDatasetSP("TSysLog_Ins", param);
        }

        public static void WriteDataLog(string logName, string logValue)
        {
            string sFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            string sPath = ConfigSettings.ReadSetting("LogFilesFolderPath") + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
            if (Directory.Exists(sPath) == false)
                System.IO.Directory.CreateDirectory(sPath);

            File.AppendAllText(sPath + '/' + sFileName + ".html", DateTime.Now + "LogName: " + logName + "</br>");
            File.AppendAllText(sPath + '/' + sFileName + ".html", DateTime.Now + "LogValue: " + logValue + "</br>");

        }
    }
}