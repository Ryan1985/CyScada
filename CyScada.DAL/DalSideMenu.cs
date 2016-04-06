using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalSideMenu
    {
        /// <summary>
        /// 获取侧边栏项目,返回集合内有两张表(SideMenu:一级目录,SubMenu:二级目录)
        /// </summary>
        /// <returns></returns>
        public DataSet QuerySideMenuSet()
        {
            var sql = string.Format(@"SELECT * FROM lonni_f.ZQ_SideMenu WITH(NOLOCK) WHERE ParentId IS NULL
SELECT * FROM lonni_f.ZQ_SideMenu WITH(NOLOCK) WHERE ParentId IS NOT NULL");

            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (result.Tables.Count != 2)
                return null;

            result.Tables[0].TableName = "SideMenu";
            result.Tables[1].TableName = "SubMenu";

            return result;
        }
    }
}
