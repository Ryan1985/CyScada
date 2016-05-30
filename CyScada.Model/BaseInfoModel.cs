using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class BaseInfoModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string IMEI { get; set; }
        public string CCID { get; set; }
        public string WorkSite { get; set; }
        public string Company { get; set; }
        public string AuthorityCode { get; set; }
        public string Pic { get; set; }
        public string MachineType { get; set; }


    }
}
