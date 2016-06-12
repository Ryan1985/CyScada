using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalMachineTag
    {
        public DataTable GetTags(string machineId)
        {
            var sql = string.Format(@"select * from Zq_machinetags where machineId="
                                    + machineId);
            var dsResult= SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(),
                CommandType.Text, sql);
            if (dsResult.Tables.Count == 0)
            {
                return null;
            }

            return dsResult.Tables[0];
        }

        public DataTable GetTags(IList<string> machineIdList)
        {
            var machineString = machineIdList.Aggregate(string.Empty, (current, s) => current + s + ",");
            if (string.IsNullOrEmpty(machineString))
            {
                return null;
            }
            machineString = machineString.TrimEnd(',');
            var sql = string.Format(@"select * from Zq_machinetags where machineId in (" + machineString + ")");
            var dsResult = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(),
                CommandType.Text, sql);
            if (dsResult.Tables.Count == 0)
            {
                return null;
            }

            return dsResult.Tables[0];
        }
    }
}
