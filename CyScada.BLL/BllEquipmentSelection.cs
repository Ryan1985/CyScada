using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CyScada.Model;
using CyScada.DAL;

namespace CyScada.BLL
{
    public class BllEquipmentSelection
    {
        protected DalEquipment DalEquipment = new DalEquipment();
        protected DalLogin DalLogin = new DalLogin();

        public IEnumerable<EquipmentModel> GetEquipments(string UserId)
        {
            var dtLogin = DalLogin.GetUser(UserId);
            if (dtLogin.Rows.Count==0)
            {
                return new List<EquipmentModel>();
            }
            var dtEquipment = new DataTable();
            if (dtLogin.Rows[0]["UserType"].ToString() == "1")
            {
                dtEquipment = DalEquipment.GetEquipmentList();
            }
            else
            {
                dtEquipment = DalEquipment.GetEquipmentList(UserId);
            }
            return dtEquipment.AsEnumerable().Select(dr => new EquipmentModel {
                EquipmentId = dr["EquipmentId"].ToString(),
                EquipmentName = dr["EquipmentName"].ToString(),
                Description = dr["Description"].ToString()
            }).ToList();
        }
    }
}
