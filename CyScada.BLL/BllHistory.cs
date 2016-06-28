using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllHistory
    {
        private DalAliyun _dalAliyun = new DalAliyun();
        private DalMachineTag _dalMachineTag = new DalMachineTag();
        private BllBaseInfo _bllBaseInfo = new BllBaseInfo();
        private BllSideMenu _bllSideMenu = new BllSideMenu();

        public BllSideMenu BllSideMenu
        {
            get { return _bllSideMenu; }
            set { _bllSideMenu = value; }
        }

        public DalMachineTag DalMachineTag
        {
            get { return _dalMachineTag; }
            set { _dalMachineTag = value; }
        }

        public BllBaseInfo BLLBaseInfo
        {
            get { return _bllBaseInfo; }
            set { _bllBaseInfo = value; }
        }

        public DalAliyun DalAliyun
        {
            get { return _dalAliyun; }
            set { _dalAliyun = value; }
        }


        public DataTable GetMachineTagList(string sideMenuId, string userId,string machineId)
        {
            var result = new DataTable();
            result.Columns.Add("text");
            result.Columns.Add("id");

            var parentId = _bllSideMenu.GetMenuListFlat().Where(s => s.Id.Value.ToString() == sideMenuId.ToString()).Select(s => s.ParentId.Value).FirstOrDefault();



            var baseInfo = _bllBaseInfo.GetBaseInfo(parentId.ToString(), userId, machineId);
            if (baseInfo == null)
                return result;

            var dtTag = _dalMachineTag.GetTags(baseInfo.Select(b => b.Id.ToString()).ToList());
            for (var i = 0; i < dtTag.Rows.Count; i++)
            {
                result.Rows.Add(dtTag.Rows[i]["TagName"].ToString(), dtTag.Rows[i]["DeviceName"].ToString());
            }

            return result;
        }

        public IEnumerable<HistoryModel> GetHistory(HistoryQueryModel model)
        {
            var htFilters = new Hashtable();
            if (!string.IsNullOrEmpty(model.DeviceName))
            {
                htFilters.Add("DeviceName", model.DeviceName);
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                htFilters.Add("EndDate", model.EndDate);
            }
            if (!string.IsNullOrEmpty(model.MachineId))
            {
                htFilters.Add("MachineId", model.MachineId);
            }
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                htFilters.Add("StartDate", model.StartDate);
            }

            var dtHistory = _dalAliyun.GetHistory(htFilters);
            return dtHistory.AsEnumerable().Select(dr => new HistoryModel
            {
                Company = dr["Company"].ToString(),
                Id = dr["Id"].ToString(),
                MachineName = dr["Name"].ToString(),
                TagName = dr["TagName"].ToString(),
                TimeStamp = dr["TimeStamp"].ToString(),
                Value = dr["Value"].ToString(),
                WorkSite = dr["WorkSite"].ToString()
            }).ToList();
        }
    }
}
