using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class HistoryModel
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string WorkSite { get; set; }
        public string MachineName { get; set; }
        public string TagName { get; set; }
        public string Value { get; set; }
        public string TimeStamp { get; set; }
    }
}
