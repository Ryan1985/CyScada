using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalRealTimeDisplay
    {




        public DataTable GetTags(string machineId)
        {
            var sql = string.Format(@"SELECT * 
FROM lonni_f.ZQ_MachineTags WITH(NOLOCK)
WHERE MachineId={0}", machineId);
            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text,sql);
            return result == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }
    }
}
