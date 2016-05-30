using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllBaseInfo
    {
        private DalBaseInfo _dalBaseInfo = new DalBaseInfo();
        private BllSideMenu _bllSideMenu = new BllSideMenu();

        public DalBaseInfo DalBaseInfo
        {
            get { return _dalBaseInfo; }
            set { _dalBaseInfo = value; }
        }

        public BllSideMenu BLLSideMenu
        {
            get { return _bllSideMenu; }
            set { _bllSideMenu = value; }
        }


        public BaseInfoModel GetBaseInfo(string sideMenuId)
        {
            var sideMenuList = _bllSideMenu.GetMenuListFlat();
            var workSiteAuthorityCode =
                sideMenuList.Where(s => s.Id == int.Parse(sideMenuId)).Select(s => s.AuthorityCode).FirstOrDefault();

            var dtBaseInfo = _dalBaseInfo.GetMachines();
            if (dtBaseInfo == null)
                return null;

            var baseInfo =
                dtBaseInfo.AsEnumerable()
                    .Where(dr => dr["AuthorityCode"].ToString() == workSiteAuthorityCode)
                    .Select(dr => new BaseInfoModel
                    {
                        AuthorityCode = dr["AuthorityCode"].ToString(),
                        CCID = dr["CCID"].ToString(),
                        Company = dr["Company"].ToString(),
                        Description = dr["Description"].ToString(),
                        Id = dr["Id"].ToString(),
                        IMEI = dr["IMEI"].ToString(),
                        Latitude = dr["Latitude"].ToString(),
                        Longitude = dr["Longitude"].ToString(),
                        MachineType = dr["MachineType"].ToString(),
                        Name = dr["Name"].ToString(),
                        Pic = dr["Pic"].ToString(),
                        Status = GetStatusName(dr["Status"].ToString()),
                        WorkSite = dr["WorkSite"].ToString()
                    }).FirstOrDefault();

            return baseInfo;

        }


        private string GetStatusName(string statusCode)
        {
            switch (statusCode)
            {
                case "0":
                    return "离线";
                case "1":
                    return "在线";
                default:
                    return string.Empty;
            }
        }
    }
}
