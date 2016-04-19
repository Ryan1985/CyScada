using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalEmpRoles
    {
        public DataTable GetEmpRoleList()
        {
            return GetEmpRoleList(new Hashtable());
        }



        public DataTable GetEmpRoleList(Hashtable filterModel)
        {
            var sqlBuilder = new StringBuilder(@"SELECT * FROM lonni_f.ZQ_EmpRoles WITH(NOLOCK) where 1=1 ");
            foreach (DictionaryEntry pair in filterModel)
            {
                var value = pair.Value is string
                    ? "'" + pair.Value + "'"
                    : pair.Value is DateTime
                        ? ((DateTime)pair.Value).ToString("yyyy-MM-dd HH:mm:ss")
                        : pair.Value == null
                            ? string.Empty
                            : pair.Value.ToString();

                sqlBuilder.AppendFormat(@" AND {0}={1}", pair.Key.ToString(), value);
            }

            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sqlBuilder.ToString());
            return result == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }







        public string CreateEmpRoles(Hashtable model)
        {
            var sql = string.Format(@"INSERT INTO lonni_f.ZQ_EmpRoles
        ( emp_id, role_id )
VALUES  ( {0}, -- emp_id - int
          {1}  -- role_id - int
          )", model["UserId"], model["RoleId"]);

            try
            {
                var result = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);

                if (result <= 0)
                {
                    return "添加失败!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }


        public string DeleteEmployee(Hashtable model)
        {
            var sql = string.Format(@"DELETE FROM lonni_f.ZQ_EmpRoles WHERE emp_id={0} AND role_id={1}", model["UserId"], model["RoleId"]);
            try
            {
                var result = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);

                if (result <= 0)
                {
                    return "删除失败!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
    }
}
