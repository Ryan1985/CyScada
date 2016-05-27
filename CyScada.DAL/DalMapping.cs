using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalMapping
    {

        public DataTable GetMappingList(string mappingType)
        {
            var sql = @"SELECT * FROM lonni_f.ZQ_MappingTable WITH(NOLOCK) where MappingType='" +
                      mappingType + "' ";

            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            return result == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        public void Create(Hashtable model)
        {
            var sql = string.Format(@"INSERT INTO lonni_f.ZQ_MappingTable
        ( Text, Value, MappingType )
VALUES  ( '{0}', -- Text - varchar(max)
          '{1}', -- Value - varchar(max)
          '{2}'  -- MappingType - varchar(250)
          )", model["Text"], model["Value"], model["MappingType"]);

            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
        }

        public void Modify(Hashtable model)
        {
            var sql = string.Format(@"UPDATE lonni_f.ZQ_MappingTable
SET Text='{0}',Value='{1}',MappingType='{2}'
WHERE Value='{1}' and MappingType='{2}'", model["Text"], model["Value"], model["MappingType"]);

            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
        }
    }
}
