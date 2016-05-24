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
    public class ControlDeskController : ApiController
    {
        private BllControlDesk _bllControlDesk = new BllControlDesk();

        public BllControlDesk BLLControlDesk
        {
            get { return _bllControlDesk; }
            set { _bllControlDesk = value; }
        }


        // GET api/controldesk
        public IEnumerable<ControlDeskItemModel> Get(string sideMenuId, string userId)
        {
            return _bllControlDesk.GetItemList(sideMenuId, userId);
        }

        // GET api/controldesk/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/controldesk
        public void Post([FromBody]string value)
        {
        }

        // PUT api/controldesk/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/controldesk/5
        public void Delete(int id)
        {
        }
    }
}
