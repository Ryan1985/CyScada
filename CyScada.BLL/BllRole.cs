using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                //Authority = Convert.ToInt32(dr["Authority"]),
                AuthorityCode = dr["AuthorityCode"].ToString(),
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


        internal string AddRoleAuthority(string roleId, string authorityCode)
        {
            //获取权限的信息
            var dtRole = _dalRole.GetRoleList(new Hashtable
            {
                {"ID", roleId}
            });

            if (dtRole.Rows.Count == 0)
            {
                return "当前角色不存在！";
            }

            var role = dtRole.AsEnumerable().Select(dr => new RoleModel
            {
                //Authority = Convert.ToInt32(dr["Authority"]) | Convert.ToInt32(authorityId),//使用或运算添加权限
                AuthorityCode = CommonUtil.AppendAuthorityCode(dr["AuthorityCode"].ToString(), authorityCode),
                Description = dr["Description"].ToString(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString()
            }).ToList().First();

            return _dalRole.ModifyRole(role.ToHashTable());
        }

        internal string DeleteRoleAuthority(string roleId, string authorityCode)
        {

            //获取权限的信息
            var dtRole = _dalRole.GetRoleList(new Hashtable
            {
                {"ID", roleId}
            });

            if (dtRole.Rows.Count == 0)
            {
                return "当前角色不存在！";
            }

            var role = dtRole.AsEnumerable().Select(dr => new RoleModel
            {
                //Authority = Convert.ToInt32(dr["Authority"]) & (~Convert.ToInt32(authorityId)),//使用与运算删除权限
                AuthorityCode = CommonUtil.RemoveAuthorityCode(dr["AuthorityCode"].ToString(), authorityCode),
                Description = dr["Description"].ToString(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString()
            }).ToList().First();

            return _dalRole.ModifyRole(role.ToHashTable());
        }
    }
}
