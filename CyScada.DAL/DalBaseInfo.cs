using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalBaseInfo
    {
        public DataTable GetMachines()
        {
            var sqlBuilder = new StringBuilder(@"SELECT * FROM lonni_f.ZQ_Machine WITH(NOLOCK) ");
            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sqlBuilder.ToString());
            return result == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }
    }
}
