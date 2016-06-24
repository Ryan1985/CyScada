using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.Common;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllBaseInfo
    {
        private DalBaseInfo _dalBaseInfo = new DalBaseInfo();
        private BllSideMenu _bllSideMenu = new BllSideMenu();
        private BllEmployee _bllEmployee = new BllEmployee();

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


        public BaseInfoModel GetBaseInfo(string sideMenuId,string userId)
        {
            var authorityCode = _bllEmployee.GetUserAuthorityCode(userId);
            var sideMenuList = _bllSideMenu.GetMenuListFlat();
            var parentId = sideMenuList.Where(s => s.Id == int.Parse(sideMenuId)).Select(s => s.ParentId).FirstOrDefault();
            if (!parentId.HasValue)
            {
                return null;
            }
            var workSiteAuthorityCode =
                sideMenuList.Where(s => s.Id == parentId.Value).Select(s => s.AuthorityCode).FirstOrDefault();

            var dtBaseInfo = _dalBaseInfo.GetMachines();
            if (dtBaseInfo == null)
                return null;

            var baseInfo =
                dtBaseInfo.AsEnumerable()
                    .Where(
                        dr =>
                            dr["AuthorityCode"].ToString() == workSiteAuthorityCode &&
                            CommonUtil.ExistAuthorityCode(authorityCode, dr["AuthorityCode"].ToString()))
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

        public IEnumerable<BaseInfoModel> GetBaseInfo(string sideMenuId, string userId,string machineId)
        {
            var authorityCode = _bllEmployee.GetUserAuthorityCode(userId);
            var sideMenuList = _bllSideMenu.GetMenuListFlat();
            var workSiteAuthorityCode =
                sideMenuList.Where(s => s.Id == int.Parse(sideMenuId)).Select(s => s.AuthorityCode).ToList();

            var dtBaseInfo = _dalBaseInfo.GetMachines();
            if (dtBaseInfo == null)
                return null;

            var baseInfo =
                dtBaseInfo.AsEnumerable()
                    .Where(
                        dr =>
                            workSiteAuthorityCode.Contains(dr["AuthorityCode"].ToString()) &&
                            CommonUtil.ExistAuthorityCode(authorityCode, dr["AuthorityCode"].ToString()))
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
                    }).ToList();




            return string.IsNullOrEmpty(machineId)
                ? baseInfo
                : baseInfo.Where(b => b.Id == machineId).Select(b => b).ToList();

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

        public IEnumerable<BaseInfoModel> GetBaseInfoList(string userId)
        {
            var authorityCode = _bllEmployee.GetUserAuthorityCode(userId);
            var dtBaseInfo = _dalBaseInfo.GetMachines();

            if (dtBaseInfo == null)
                return new List<BaseInfoModel>();

            var baseInfo =
                dtBaseInfo.AsEnumerable()
                    .Where(dr => CommonUtil.ExistAuthorityCode(authorityCode, dr["AuthorityCode"].ToString()))
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
                    }).ToList();

            return baseInfo;

        }
    }
}
