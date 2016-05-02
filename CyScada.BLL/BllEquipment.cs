using System.Collections.Generic;
using CyScada.Model;
using CyScada.DAL;

namespace CyScada.BLL
{
    public class BllEquipment
    {
        public EquipmentModel GetEquipment(string equipmentId)
        {
            var equipment = new EquipmentModel();
            equipment.EquipmentId = equipmentId;

            var dtEquipment = new DalEquipment().GetEquipment(equipmentId);
            if (dtEquipment.Rows.Count == 0)
            {
                return equipment;
            }


            equipment.EquipmentName = dtEquipment.Rows[0]["EquipmentName"].ToString();

            if (dtEquipment.Rows[0]["DTUID"] == null || string.IsNullOrEmpty(dtEquipment.Rows[0]["DTUID"].ToString()))
            {
                return equipment;
            }

            equipment.Items = new List<ItemModel>();
            for (var i = 2; i < dtEquipment.Columns.Count; i++)
            {
                equipment.Items.Add(new ItemModel { Name = dtEquipment.Columns[i].ColumnName, Value = dtEquipment.Rows[0][i].ToString() });
            }


            return equipment;
        }
    }
}
