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
                var roleAuthList = employee.RoleList.Where(r => r.Id == empRoleModel.RoleId).Select(r => r.AuthorityCode);
                foreach (var roleAuth in roleAuthList)
                {
                    CommonUtil.AppendAuthorityCode(authString, roleAuth);
                }
            }

            var dtItems = _dalControlDesk.GetItemList();


            return
                dtItems.AsEnumerable()
                    .Where(dr => CommonUtil.ExistAuthorityCode(authString, dr["AuthorityCode"].ToString()))
                    .Select(dr => new ControlDeskItemModel
                    {
                        Description = dr["Description"].ToString(),
                        Id = dr["Id"].ToString(),
                        Name = dr["Name"].ToString(),


                    });
        }
    }
}
