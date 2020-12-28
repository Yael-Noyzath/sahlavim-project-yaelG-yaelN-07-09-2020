using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SachlavimService.Entities
{
    public class OperatorSchoolType
    {
        #region Members
        [DataMember]
        public int? iOperatorSchoolTypeId { get; set; }
        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public int? iSettingId { get; set; }
        [DataMember]
        public string nvSettingName { get; set; }
        #endregion Members
    }
}