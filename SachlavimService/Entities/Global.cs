using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using SachlavimService.Utilities;

namespace SachlavimService.Entities
{
    public class Global
    {
        #region Methods

        public static string GetGlobalParameters(int iGlobalParamId)
        {
            try
            {
                SqlParameter returnParam = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                returnParam.Direction = ParameterDirection.ReturnValue;
                SqlParameter nvValue = new SqlParameter("nvGlobalParamValue", SqlDbType.NVarChar, 50);
                nvValue.Direction = ParameterDirection.Output;
                nvValue.Size = 50;

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("iGlobalParamId", iGlobalParamId));
                param.Add(returnParam);
                param.Add(nvValue);
                SqlDataAccess.ExecuteSP("TSysGlobalParameters_SLCT", param);

                if (Convert.ToInt32(returnParam.Value) == 0)
                    return nvValue.Value.ToString();
                else
                    return null;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog("GetGlobalParameters", e);
                return null;
            }
        }

        public static string SaveFile(string sFileName, string sFolder, string sFile, string sTypeFile)
        {
            try
            {
                byte[] array = Convert.FromBase64String(sFile);
                string sPath = sFolder + sFileName + "." + sTypeFile;
                // if (fileExistWithAnyExtention(sFolder, sFileName))
                // {
                if (!Directory.Exists(sFolder))
                {
                    Directory.CreateDirectory(sFolder);
                }
                string[] files = Directory.GetFiles(sFolder, sFileName + ".*");
                foreach (var file in files)
                {
                    File.Delete(file);
                }
                // }
                File.WriteAllBytes(sPath, array);
                return sFileName + "." + sTypeFile;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("SaveFile", ex);
                return "error";
            }
        }

        public static bool DeleteFile(string sShortFileName, string sURL)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(sURL);
                foreach (string filePath in filePaths)
                {
                    if (filePath.StartsWith(sURL + sShortFileName))
                        File.Delete(filePath);
                }
                return true;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog("DeleteFile", e);

                return false;
            }
        }

        #endregion Methods
    }
}