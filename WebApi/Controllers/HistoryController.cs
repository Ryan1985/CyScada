using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CyScada.BLL;
using CyScada.Model;

namespace CyScada.Web.WebApi.Controllers
{
    public class HistoryController : ApiController
    {
        private BllHistory _bllHistory = new BllHistory();

        public BllHistory BLLHistory
        {
            get { return _bllHistory; }
            set { _bllHistory = value; }
        }


        public DataTable Get(string sideMenuId, string userId)
        {
            return _bllHistory.GetMachineTagList(sideMenuId, userId);
        }



        public IEnumerable<HistoryModel> Get(string paramstring)
        {
            throw new NotImplementedException();
        }



    }
}
