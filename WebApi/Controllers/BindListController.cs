using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CyScada.BLL;

namespace CyScada.Web.WebApi.Controllers
{
    public class BindListController : ApiController
    {
        private BllBindList _bllBindList = new BllBindList();

        public BllBindList BLLBindList
        {
            get { return _bllBindList; }
            set { _bllBindList = value; }
        }


        // GET api/bindlist
        public int Get()
        {
            return new Random().Next();
        }

        // GET api/bindlist/5
        public DataTable Get(string bindType,string param)
        {
            return _bllBindList.GetBindList(bindType, param);
        }

        // POST api/bindlist
        public void Post([FromBody]string value)
        {
        }

        // PUT api/bindlist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bindlist/5
        public void Delete(int id)
        {
        }
    }
}
