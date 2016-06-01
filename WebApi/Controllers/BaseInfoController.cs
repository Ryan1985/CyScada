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
    public class BaseInfoController : ApiController
    {
        private BllBaseInfo _bllBaseInfo = new BllBaseInfo();


        public BllBaseInfo BLLBaseInfo
        {
            get { return _bllBaseInfo; }
            set { _bllBaseInfo = value; }
        }



        public BaseInfoModel Get(string sideMenuId,string userId)
        {
            return _bllBaseInfo.GetBaseInfo(sideMenuId,userId);
        }



    }
}
