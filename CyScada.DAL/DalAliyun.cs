using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalAliyun
    {

        public DataTable GetHistory(Hashtable htFilters)
        {
            var sb = new StringBuilder(@"SELECT TOP 100 aliyun.[_VALUE] as Value,aliyun.[_TIMESTAMP] as TimeStamp,
ZQ_Machine.Id,ZQ_Machine.Company,
ZQ_Machine.WorkSite,ZQ_Machine.Name,
ZQ_MachineTags.TagName,
ZQ_MachineTags.DeviceName
FROM lonni_f.ZQ_Machine WITH(NOLOCK)
INNER JOIN lonni_f.ZQ_MachineTags WITH(NOLOCK) ON ZQ_MachineTags.MachineId = ZQ_Machine.Id
INNER JOIN lonni_f.aliyun WITH(NOLOCK) ON aliyun.[_NAME]=ZQ_MachineTags.DeviceName
WHERE 1=1 ");

            if (htFilters.ContainsKey("DeviceName"))
            {
                sb.Append("AND ZQ_MachineTags.DeviceName='" + htFilters["DeviceName"] + "'");
            }
            if (htFilters.ContainsKey("EndDate"))
            {
                sb.Append("AND aliyun.[_TIMESTAMP]<='" + htFilters["EndDate"] + "'");
            }
            if (htFilters.ContainsKey("MachineId"))
            {
                sb.Append("AND ZQ_Machine.Id='" + htFilters["MachineId"] + "'");
            }
            if (htFilters.ContainsKey("StartDate"))
            {
                sb.Append("AND aliyun.[_TIMESTAMP]>='" + htFilters["StartDate"] + "'");
            }

            sb.Append(" ORDER BY aliyun.[_TIMESTAMP] desc");


            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sb.ToString());
            return result == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }
    }
}
