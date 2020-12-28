using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SachlavimService.Utilities;

namespace SachlavimService.Entities
{
    public class Review
    {
        #region Members
        [DataMember]
        public int? iReviewId { get; set; }
        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public string nvTopic { get; set; }
        [DataMember]
        public string nvContent { get; set; }
        [DataMember]
        public string nvFilePath { get; set; }
        [DataMember]
        public Boolean bStatus { get; set; }
        [DataMember]
        [NoSendToSQL]
        public DateTime? dtCreateDate { get; set; }

        #endregion Members

        #region Methods

        public static List<Review> GetReviews(int iOperatorId)
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TReviews_Slct", new SqlParameter("iOperatorId", iOperatorId));
                List<Review> lReviews = new List<Review>();
                if (ds.Tables.Count > 0)
                    lReviews = ObjectGenerator<Review>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                return lReviews;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("GetReviews", ex);
                return null;
            }
        }

        public static Review InsertReview(Review oReview, int iUserId)
        {
            try
            {
                if (oReview.nvFilePath != null && oReview.nvFilePath != "" && oReview.nvFilePath.IndexOf("+++**") != -1)
                {
                    string file = oReview.nvFilePath;
                    string timeStmp = DateTime.Now.Ticks.ToString();
                    Global.SaveFile("Review" + oReview.iReviewId + "_" + timeStmp, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Review\\", file.Substring(0, file.IndexOf("+++**")), file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5)));
                    oReview.nvFilePath = "Review" + oReview.iReviewId + "_" + timeStmp + "." + file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5));
                }
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters = ObjectGenerator<Review>.GetSqlParametersFromObject(oReview);
                parameters.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TReview_INS", parameters);
                Review Oreview = new Review();
                if (ds.Tables.Count > 0)
                    Oreview = ObjectGenerator<Review>.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                return Oreview;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("InsertReview", ex);
                return null;
            }
        }

        public static string UpdtReview(Review oReview, int iUserId)
        {
            try
            {
                if (oReview.nvFilePath != null && oReview.nvFilePath != "" && oReview.nvFilePath.IndexOf("+++**") != -1)
                {
                    string file = oReview.nvFilePath;
                    string timeStmp = DateTime.Now.Ticks.ToString();
                    Global.SaveFile("Review" + oReview.iReviewId + "_" + timeStmp, ConfigurationManager.AppSettings[ConfigSettings.GetConfigSettingByHost("FilesPath")] + "Review\\", file.Substring(0, file.IndexOf("+++**")), file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5)));
                    oReview.nvFilePath = "Review" + oReview.iReviewId + "_" + timeStmp + "." + file.Substring(file.IndexOf("+++**") + 5, file.Length - (file.IndexOf("+++**") + 5));
                }
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters = ObjectGenerator<Review>.GetSqlParametersFromObject(oReview);
                parameters.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TReview_UPDT", parameters);
                if (oReview.nvFilePath!=null && oReview.nvFilePath != "")
                    return oReview.nvFilePath;
                return "true";
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("UpdtReview", ex);
                return "";
            }
        }

        #endregion Methods

    }
}