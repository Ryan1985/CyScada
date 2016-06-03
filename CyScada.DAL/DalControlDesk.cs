using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalControlDesk
    {

        public DataSet GetItemList(string sideMenuId)
        {
            var sql =
                string.Format(@"SELECT * FROM lonni_f.ZQ_SideMenu WITH(NOLOCK) WHERE MenuType = 1  AND ParentId = " +
                              sideMenuId + ";SELECT * FROM lonni_f.ZQ_SideMenu WITH(NOLOCK) WHERE Id = " + sideMenuId);
            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            return result == null || result.Tables.Count == 0 ? null : result;
        }
    }
}
