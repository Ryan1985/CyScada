using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CyScada.Model
{
    public class EquipmentModel
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string Description { get; set; }

        public List<ItemModel> Items { get; set; }
    }
}
