using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.ServiceModel.Channels;
using SachlavimService.Utilities;

namespace SachlavimService.Entities
{
    [DataContract]
    public class Coordinator
    {
        #region Members

        [DataMember]
        public int? iCoordinatorId { get; set; }
        [DataMember]
        public string nvFirstName { get; set; }
        [DataMember]
        public string nvLastName { get; set; }
        [DataMember]
        public string nvPhone { get; set; }
        [DataMember]
        public string nvMail { get; set; }
        [DataMember]
        public bool? bIsActive { get; set; }

        #endregion Members

        #region Methods

        public static List<Coordinator> CoordinatorsGet()
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TCoordinator_Slct");
                List<Coordinator> lCoordinator = new List<Coordinator>();
                if (ds.Tables.Count > 0)
                {
                    lCoordinator = ObjectGenerator<Coordinator>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                }
                return lCoordinator;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("CoordinatorsGet", ex);
                return null;
            }
        }
        
        public static List<Coordinator> CoordinatorInsertUpdt(Coordinator oCoordinator, int? iUserId)
        {
            try
            {
                List<Coordinator> lCoordinator = new List<Coordinator>();
                List<SqlParameter> lParams = new List<SqlParameter>();
                lParams = ObjectGenerator<Coordinator>.GetSqlParametersFromObject(oCoordinator);
                lParams.Add(new SqlParameter("iUserId", iUserId));
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TCoordinator_Ins_Updt", lParams);
                if (ds.Tables.Count > 0)
                {
                    lCoordinator = ObjectGenerator<Coordinator>.GeneratListFromDataRowCollection(ds.Tables[0].Rows);
                }

                return lCoordinator;

            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("CoordinatorInsert", ex);
                return null;
            }
        }


        #endregion Methods

    }
}
      