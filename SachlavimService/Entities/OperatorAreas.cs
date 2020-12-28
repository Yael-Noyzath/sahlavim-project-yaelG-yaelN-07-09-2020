using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SachlavimService.Entities
{
    [DataContract]
    public class OperatorAreas
    {
        #region Members
        [DataMember]
        public int? iOperatorAreasId { get; set; }
        [DataMember]
        public int? iOperatorId { get; set; }
        [DataMember]
        public int? iAreasType { get; set; }
        [DataMember]
        public string nvValue { get; set; }
        #endregion Members
    }
}