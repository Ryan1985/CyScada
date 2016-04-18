using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.Common;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllRole
    {
        private DalRole _dalRole = new DalRole();

        public DalRole DalRole
        {
            get { return _dalRole; }
            set { _dalRole = value; }
        }


        public IEnumerable<RoleModel> GetRoleList()
        {
            return GetRoleList(new RoleModel());
        }


        public IEnumerable<RoleModel> GetRoleList(RoleModel filterModel)
        {
            var dtRoles = _dalRole.GetRoleList(filterModel.ToHashTable());
            return dtRoles.AsEnumerable().Select(dr => new RoleModel
            {
                Authority = Convert.ToInt32(dr["Authority"]),
                Description = dr["Description"].ToString(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString()
            });
        }

        public string SaveRole(RoleModel model)
        {
            return model.Id.HasValue ? ModifyRole(model) : CreateRole(model);
        }

        protected string CreateRole(RoleModel model)
        {
            return _dalRole.CreateRole(model.ToHashTable());
        }

        protected string ModifyRole(RoleModel model)
        {
            return _dalRole.ModifyRole(model.ToHashTable());
        }



        public void DeleteRole(int id)
        {
            _dalRole.DeleteRole(id);
        }

    }
}
