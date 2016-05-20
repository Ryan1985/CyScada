using System;
using System.Collections;
using System.Data;
using System.Text;

namespace CyScada.DAL
{
    public class DalEmployee
    {
        public DataTable GetEmployeeList(Hashtable filterModel)
        {
            var sqlBuilder = new StringBuilder(@"SELECT * FROM lonni_f.ZQ_Employees WITH(NOLOCK) where 1=1 ");
            foreach (DictionaryEntry pair in filterModel)
            {
                //如果是字符串类型，但是是空值，则忽略
                if (pair.Value is string && string.IsNullOrEmpty(pair.Value.ToString())) { continue; }

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


        public string CreateEmployee(Hashtable model)
        {
            var sql = string.Format(@"INSERT INTO lonni_f.ZQ_Employees
        ( Name ,
          LoginName ,
          Password ,
          Description ,
          Authority ,
          AuthorityCode ,
          Code
        )
VALUES  ( '{0}' , -- Name - varchar(100)
          '{1}' , -- LoginName - varchar(50)
          '{2}' , -- Password - varchar(50)
          '{3}' , -- Description - varchar(500)
          0 , -- Authority - bigint
          '' , -- AuthorityCode - varchar(MAX)
          '{4}'  -- Code - varchar(50)
        )", model["Name"], model["LoginName"], model["Password"], model["Description"], model["Code"]);

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

        public string ModifyEmployee(Hashtable model)
        {
            var sql = string.Format(@"UPDATE  lonni_f.ZQ_Employees
SET     Name = '{1}' ,
        LoginName = '{2}' ,
        Password = '{3}' ,
        Description = '{4}' ,
        Code = '{5}',
        Authority={6},
        AuthorityCode='{7}'

WHERE   ID = {0}", model["Id"], model["Name"], model["LoginName"], model["Password"], model["Description"],
                model["Code"], model.ContainsKey("Authority") ? model["Authority"] : 0,
                string.IsNullOrEmpty(model["AuthorityCode"] == null ? string.Empty : model["AuthorityCode"].ToString())
                    ? string.Empty
                    : model["AuthorityCode"].ToString());
            try
            {

                var result = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (result <= 0)
                {
                    return "修改失败!" + result;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public void DeleteEmployee(int id)
        {
            var sql = string.Format(@"DELETE FROM lonni_f.ZQ_Employees WHERE ID={0}", id);
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
        }

    }
}
