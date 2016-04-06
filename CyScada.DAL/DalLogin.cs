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

            var sql = string.Format(@"SELECT * 
FROM lonni_f.ZQ_Employees WITH(NOLOCK)
WHERE LoginName='{0}' AND Password = '{1}'", userName, password);

            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 根据USERID获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataTable GetUser(int UserId)
        {
            var sql = string.Format(@"SELECT * 
FROM lonni_f.ZQ_Employees WITH(NOLOCK)
WHERE Id={0}", UserId);

            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
