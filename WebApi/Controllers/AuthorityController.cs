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
    public class AuthorityController : ApiController
    {
        private BllAuthority _bllAuthority = new BllAuthority();

        public BllAuthority BLLAuthority
        {
            get { return _bllAuthority; }
            set { _bllAuthority = value; }
        }


        // GET api/authority
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/authority/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/authority
        public string Post([FromBody]AuthorityModel model)
        {
            var result = _bllAuthority.SaveAuthority(model);
            return result;
        }

        // PUT api/authority/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/authority/5
        public void Delete(int id)
        {
            _bllAuthority.DeleteAuthority(id);
        }
    }
}
