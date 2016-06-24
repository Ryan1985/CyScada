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
                SideMenuDesc = dr["SideMenuDesc"].ToString(),
                MenuType = dr["MenuType"].ToString(),
                Class = dr["Class"].ToString(),
                ClassName = classList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString(),
                SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
                Url = dr["Url"].ToString(),
                AuthorityName = authList.AsEnumerable().Where(a => a["AuthorityCode"].ToString().ToLower() == dr["AuthorityCode"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
            }).ToList();

            var submenuList = sideMenuSet.Tables["SubMenu"].AsEnumerable().Select(dr => new SideMenuModel
            {
                //AuthorityId = dr["AuthorityId"].ConvertToNullable<Int32>(),
                AuthorityCode = dr["AuthorityCode"].ToString(),
                SideMenuDesc = dr["SideMenuDesc"].ToString(),
                MenuType = dr["MenuType"].ToString(),
                Class = dr["Class"].ToString(),
                ClassName = classList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString(),
                ParentId = dr["ParentId"].ConvertToNullable<Int32>(),
                ParentName =GetParentName(sideMenuSet,dr["ParentId"].ToString()),
                SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
                Url = dr["Url"].ToString(),
                AuthorityName = authList.AsEnumerable().Where(a => a["AuthorityCode"].ToString().ToLower() == dr["AuthorityCode"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
            }).ToList();

            menuList.AddRange(submenuList);
            return menuList;
        }


        protected string GetParentName(DataSet dsSideMenuSet, string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                return string.Empty;
            }

            var parentName = string.Empty;
            if (dsSideMenuSet.Tables[0].Select("Id = '" + parentId + "'").Length > 0)
            {
                parentName = dsSideMenuSet.Tables[0].Select("Id = '" + parentId + "'")[0]["Name"].ToString();
            }
            if (dsSideMenuSet.Tables[1].Select("Id = '" + parentId + "'").Length > 0)
            {
                parentName = dsSideMenuSet.Tables[1].Select("Id = '" + parentId + "'")[0]["Name"].ToString();
            }
            return parentName;
        }




        //public IList<SideMenuModel> GetMenuListFlat()
        //{
        //    var sideMenu = _dalSideMenu.QuerySideMenu();
        //    var authList = _dalAuthority.GetAuthorityList();
        //    var classList = GetClassList();


        //    var menuList =
        //        sideMenu.AsEnumerable()
        //            .Where(dr => dr["ParentId"] == null || dr["ParentId"].ToString() == string.Empty)
        //            .Select(dr => new SideMenuModel
        //            {
        //                AuthorityCode = dr["AuthorityCode"].ToString(),
        //                Class = dr["Class"].ToString(),
        //                ClassName = classList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
        //                Id = Convert.ToInt32(dr["Id"]),
        //                Name = dr["Name"].ToString(),
        //                SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
        //                Url = dr["Url"].ToString(),
        //                AuthorityName = authList.AsEnumerable().Where(a => a["AuthorityCode"].ToString().ToLower() == dr["AuthorityCode"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
        //            }).ToList();


        //    foreach (var menu in menuList)
        //    {
        //        AppendSubMenus(menu, sideMenu, classList, authList);
        //    }

        //    return menuList;
        //}


        protected void AppendSubMenus(SideMenuModel menu, DataTable dtSideMenus, DataTable dtClassList,
            DataTable dtAuthList,string strUserAuth)
        {
            var submenuList =
                dtSideMenus.AsEnumerable()
                .Where(dr => dr["ParentId"].ToString() == menu.Id.ToString() && CommonUtil.ExistAuthorityCode(strUserAuth, dr["AuthorityCode"].ToString()))
                    .Select(dr => new SideMenuModel
                    {
                        AuthorityCode = dr["AuthorityCode"].ToString(),
                        SideMenuDesc = dr["SideMenuDesc"].ToString(),
                        MenuType = dr["MenuType"].ToString(),
                        Class = dr["Class"].ToString(),
                        ClassName = dtClassList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        ParentId = dr["ParentId"].ConvertToNullable<Int32>(),
                        ParentName = dtSideMenus.AsEnumerable().Where(a => a["Id"].ToString().ToLower() == dr["ParentId"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
                        SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
                        Url = dr["Url"].ToString(),
                        AuthorityName = dtAuthList.AsEnumerable().Where(a => a["AuthorityCode"].ToString().ToLower() == dr["AuthorityCode"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
                    }).ToList();


            if (submenuList.Any())
            {
                foreach (var submenu in submenuList)
                {
                    AppendSubMenus(submenu, dtSideMenus, dtClassList, dtAuthList, strUserAuth);
                }
            }

            menu.SubMenus = submenuList;
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
                SideMenuDesc = dr["SideMenuDesc"].ToString(),
                MenuType = dr["MenuType"].ToString(),
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
                SideMenuDesc = dr["SideMenuDesc"].ToString(),
                MenuType = dr["MenuType"].ToString(),
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
            
            if (!string.IsNullOrEmpty(model.MenuType))
            {
                menuList = menuList.Where(s => s.MenuType.Equals(model.MenuType, StringComparison.CurrentCultureIgnoreCase)).Select(s => s).ToList();
            }
            //if (model.AuthorityId != null)
            //{
            //    menuList = menuList.Where(s => s.AuthorityId == model.AuthorityId).Select(s => s).ToList();
            //}
            return menuList;
        }

        public DataTable GetClassList(string themeType)
        {
            switch (themeType)
            {
                case "0":
                    return GetClassList();
            }


            var classTable = new DataTable();
            classTable.Columns.Add("Class");
            classTable.Columns.Add("ClassName");

            classTable.Rows.Add("fa fa-user fa-fw", "用户");
            classTable.Rows.Add("fa fa-sign-out fa-fw", "登出");

            return classTable;
        }

        public DataTable GetClassList()
        {
            var classTable = new DataTable();
            classTable.Columns.Add("Class");
            classTable.Columns.Add("ClassName");
            classTable.Columns.Add("ClassImgUrl");
            classTable.Rows.Add("icon-adjust", "");
            classTable.Rows.Add("icon-asterisk", "");
            classTable.Rows.Add("icon-ban-circle", "");
            classTable.Rows.Add("icon-bar-chart", "");
            classTable.Rows.Add("icon-barcode", "");
            classTable.Rows.Add("icon-beaker", "");
            classTable.Rows.Add("icon-beer", "");
            classTable.Rows.Add("icon-bell", "");
            classTable.Rows.Add("icon-bell-alt", "");
            classTable.Rows.Add("icon-bolt", "");
            classTable.Rows.Add("icon-book", "");
            classTable.Rows.Add("icon-bookmark", "");
            classTable.Rows.Add("icon-bookmark-empty", "");
            classTable.Rows.Add("icon-briefcase", "");
            classTable.Rows.Add("icon-bullhorn", "");
            classTable.Rows.Add("icon-calendar", "");
            classTable.Rows.Add("icon-camera", "");
            classTable.Rows.Add("icon-camera-retro", "");
            classTable.Rows.Add("icon-certificate", "");
            classTable.Rows.Add("icon-check", "");
            classTable.Rows.Add("icon-check-empty", "");
            classTable.Rows.Add("icon-circle", "");
            classTable.Rows.Add("icon-circle-blank", "");
            classTable.Rows.Add("icon-cloud", "");
            classTable.Rows.Add("icon-cloud-download", "");
            classTable.Rows.Add("icon-cloud-upload", "");
            classTable.Rows.Add("icon-coffee", "");
            classTable.Rows.Add("icon-cog", "");
            classTable.Rows.Add("icon-cogs", "");
            classTable.Rows.Add("icon-comment", "");
            classTable.Rows.Add("icon-comment-alt", "");
            classTable.Rows.Add("icon-comments", "");
            classTable.Rows.Add("icon-comments-alt", "");
            classTable.Rows.Add("icon-credit-card", "");
            classTable.Rows.Add("icon-dashboard", "");
            classTable.Rows.Add("icon-desktop", "");
            classTable.Rows.Add("icon-download", "");
            classTable.Rows.Add("icon-download-alt", "");
            classTable.Rows.Add("icon-edit", "");
            classTable.Rows.Add("icon-envelope", "");
            classTable.Rows.Add("icon-envelope-alt", "");
            classTable.Rows.Add("icon-exchange", "");
            classTable.Rows.Add("icon-exclamation-sign", "");
            classTable.Rows.Add("icon-external-link", "");
            classTable.Rows.Add("icon-eye-close", "");
            classTable.Rows.Add("icon-eye-open", "");
            classTable.Rows.Add("icon-facetime-video", "");
            classTable.Rows.Add("icon-fighter-jet", "");
            classTable.Rows.Add("icon-film", "");
            classTable.Rows.Add("icon-filter", "");
            classTable.Rows.Add("icon-fire", "");
            classTable.Rows.Add("icon-flag", "");
            classTable.Rows.Add("icon-folder-close", "");
            classTable.Rows.Add("icon-folder-open", "");
            classTable.Rows.Add("icon-folder-close-alt", "");
            classTable.Rows.Add("icon-folder-open-alt", "");
            classTable.Rows.Add("icon-food", "");
            classTable.Rows.Add("icon-gift", "");
            classTable.Rows.Add("icon-glass", "");
            classTable.Rows.Add("icon-globe", "");
            classTable.Rows.Add("icon-group", "");
            classTable.Rows.Add("icon-hdd", "");
            classTable.Rows.Add("icon-headphones", "");
            classTable.Rows.Add("icon-heart", "");
            classTable.Rows.Add("icon-heart-empty", "");
            classTable.Rows.Add("icon-home", "");
            classTable.Rows.Add("icon-inbox", "");
            classTable.Rows.Add("icon-info-sign", "");
            classTable.Rows.Add("icon-key", "");
            classTable.Rows.Add("icon-leaf", "");
            classTable.Rows.Add("icon-laptop", "");
            classTable.Rows.Add("icon-legal", "");
            classTable.Rows.Add("icon-lemon", "");
            classTable.Rows.Add("icon-lightbulb", "");
            classTable.Rows.Add("icon-lock", "");
            classTable.Rows.Add("icon-unlock", "");
            classTable.Rows.Add("icon-magic", "");
            classTable.Rows.Add("icon-magnet", "");
            classTable.Rows.Add("icon-map-marker", "");
            classTable.Rows.Add("icon-minus", "");
            classTable.Rows.Add("icon-minus-sign", "");
            classTable.Rows.Add("icon-mobile-phone", "");
            classTable.Rows.Add("icon-money", "");
            classTable.Rows.Add("icon-move", "");
            classTable.Rows.Add("icon-music", "");
            classTable.Rows.Add("icon-off", "");
            classTable.Rows.Add("icon-ok", "");
            classTable.Rows.Add("icon-ok-circle", "");
            classTable.Rows.Add("icon-ok-sign", "");
            classTable.Rows.Add("icon-pencil", "");
            classTable.Rows.Add("icon-picture", "");
            classTable.Rows.Add("icon-plane", "");
            classTable.Rows.Add("icon-plus", "");
            classTable.Rows.Add("icon-plus-sign", "");
            classTable.Rows.Add("icon-print", "");
            classTable.Rows.Add("icon-pushpin", "");
            classTable.Rows.Add("icon-qrcode", "");
            classTable.Rows.Add("icon-question-sign", "");
            classTable.Rows.Add("icon-quote-left", "");
            classTable.Rows.Add("icon-quote-right", "");
            classTable.Rows.Add("icon-random", "");
            classTable.Rows.Add("icon-refresh", "");
            classTable.Rows.Add("icon-remove", "");
            classTable.Rows.Add("icon-remove-circle", "");
            classTable.Rows.Add("icon-remove-sign", "");
            classTable.Rows.Add("icon-reorder", "");
            classTable.Rows.Add("icon-reply", "");
            classTable.Rows.Add("icon-resize-horizontal", "");
            classTable.Rows.Add("icon-resize-vertical", "");
            classTable.Rows.Add("icon-retweet", "");
            classTable.Rows.Add("icon-road", "");
            classTable.Rows.Add("icon-rss", "");
            classTable.Rows.Add("icon-screenshot", "");
            classTable.Rows.Add("icon-search", "");
            classTable.Rows.Add("icon-share", "");
            classTable.Rows.Add("icon-share-alt", "");
            classTable.Rows.Add("icon-shopping-cart", "");
            classTable.Rows.Add("icon-signal", "");
            classTable.Rows.Add("icon-signin", "");
            classTable.Rows.Add("icon-signout", "");
            classTable.Rows.Add("icon-sitemap", "");
            classTable.Rows.Add("icon-sort", "");
            classTable.Rows.Add("icon-sort-down", "");
            classTable.Rows.Add("icon-sort-up", "");
            classTable.Rows.Add("icon-spinner", "");
            classTable.Rows.Add("icon-star", "");
            classTable.Rows.Add("icon-star-empty", "");
            classTable.Rows.Add("icon-star-half", "");
            classTable.Rows.Add("icon-tablet", "");
            classTable.Rows.Add("icon-tag", "");
            classTable.Rows.Add("icon-tags", "");
            classTable.Rows.Add("icon-tasks", "");
            classTable.Rows.Add("icon-thumbs-down", "");
            classTable.Rows.Add("icon-thumbs-up", "");
            classTable.Rows.Add("icon-time", "");
            classTable.Rows.Add("icon-tint", "");
            classTable.Rows.Add("icon-trash", "");
            classTable.Rows.Add("icon-trophy", "");
            classTable.Rows.Add("icon-truck", "");
            classTable.Rows.Add("icon-umbrella", "");
            classTable.Rows.Add("icon-upload", "");
            classTable.Rows.Add("icon-upload-alt", "");
            classTable.Rows.Add("icon-user", "");
            classTable.Rows.Add("icon-user-md", "");
            classTable.Rows.Add("icon-volume-off", "");
            classTable.Rows.Add("icon-volume-down", "");
            classTable.Rows.Add("icon-volume-up", "");
            classTable.Rows.Add("icon-warning-sign", "");
            classTable.Rows.Add("icon-wrench", "");
            classTable.Rows.Add("icon-zoom-in", "");
            classTable.Rows.Add("icon-zoom-out", "");
            classTable.Rows.Add("icon-file", "");
            classTable.Rows.Add("icon-file-alt", "");
            classTable.Rows.Add("icon-cut", "");
            classTable.Rows.Add("icon-copy", "");
            classTable.Rows.Add("icon-paste", "");
            classTable.Rows.Add("icon-save", "");
            classTable.Rows.Add("icon-undo", "");
            classTable.Rows.Add("icon-repeat", "");
            classTable.Rows.Add("icon-text-height", "");
            classTable.Rows.Add("icon-text-width", "");
            classTable.Rows.Add("icon-align-left", "");
            classTable.Rows.Add("icon-align-center", "");
            classTable.Rows.Add("icon-align-right", "");
            classTable.Rows.Add("icon-align-justify", "");
            classTable.Rows.Add("icon-indent-left", "");
            classTable.Rows.Add("icon-indent-right", "");
            classTable.Rows.Add("icon-font", "");
            classTable.Rows.Add("icon-bold", "");
            classTable.Rows.Add("icon-italic", "");
            classTable.Rows.Add("icon-strikethrough", "");
            classTable.Rows.Add("icon-underline", "");
            classTable.Rows.Add("icon-link", "");
            classTable.Rows.Add("icon-paper-clip", "");
            classTable.Rows.Add("icon-columns", "");
            classTable.Rows.Add("icon-table", "");
            classTable.Rows.Add("icon-th-large", "");
            classTable.Rows.Add("icon-th", "");
            classTable.Rows.Add("icon-th-list", "");
            classTable.Rows.Add("icon-list", "");
            classTable.Rows.Add("icon-list-ol", "");
            classTable.Rows.Add("icon-list-ul", "");
            classTable.Rows.Add("icon-list-alt", "");
            classTable.Rows.Add("icon-angle-left", "");
            classTable.Rows.Add("icon-angle-right", "");
            classTable.Rows.Add("icon-angle-up", "");
            classTable.Rows.Add("icon-angle-down", "");
            classTable.Rows.Add("icon-arrow-down", "");
            classTable.Rows.Add("icon-arrow-left", "");
            classTable.Rows.Add("icon-arrow-right", "");
            classTable.Rows.Add("icon-arrow-up", "");
            classTable.Rows.Add("icon-caret-down", "");
            classTable.Rows.Add("icon-caret-left", "");
            classTable.Rows.Add("icon-caret-right", "");
            classTable.Rows.Add("icon-caret-up", "");
            classTable.Rows.Add("icon-chevron-down", "");
            classTable.Rows.Add("icon-chevron-left", "");
            classTable.Rows.Add("icon-chevron-right", "");
            classTable.Rows.Add("icon-chevron-up", "");
            classTable.Rows.Add("icon-circle-arrow-down", "");
            classTable.Rows.Add("icon-circle-arrow-left", "");
            classTable.Rows.Add("icon-circle-arrow-right", "");
            classTable.Rows.Add("icon-circle-arrow-up", "");
            classTable.Rows.Add("icon-double-angle-left", "");
            classTable.Rows.Add("icon-double-angle-right", "");
            classTable.Rows.Add("icon-double-angle-up", "");
            classTable.Rows.Add("icon-double-angle-down", "");
            classTable.Rows.Add("icon-hand-down", "");
            classTable.Rows.Add("icon-hand-left", "");
            classTable.Rows.Add("icon-hand-right", "");
            classTable.Rows.Add("icon-hand-up", "");
            classTable.Rows.Add("icon-circle", "");
            classTable.Rows.Add("icon-circle-blank", "");
            classTable.Rows.Add("icon-play-circle", "");
            classTable.Rows.Add("icon-play", "");
            classTable.Rows.Add("icon-pause", "");
            classTable.Rows.Add("icon-stop", "");
            classTable.Rows.Add("icon-step-backward", "");
            classTable.Rows.Add("icon-fast-backward", "");
            classTable.Rows.Add("icon-backward", "");
            classTable.Rows.Add("icon-forward", "");
            classTable.Rows.Add("icon-fast-forward", "");
            classTable.Rows.Add("icon-step-forward", "");
            classTable.Rows.Add("icon-eject", "");
            classTable.Rows.Add("icon-fullscreen", "");
            classTable.Rows.Add("icon-resize-full", "");
            classTable.Rows.Add("icon-resize-small", "");
            classTable.Rows.Add("icon-phone", "");
            classTable.Rows.Add("icon-phone-sign", "");
            classTable.Rows.Add("icon-facebook", "");
            classTable.Rows.Add("icon-facebook-sign", "");
            classTable.Rows.Add("icon-twitter", "");
            classTable.Rows.Add("icon-twitter-sign", "");
            classTable.Rows.Add("icon-github", "");
            classTable.Rows.Add("icon-github-alt", "");
            classTable.Rows.Add("icon-github-sign", "");
            classTable.Rows.Add("icon-linkedin", "");
            classTable.Rows.Add("icon-linkedin-sign", "");
            classTable.Rows.Add("icon-pinterest", "");
            classTable.Rows.Add("icon-pinterest-sign", "");
            classTable.Rows.Add("icon-google-plus", "");
            classTable.Rows.Add("icon-google-plus-sign", "");
            classTable.Rows.Add("icon-sign-blank", "");
            classTable.Rows.Add("icon-ambulance", "");
            classTable.Rows.Add("icon-beaker", "");
            classTable.Rows.Add("icon-h-sign", "");
            classTable.Rows.Add("icon-hospital", "");
            classTable.Rows.Add("icon-medkit", "");
            classTable.Rows.Add("icon-plus-sign-alt", "");
            classTable.Rows.Add("icon-stethoscope", "");
            classTable.Rows.Add("icon-user-md", "");

            //classTable.Rows.Add("sa-side-sysMgm", "系统管理", "/img/SystemMgm.png");
            //classTable.Rows.Add("sa-side-deviceMap", "分布图", "/img/DeviceMap.png");
            //classTable.Rows.Add("sa-side-cyDisplay", "功能展示", "/img/CyDisplay.png");

            return classTable;
        }



        public DataTable GetSuperAdminClassList()
        {
            var classTable = new DataTable();
            classTable.Columns.Add("Class");
            classTable.Columns.Add("ClassName");

            classTable.Rows.Add("fa fa-user fa-fw", "用户");
            classTable.Rows.Add("fa fa-sign-out fa-fw", "登出");

            return classTable;
        }


        public DataTable GetParentMenuList()
        {
            var dtParentMenu = new DataTable();
            dtParentMenu.Columns.Add("id");
            dtParentMenu.Columns.Add("text");

            var sideMenuTable = _dalSideMenu.QuerySideMenu("'0'");
            foreach (DataRow sideMenu in sideMenuTable.Rows)
            {
                dtParentMenu.Rows.Add(sideMenu["Id"], sideMenu["Name"] + "[" + sideMenu["SideMenuDesc"] + "]");
            }
            return dtParentMenu;
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
                            MenuType = dr["MenuType"].ToString(),
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




        public IList<SideMenuModel> GetMenuList(UserModel user,string themeType="0")
        {
            var authList = _dalAuthority.GetAuthorityList();
            var classList = GetClassList();
            var empWithAuth = _bllEmployee.GetEmployeeWithAuthority(user.Id.ToString());
            var userAuth =
                empWithAuth.EmpRoleList.Select(
                    empRoleModel =>
                        empWithAuth.RoleList.Where(r => r.Id == empRoleModel.RoleId)
                            .Select(r => r.AuthorityCode)
                            .FirstOrDefault())
                    .Aggregate(empWithAuth.AuthorityCode,
                        CommonUtil.AppendAuthorityCode);

            var sideMenu = _dalSideMenu.QuerySideMenu("'0'");

            var menuList =
                sideMenu.AsEnumerable()
                    .Where(dr => dr["ParentId"] == null || dr["ParentId"].ToString() == string.Empty && CommonUtil.ExistAuthorityCode(userAuth, dr["AuthorityCode"].ToString()))
                    .Select(dr => new SideMenuModel
                    {
                        AuthorityCode = dr["AuthorityCode"].ToString(),
                        MenuType = dr["MenuType"].ToString(),
                        SideMenuDesc = dr["SideMenuDesc"].ToString(),
                        Class = dr["Class"].ToString(),
                        ClassName = classList.AsEnumerable().Where(a => a["Class"].ToString().ToLower() == dr["Class"].ToString().ToLower()).Select(a => a["ClassName"].ToString()).ToList().FirstOrDefault(),
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        SortNumber = dr["SortNumber"].ConvertToNullable<Int32>(),
                        Url = dr["Url"].ToString(),
                        AuthorityName = authList.AsEnumerable().Where(a => a["AuthorityCode"].ToString().ToLower() == dr["AuthorityCode"].ToString().ToLower()).Select(a => a["Name"].ToString()).ToList().FirstOrDefault(),
                    }).ToList();


            foreach (var menu in menuList)
            {
                AppendSubMenus(menu, sideMenu, classList, authList,userAuth);
            }

            return menuList;


            //var sideMenuList = sideMenuSet.Tables["SideMenu"].AsEnumerable()
            //    .Where(dr => CommonUtil.ExistAuthorityCode(userAuth,dr["AuthorityCode"].ToString()))
            //    .Select(dr => new SideMenuListModel
            //    {
            //        //AuthorityId = Convert.ToInt32(dr["AuthorityId"]),
            //        AuthorityCode=dr["AuthorityCode"].ToString(),
            //        Class = dr["Class"].ToString(),
            //        Id = Convert.ToInt32(dr["Id"]),
            //        Name = dr["Name"].ToString(),
            //        Url = dr["Url"].ToString(),
            //        SortNumber = Convert.ToInt32(dr["SortNumber"]),
            //        SubMenus = sideMenuSet.Tables["SubMenu"].AsEnumerable()
            //            .Where(d => Convert.ToInt32(dr["Id"]) == Convert.ToInt32(d["ParentId"])
            //                        &&
            //                        (CommonUtil.ExistAuthorityCode(user.AuthorityCode,d["AuthorityCode"].ToString())))
            //            .Select(d => new SideMenuModel
            //            {
            //                //AuthorityId = Convert.ToInt32(d["AuthorityId"]),
            //                AuthorityCode = d["AuthorityCode"].ToString(),
            //                Class = d["Class"].ToString(),
            //                Id = Convert.ToInt32(d["Id"]),
            //                Name = d["Name"].ToString(),
            //                Url = d["Url"].ToString(),
            //                SortNumber = Convert.ToInt32(d["SortNumber"]),
            //                ParentId = Convert.ToInt32(d["ParentId"])
            //            }).OrderBy(s => s.SortNumber).ToList()
            //    }).OrderBy(sm => sm.SortNumber).ToList();
            //return sideMenuList;
        }


        public string GetSideMenuIdByAuthorityCode(string userId,string authorityCode)
        {
            var userAuth = _bllEmployee.GetUserAuthorityCode(userId);
            if (!CommonUtil.ExistAuthorityCode(userAuth, authorityCode))
            {
                return string.Empty;
            }

            var dtSideMenu = _dalSideMenu.QuerySideMenu(string.Empty);
            return
                dtSideMenu.AsEnumerable()
                    .Where(dr => dr["AuthorityCode"].ToString() == authorityCode)
                    .Select(dr => dr["Id"].ToString())
                    .FirstOrDefault();
        }


        public string GetAuthorityCodeBySideMenuId(string userId, string sideMenuId)
        {
            var dtSideMenu = _dalSideMenu.QuerySideMenu(string.Empty);
            var sideMenuAuth = dtSideMenu.AsEnumerable().Where(dr => dr["Id"].ToString() == sideMenuId)
                .Select(dr => dr["AuthorityCode"].ToString()).FirstOrDefault();

            if (string.IsNullOrEmpty(sideMenuAuth))
            {
                return string.Empty;
            }

            var userAuth = _bllEmployee.GetUserAuthorityCode(userId);
            return CommonUtil.ExistAuthorityCode(userAuth, sideMenuAuth) ? sideMenuAuth : string.Empty;
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
