using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalControlDesk
    {

        public DataTable GetItemList()
        {
            var sql = string.Format(@"select * FROM lonni_f.ZQ_Authorities WITH(NOLOCK) WHERE AuthorityType=1");
            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            return result == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }
    }
}
