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
                AuthorityId = Convert.ToInt32(dr["AuthorityId"])
            });
        }

        //protected string CheckAuthorityId(AuthorityModel model)
        //{
        //    var authId = -1;
        //    if (!model.AuthorityId.HasValue||!int.TryParse(model.AuthorityId.Value.ToString(CultureInfo.InvariantCulture),out authId))
        //    {
        //        return "错误的权限ID";
        //    }

        //    var authList = GetAuthorityList(new AuthorityModel { AuthorityId = authId }).ToArray();
        //    if (authList.Any())
        //    {
        //        return "该ID与已配置的权限:" + authList.First().Name + "(id=" + authList.First().Id + ")重复";
        //    }

        //    return string.Empty;
        //}


        public string SaveAuthority(AuthorityModel model)
        {
            return model.Id.HasValue ? ModifyAuthority(model) : CreateAuthority(model);
        }

        protected string CreateAuthority(AuthorityModel model)
        {
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

    }
}
