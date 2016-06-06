﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.Common;

namespace CyScada.BLL
{
    public class BllBindList
    {
        private BllEmployee _bllEmployee = new BllEmployee();
        private BllAuthority _bllAuthority = new BllAuthority();
        private BllSideMenu _bllSideMenu = new BllSideMenu();

        public BllEmployee BLLEmployee
        {
            get { return _bllEmployee; }
            set { _bllEmployee = value; }
        }

        public BllAuthority BLLAuthority
        {
            get { return _bllAuthority; }
            set { _bllAuthority = value; }
        }

        public BllSideMenu BLLSideMenu
        {
            get { return _bllSideMenu; }
            set { _bllSideMenu = value; }
        }


        public DataTable GetUserList()
        {
            var dtUser = new DataTable();
            dtUser.Columns.Add("id");
            dtUser.Columns.Add("Name");

            var userList = _bllEmployee.GetEmployeeList();
            foreach (var user in userList)
            {
                dtUser.Rows.Add(user.Id, user.Name);
            }
            return dtUser;
        }


        public DataTable GetParentMenuList()
        {
            return _bllSideMenu.GetParentMenuList();
        }



        public DataTable GetClassList()
        {
            var dtClass = new DataTable();
            dtClass.Columns.Add("id");
            dtClass.Columns.Add("text");
            dtClass.Columns.Add("img");

            var classTable = _bllSideMenu.GetClassList();//&lt;i class='{0}'&gt;&lt;/i&gt;
            foreach (DataRow dr in classTable.Rows)
            {
                dtClass.Rows.Add(dr["Class"], dr["ClassName"], dr["ClassImgUrl"]);
            }


            return dtClass;
        }

        public DataTable GetAuthorityList()
        {
            var dtAuth = new DataTable();
            dtAuth.Columns.Add("id");
            dtAuth.Columns.Add("text");

            var authList = _bllAuthority.GetAuthorityList().ToList();
            foreach (var user in authList)
            {
                dtAuth.Rows.Add(user.AuthorityCode, user.Name + "[" + user.Description + "]");
            }

            return dtAuth;
        }



        public DataTable GetSideMenuAuthorityList()
        {
            var dtAuth = new DataTable();
            dtAuth.Columns.Add("id");
            dtAuth.Columns.Add("text");

            var authList = _bllAuthority.GetAuthorityList().ToList();
            foreach (var user in authList)
            {
                if (user.AuthorityType == "0")//只取目录型权限
                    dtAuth.Rows.Add(user.AuthorityCode, user.Name);
            }

            return dtAuth;
        }



        public DataTable GetBindList(string bindType,object param)
        {
            var methodName = "Get" + bindType;
            var method = GetType().GetMethod(methodName);
            if (method == null)
            {
                return null;
            }

            var parameters = method.GetParameters();
            if (parameters.Any())
            {
                return method.Invoke(this, new[] { param }) as DataTable;
            }
            return method.Invoke(this, null) as DataTable;
        }

    }
}
