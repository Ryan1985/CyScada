using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CyScada.Model;
using CyScada.DAL;
using CyScada.Common;

namespace CyScada.BLL
{
    
    public class BllSideMenu
    {
        private DalSideMenu _dalSideMenu = new DalSideMenu();
        private DalAuthority _dalAuthority = new DalAuthority();
        private BllEmployee _bllEmployee = new BllEmployee();


        public DalSideMenu DalSideMenu
        {
            get { return _dalSideMenu; }
            set { _dalSideMenu = value; }
        }

        public DalAuthority DalAuthority
        {
            get { return _dalAuthority; }
            set { _dalAuthority = value; }
        }

        public BllEmployee BLLEmployee
        {
            get { return _bllEmployee; }
            set { _bllEmployee = value; }
        }


        public IList<SideMenuModel> GetMenuListFlat()
        {
            var sideMenuSet = _dalSideMenu.QuerySideMenuSet();
            var authList = _dalAuthority.GetAuthorityList();
            var classList = GetClassList();

            var menuList = sideMenuSet.Tables["SideMenu"].AsEnumerable().Select(dr => new SideMenuModel
            {
                //AuthorityId = dr["AuthorityId"].ConvertToNullable<Int32>(),
                AuthorityCode = dr["AuthorityCode"].ToString(),
                Class = dr["Class"].ToString(),
                ClassName = classList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString(),
                SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
                Url = dr["Url"].ToString(),
                AuthorityName = authList.AsEnumerable().Where(a => a["AuthorityId"].ToString().ToLower() == dr["AuthorityId"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
            }).ToList();

            var submenuList = sideMenuSet.Tables["SubMenu"].AsEnumerable().Select(dr => new SideMenuModel
            {
                //AuthorityId = dr["AuthorityId"].ConvertToNullable<Int32>(),
                AuthorityCode = dr["AuthorityCode"].ToString(),
                Class = dr["Class"].ToString(),
                ClassName = classList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString(),
                ParentId = dr["ParentId"].ConvertToNullable<Int32>(),
                ParentName = sideMenuSet.Tables["SideMenu"].AsEnumerable().Where(a => a["Id"].ToString().ToLower() == dr["ParentId"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
                SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
                Url = dr["Url"].ToString(),
                AuthorityName = authList.AsEnumerable().Where(a => a["AuthorityId"].ToString().ToLower() == dr["AuthorityId"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
            }).ToList();

            menuList.AddRange(submenuList);
            return menuList;
        }


        public IList<SideMenuModel> GetMenuListFlat(SideMenuModel model)
        {
            var sideMenuSet = _dalSideMenu.QuerySideMenuSet();
            var authList = _dalAuthority.GetAuthorityList();
            var classList = GetClassList();

            var menuList = sideMenuSet.Tables["SideMenu"].AsEnumerable()
                .Select(dr => new SideMenuModel
            {
                //AuthorityId = dr["AuthorityId"].ConvertToNullable<Int32>(),
                AuthorityCode = dr["AuthorityCode"].ToString(),
                Class = dr["Class"].ToString(),
                ClassName = classList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString(),
                SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
                Url = dr["Url"].ToString(),
                AuthorityName = authList.AsEnumerable().Where(a => a["AuthorityId"].ToString().ToLower() == dr["AuthorityId"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
            }).ToList();

            var submenuList = sideMenuSet.Tables["SubMenu"].AsEnumerable().Select(dr => new SideMenuModel
            {
                //AuthorityId = dr["AuthorityId"].ConvertToNullable<Int32>(),
                AuthorityCode = dr["AuthorityCode"].ToString(),
                Class = dr["Class"].ToString(),
                ClassName = classList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString(),
                ParentId = dr["ParentId"].ConvertToNullable<Int32>(),
                ParentName = sideMenuSet.Tables["SideMenu"].AsEnumerable().Where(a => a["Id"].ToString().ToLower() == dr["ParentId"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
                SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
                Url = dr["Url"].ToString(),
                AuthorityName = authList.AsEnumerable().Where(a => a["AuthorityId"].ToString().ToLower() == dr["AuthorityId"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
            }).ToList();

            menuList.AddRange(submenuList);

            if (!string.IsNullOrEmpty(model.Name))
            {
                menuList = menuList.Where(s => s.Name.IndexOf(model.Name, StringComparison.Ordinal) > -1).Select(s => s).ToList();
            }

            if (!string.IsNullOrEmpty(model.AuthorityCode))
            {
                menuList = menuList.Where(s => s.AuthorityCode.Equals(model.AuthorityCode, StringComparison.CurrentCultureIgnoreCase)).Select(s => s).ToList();
            }
            //if (model.AuthorityId != null)
            //{
            //    menuList = menuList.Where(s => s.AuthorityId == model.AuthorityId).Select(s => s).ToList();
            //}
            return menuList;
        }

        public DataTable GetClassList()
        {
            var classTable = new DataTable();
            classTable.Columns.Add("Class");
            classTable.Columns.Add("ClassName");

            classTable.Rows.Add("fa fa-user fa-fw", "用户");
            classTable.Rows.Add("fa fa-sign-out fa-fw", "登出");

            return classTable;
        }




        public IList<SideMenuListModel> GetMenuList()
        {
            var sideMenuSet = _dalSideMenu.QuerySideMenuSet();
            var sideMenuList = sideMenuSet.Tables["SideMenu"].AsEnumerable()
                .Select(dr => new SideMenuListModel
                {
                    //AuthorityId = Convert.ToInt32(dr["AuthorityId"]),
                    AuthorityCode=dr["AuthorityCode"].ToString(),
                    Class = dr["Class"].ToString(),
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Url = dr["Url"].ToString(),
                    SortNumber = Convert.ToInt32(dr["SortNumber"]),
                    SubMenus = sideMenuSet.Tables["SubMenu"].AsEnumerable()
                        .Where(d => Convert.ToInt32(dr["Id"]) == Convert.ToInt32(d["ParentId"]))
                        .Select(d => new SideMenuModel
                        {
                            //AuthorityId = Convert.ToInt32(d["AuthorityId"]),
                            AuthorityCode = d["AuthorityCode"].ToString(),
                            Class = d["Class"].ToString(),
                            Id = Convert.ToInt32(d["Id"]),
                            Name = d["Name"].ToString(),
                            Url = d["Url"].ToString(),
                            SortNumber = Convert.ToInt32(d["SortNumber"]),
                            ParentId = Convert.ToInt32(d["ParentId"])
                        }).OrderBy(s => s.SortNumber).ToList()
                }).OrderBy(sm => sm.SortNumber).ToList();
            return sideMenuList;
        }




        public IList<SideMenuListModel> GetMenuList(UserModel user)
        {
            var empWithAuth = _bllEmployee.GetEmployeeWithAuthority(user.Id.ToString());
            var userAuth =
                empWithAuth.EmpRoleList.Select(
                    empRoleModel =>
                        empWithAuth.RoleList.Where(r => r.Id == empRoleModel.RoleId)
                            .Select(r => r.AuthorityCode)
                            .FirstOrDefault())
                    .Aggregate(empWithAuth.AuthorityCode,
                        CommonUtil.AppendAuthorityCode);

            var sideMenuSet = _dalSideMenu.QuerySideMenuSet();
            var sideMenuList = sideMenuSet.Tables["SideMenu"].AsEnumerable()
                .Where(dr => CommonUtil.ExistAuthorityCode(userAuth,dr["AuthorityCode"].ToString()))
                .Select(dr => new SideMenuListModel
                {
                    //AuthorityId = Convert.ToInt32(dr["AuthorityId"]),
                    AuthorityCode=dr["AuthorityCode"].ToString(),
                    Class = dr["Class"].ToString(),
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Url = dr["Url"].ToString(),
                    SortNumber = Convert.ToInt32(dr["SortNumber"]),
                    SubMenus = sideMenuSet.Tables["SubMenu"].AsEnumerable()
                        .Where(d => Convert.ToInt32(dr["Id"]) == Convert.ToInt32(d["ParentId"])
                                    &&
                                    (CommonUtil.ExistAuthorityCode(user.AuthorityCode,d["AuthorityCode"].ToString())))
                        .Select(d => new SideMenuModel
                        {
                            //AuthorityId = Convert.ToInt32(d["AuthorityId"]),
                            AuthorityCode = d["AuthorityCode"].ToString(),
                            Class = d["Class"].ToString(),
                            Id = Convert.ToInt32(d["Id"]),
                            Name = d["Name"].ToString(),
                            Url = d["Url"].ToString(),
                            SortNumber = Convert.ToInt32(d["SortNumber"]),
                            ParentId = Convert.ToInt32(d["ParentId"])
                        }).OrderBy(s => s.SortNumber).ToList()
                }).OrderBy(sm => sm.SortNumber).ToList();
            return sideMenuList;
        }

        public string SaveSideMenu(SideMenuModel model)
        {
            return model.Id.HasValue ? ModifySideMenu(model) : CreateSideMenu(model);
        }

        private string ModifySideMenu(SideMenuModel model)
        {
            return _dalSideMenu.ModifySideMenu(model.ToHashTable());
        }

        private string CreateSideMenu(SideMenuModel model)
        {
           return  _dalSideMenu.CreateSideMenu(model.ToHashTable());
        }

        public void DeleteSideMenu(int id)
        {
            _dalSideMenu.DeleteSideMenu(id);
        }
    }
}
