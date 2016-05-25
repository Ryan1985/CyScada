using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalControlDesk
    {

        public DataTable GetItemList(string sideMenuId)
        {
            var sql =
                string.Format(@"SELECT * FROM lonni_f.ZQ_SideMenu WITH(NOLOCK) WHERE MenuType = 1  AND ParentId = " +
                              sideMenuId);
            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            return result == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }
    }
}
