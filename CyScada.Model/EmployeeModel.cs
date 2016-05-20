using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class EmployeeModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        //public int? Authority { get; set; }
        public string AuthorityCode { get; set; }
        public string Code { get; set; }
        //public string AuthoritySummary
        //{
        //    get
        //    {
        //        if (AuthorityCode == null)
        //            return null;
        //        var sbAuth = new StringBuilder(AuthorityCode);
        //        foreach (
        //            var authCode in EmpRoleList.Select(empRoleModel => RoleList.Where(r => r.Id == empRoleModel.RoleId)
        //                .Select(r => r.AuthorityCode).FirstOrDefault()))
        //        {
        //            sbAuth.Append(authCode + ",");
        //        }
        //        return sbAuth.ToString();
        //    }
        //}
        /// <summary>
        /// 所有的可用角色列表
        /// </summary>
        public IList<RoleModel> RoleList { get; set; }
        /// <summary>
        /// 所有的可用权限列表
        /// </summary>
        public IList<AuthorityModel> AuthorityList { get; set; }
        /// <summary>
        /// 当前用户的角色
        /// </summary>
        public IList<EmpRoleModel> EmpRoleList { get; set; }

    }
}
