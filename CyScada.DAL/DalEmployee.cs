using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.DAL;

namespace CyScada.BLL
{
    public class DalEmployee
    {
        public DataTable GetEmployeeList(Hashtable filterModel)
        {
            var sqlBuilder = new StringBuilder(@"SELECT * FROM lonni_f.ZQ_Employees WITH(NOLOCK) where 1=1 ");
            foreach (DictionaryEntry pair in filterModel)
            {
                var value = pair.Value is string
                    ? "'" + pair.Value + "'"
                    : pair.Value is DateTime
                        ? ((DateTime) pair.Value).ToString("yyyy-MM-dd HH:mm:ss")
                        : pair.Value == null
                            ? string.Empty
                            : pair.Value.ToString();

                sqlBuilder.AppendFormat(@" AND {0}={1}", pair.Key.ToString(), value);
            }

            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sqlBuilder.ToString());
            return result == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }
    }
}
