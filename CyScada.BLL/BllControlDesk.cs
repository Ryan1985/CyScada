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
    public class BllControlDesk
    {
        private DalControlDesk _dalControlDesk = new DalControlDesk();
        private BllEmployee _bllEmployee = new BllEmployee();

        public IEnumerable<ControlDeskItemModel> GetItemList(string sideMenuId, string userId)
        {
            var employee = _bllEmployee.GetEmployeeWithAuthority(userId);
            var authString = employee.AuthorityCode;
            foreach (var empRoleModel in employee.EmpRoleList)
            {
                var roleAuthCode =
                    employee.RoleList.Where(r => r.Id == empRoleModel.RoleId)
                        .Select(r => r.AuthorityCode)
                        .FirstOrDefault();
                if (string.IsNullOrEmpty(roleAuthCode)) continue;

                var roleAuthList = roleAuthCode.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries);
                authString = roleAuthList.Aggregate(authString, CommonUtil.AppendAuthorityCode);
            }

            var dsItems = _dalControlDesk.GetItemList(sideMenuId);
            if (dsItems == null)
                return null;


            var result = dsItems.Tables[0].AsEnumerable()
                .Where(dr => CommonUtil.ExistAuthorityCode(authString, dr["AuthorityCode"].ToString()))
                .Select(dr => new ControlDeskItemModel
                {
                    Description = dr["SideMenuDesc"].ToString(),
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString(),
                    Url = dr["Url"].ToString(),
                    Title = dsItems.Tables[1].AsEnumerable().Select(d => d["SideMenuDesc"].ToString()).FirstOrDefault()
                }).ToList();

            return result;

        }
    }
}
