using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.DAL;

namespace CyScada.BLL
{
    public class BllHistory
    {
        private DalMachineTag _dalMachineTag = new DalMachineTag();
        private BllBaseInfo _bllBaseInfo = new BllBaseInfo();

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


        public DataTable GetMachineTagList(string sideMenuId, string userId)
        {
            var result = new DataTable();
            result.Columns.Add("TagName");
            result.Columns.Add("DeviceName");

            var baseInfo = _bllBaseInfo.GetBaseInfo(sideMenuId, userId);
            if (baseInfo == null)
                return result;

            var dtTag = _dalMachineTag.GetTags(baseInfo.Id);
            for (var i = 0; i < dtTag.Rows.Count; i++)
            {
                result.Rows.Add(dtTag.Rows[i]["TagName"].ToString(), dtTag.Rows[i]["DeviceName"].ToString());
            }

            return result;
        }
    }
}
