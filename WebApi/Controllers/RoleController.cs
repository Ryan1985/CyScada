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
    public class RoleController : ApiController
    {
        private BllRole _bllRole = new BllRole();

        public BllRole BLLRole
        {
            get { return _bllRole; }
            set { _bllRole = value; }
        }

        // GET api/role
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/role/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/role
        public string Post([FromBody]RoleModel model)
        {
            var result = _bllRole.SaveRole(model);
            return result;
        }

        // PUT api/role/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/role/5
        public void Delete(int id)
        {
            _bllRole.DeleteRole(id);
        }
    }
}
