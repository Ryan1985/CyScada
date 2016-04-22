using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CyScada.DAL
{
    public class DalSideMenu
    {
        /// <summary>
        /// 获取侧边栏项目,返回集合内有两张表(SideMenu:一级目录,SubMenu:二级目录)
        /// </summary>
        /// <returns></returns>
        public DataSet QuerySideMenuSet()
        {
            var sql = string.Format(@"SELECT * FROM lonni_f.ZQ_SideMenu WITH(NOLOCK) WHERE ParentId IS NULL
SELECT * FROM lonni_f.ZQ_SideMenu WITH(NOLOCK) WHERE ParentId IS NOT NULL");

            var result = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (result.Tables.Count != 2)
                return null;

            result.Tables[0].TableName = "SideMenu";
            result.Tables[1].TableName = "SubMenu";

            return result;
        }

        public string CreateSideMenu(Hashtable model)
        {
            var sql = string.Format(@"INSERT INTO lonni_f.ZQ_SideMenu
        ( Name ,
          AuthorityId ,
          Class ,
          Url ,
          ParentId ,
          SortNumber
        )
VALUES  ( '{0}' , -- Name - varchar(50)
          {1} , -- AuthorityId - bigint
          '{2}' , -- Class - varchar(500)
          '{3}' , -- Url - varchar(500)
          {4} , -- ParentId - int
          {5}  -- SortNumber - int
        )", model["Name"], model["AuthorityId"], model["Class"], model["Url"], model.ContainsKey("ParentId") ? model["ParentId"] : "NULL", model["SortNumber"]);

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

        public string ModifySideMenu(Hashtable model)
        {
            var sql = string.Format(@"UPDATE  lonni_f.ZQ_SideMenu
SET     Name = '{1}' ,
        AuthorityId = '{2}' ,
        Class = '{3}' ,
        Url = '{4}' ,
        ParentId = {5},
        SortNumber={6}
WHERE   ID = {0}", model["Id"], model["Name"], model["AuthorityId"], model["Class"], model["Url"],
                 model.ContainsKey("ParentId")?model["ParentId"]:"NULL", model["SortNumber"]);
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

        public void DeleteSideMenu(int id)
        {
            var sql = string.Format(@"DELETE FROM lonni_f.ZQ_SideMenu WHERE ID={0}", id);
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
        }
    }
}
