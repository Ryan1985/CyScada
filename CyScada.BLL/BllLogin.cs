using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllLogin
    {
        protected DalLogin DalLogin = new DalLogin();
        public string Login(UserModel user)
        {
            var dtUser = DalLogin.GetUser(user.UserName, user.Password);
            if (dtUser.Rows.Count == 0)
            {
                return "错误的用户名或者密码";
            }



            user.UserId = dtUser.Rows[0]["userId"].ToString();
            user.ReadName = dtUser.Rows[0]["ReadName"].ToString();
            user.UserType = dtUser.Rows[0]["UserType"].ToString();
            
            return string.Empty;
        }


    }
}
