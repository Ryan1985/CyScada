using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalLogin
    {
        /// <summary>
        /// 根据用户名密码获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DataTable GetUser(string userName,string password)
        {

            var sql = string.Format(@"SELECT TOP 50 * 
FROM dbo.dt_Administrator WITH(NOLOCK)
WHERE UserName='{0}' AND UserPwd = '{1}'", userName, password);

            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 根据USERID获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataTable GetUser(string UserId)
        {
            var sql = string.Format(@"SELECT TOP 50 * 
FROM dbo.dt_Administrator WITH(NOLOCK)
WHERE UserId={0}", UserId);

            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
