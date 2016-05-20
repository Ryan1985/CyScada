using System;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllLogin
    {
        protected DalLogin DalLogin = new DalLogin();
        public string Login(UserModel user)
        {
            var dtUser = DalLogin.GetUser(user.LoginName, user.Password);
            if (dtUser.Rows.Count == 0)
            {
                return "错误的用户名或者密码";
            }

            user.Id = Convert.ToInt32(dtUser.Rows[0]["Id"]);
            user.Name = dtUser.Rows[0]["Name"].ToString();
            user.AuthorityCode = dtUser.Rows[0]["AuthorityCode"].ToString();
            
            return string.Empty;
        }

        public UserModel GetUserInfo(UserModel user)
        {
            var dtUser = DalLogin.GetUser(user.Id);
            if (dtUser.Rows.Count == 0)
                return user;

            user.LoginName = dtUser.Rows[0]["LoginName"].ToString();
            user.Password = dtUser.Rows[0]["Password"].ToString();
            user.Name = dtUser.Rows[0]["Name"].ToString();
            user.AuthorityCode = dtUser.Rows[0]["AuthorityCode"].ToString();


            return user;
        }

    }
}
