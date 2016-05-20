using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using CyScada.Common;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllAuthority
    {
        private DalAuthority _dalAuthority = new DalAuthority();

        public DalAuthority DalAuthority
        {
            get { return _dalAuthority; }
            set { _dalAuthority = value; }
        }


        public IEnumerable<AuthorityModel> GetAuthorityList()
        {
            return GetAuthorityList(new AuthorityModel());
        }


        public IEnumerable<AuthorityModel> GetAuthorityList(AuthorityModel filterModel)
        {
            var dtAuthoritys = _dalAuthority.GetAuthorityList(filterModel.ToHashTable());
            return dtAuthoritys.AsEnumerable().Select(dr => new AuthorityModel
            {
                Description = dr["Description"].ToString(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString(),
                AuthorityCode = dr["AuthorityCode"].ToString()
            });
        }


        public string SaveAuthority(AuthorityModel model)
        {
            return model.Id.HasValue ? ModifyAuthority(model) : CreateAuthority(model);
        }

        protected string CreateAuthority(AuthorityModel model)
        {
            if (CheckExist(model))
            {
                return "新增的权限标识已经存在";
            }
            return _dalAuthority.CreateAuthority(model.ToHashTable());
        }

        protected string ModifyAuthority(AuthorityModel model)
        {
            return _dalAuthority.ModifyAuthority(model.ToHashTable());
        }



        public void DeleteAuthority(int id)
        {
            _dalAuthority.DeleteAuthority(id);
        }

        protected bool CheckExist(AuthorityModel model)
        {
            var authList = _dalAuthority.GetAuthorityList();
            return
                authList.Rows.Cast<DataRow>()
                    .Any(
                        dr =>
                            dr["AuthorityCode"].ToString()
                                .Equals(model.AuthorityCode, StringComparison.CurrentCultureIgnoreCase));
        }



    }
}
