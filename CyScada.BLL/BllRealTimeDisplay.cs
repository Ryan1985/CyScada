using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllRealTimeDisplay
    {
        private BllBaseInfo _bllBaseInfo = new BllBaseInfo();
        private DalRealTimeDisplay _dalRealTimeDisplay = new DalRealTimeDisplay();



        public BllBaseInfo BLLBaseInfo
        {
            get { return _bllBaseInfo; }
            set { _bllBaseInfo = value; }
        }

        public DalRealTimeDisplay DalRealTimeDisplay
        {
            get { return _dalRealTimeDisplay; }
            set { _dalRealTimeDisplay = value; }
        }


        public BaseInfoModel GetMachineTags(string sideMenuId, string userId)
        {
            var machine = _bllBaseInfo.GetBaseInfo(sideMenuId, userId);
            if (machine == null)
                return null;

            var dtTags = _dalRealTimeDisplay.GetTags(machine.Id);
            machine.Tags = dtTags.AsEnumerable().Select(dr => new TagItemModel
            {
                Key = dr["TagKey"].ToString(),
                Name = dr["TagName"].ToString(),
                Scale = dr["Scale"].ToString(),
                MaxScale = dr["MaxScale"].ToString(),
                MinScale = dr["MinScale"].ToString()
            }).ToList();


            return machine;
        }
    }
}
