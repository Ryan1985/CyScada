using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CyScada.BLL;
using CyScada.Model;

namespace CyScada.Web.WebApi.Controllers
{
    public class MapBoardController : ApiController
    {
        private BllBaseInfo _bllBaseInfo = new BllBaseInfo();
        private BllSideMenu _bllSideMenu = new BllSideMenu();


        public BllBaseInfo BLLBaseInfo
        {
            get { return _bllBaseInfo; }
            set { _bllBaseInfo = value; }
        }

        public BllSideMenu BLLSideMenu
        {
            get { return _bllSideMenu; }
            set { _bllSideMenu = value; }
        }

        public IEnumerable<BaseInfoModel> Get(string userId)
        {
            return _bllBaseInfo.GetBaseInfoList(userId);
        }


        public string Get(string userId,string authorityCode)
        {
            return _bllSideMenu.GetSideMenuIdByAuthorityCode(userId, authorityCode);
        }
    }
}
