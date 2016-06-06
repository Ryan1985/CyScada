using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CyScada.BLL;
using CyScada.Model;

namespace CyScada.Web.WebApi.Controllers
{
    public class RealTimeDisplayController : ApiController
    {
        private BllRealTimeDisplay _bllRealTimeDisplay = new BllRealTimeDisplay();
        
        public BllRealTimeDisplay BLLRealTimeDisplay
        {
            get { return _bllRealTimeDisplay; }
            set { _bllRealTimeDisplay = value; }
        }




        public BaseInfoModel Get(string sideMenuId, string userId)
        {
            return _bllRealTimeDisplay.GetMachineTags(sideMenuId, userId);
        }


        public Hashtable Get()
        {
            return OpcClient.OpcClient.ItemTable;
        }


        public void Post([FromBody] string[] values)
        {
            OpcClient.OpcClient.SetValue(values[0], values[1]);
        }

    }
}
