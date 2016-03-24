using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CyScada.DAL
{
    public class DalEquipment
    {
        public DataTable GetEquipment(string equipmentId)
        {
            var sql = string.Format(@"  select t_Equipment.EquipmentName ,tt.*
  from t_Equipment
  left join 
  ( select 
  DTUID,
   CONVERT(varchar(19), sj, 20)  as '时间'
      ,round([TLYLM],2) as '推力压力(MPa)'
      ,round([TLYLT],2) as '推力压力(T)'
      ,round([LLYLM],2) as '拉力压力(MPa)'
      ,round([LLYLT],2) as '拉力压力(T)'
      ,[TLDW] as '推拉档位'
      ,round([ZZYLM],2) as '正转压力(MPa)'
      ,round([ZZYLN],2) as '正转压力(T)'
      ,round([FZYLM] ,2)as '反转压力(MPa)'
      ,round([FZYLN] ,2)as '反转压力(T)'
      ,[HZDW] as '回转档位'
      ,round([NJYL] ,2)as '泥浆压力(MPa)'
      ,round([NJLL] ,2)as '泥浆流量(M³/min)'
FROM t_zhisdata 
inner join (
  select max(sj) as lastTime from t_zhisdata where DTUID = '{0}') T
  on t_zhisdata.sj = T.lastTime and t_zhisdata.DTUID='{0}') tt on t_Equipment.EquipmentId = tt.DTUID
  where t_Equipment.EquipmentId = '{0}'", equipmentId);

            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            return ds.Tables[0];

        }

        /// <summary>
        /// 根据用户ID获取相关的设备列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetEquipmentList(string userId)
        {
            var sql = string.Format(@"select t_Equipment.EquipmentId,t_Equipment.EquipmentName,t_Equipment.Description
from t_EquipmentEmployees
inner join t_Equipment on t_Equipment.EquipmentId = t_EquipmentEmployees.EquipmentId
where t_EquipmentEmployees.EmployeesId = {0}", userId);

            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

            return ds.Tables[0];


        }

        /// <summary>
        /// 获取所有设备列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetEquipmentList()
        {
            var sql = @"select t_Equipment.EquipmentId,t_Equipment.EquipmentName,t_Equipment.Description from t_Equipment";

            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

            return ds.Tables[0];


        }
    }
}
