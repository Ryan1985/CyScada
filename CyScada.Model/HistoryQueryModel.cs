using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class HistoryQueryModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string MachineId { get; set; }
        public string DeviceName { get; set; }

    }
}
