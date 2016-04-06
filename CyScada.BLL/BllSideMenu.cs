using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.Model;
using CyScada.DAL;

namespace CyScada.BLL
{
    
    public class BllSideMenu
    {
        private DalSideMenu _DalSideMenu = new DalSideMenu();

        public DalSideMenu DalSideMenu
        {
            get { return _DalSideMenu; }
            set { _DalSideMenu = value; }
        }


        public IList<SideMenuListModel> GetMenuList(UserModel user)
        {
            var sideMenuSet = _DalSideMenu.QuerySideMenuSet();
            var sideMenuList = sideMenuSet.Tables["SideMenu"].AsEnumerable()
                .Where(dr => (Convert.ToInt32(dr["AuthorityId"]) & user.Authority) == Convert.ToInt32(dr["AuthorityId"]))
                .Select(dr => new SideMenuListModel
                {
                    AuthorityId = Convert.ToInt32(dr["AuthorityId"]),
                    Class = dr["Class"].ToString(),
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Url = dr["Url"].ToString(),
                    SortNumber = Convert.ToInt32(dr["SortNumber"]),
                    SubMenus = sideMenuSet.Tables["SubMenu"].AsEnumerable()
                        .Where(d => Convert.ToInt32(dr["Id"]) == Convert.ToInt32(d["ParentId"])
                                    &&
                                    (Convert.ToInt32(d["AuthorityId"]) & user.Authority) ==
                                    Convert.ToInt32(d["AuthorityId"]))
                        .Select(d => new SideMenuModel
                        {
                            AuthorityId = Convert.ToInt32(d["AuthorityId"]),
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
    }
}
