using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CyScada.BLL;
using CyScada.Model;
using Newtonsoft.Json;

namespace CyScada.Web.WebApi.Controllers
{
    public class AuthorityListController : ApiController
    {
        private BllAuthority _bllAuthority = new BllAuthority();

        public BllAuthority BLLAuthority
        {
            get { return _bllAuthority; }
            set { _bllAuthority = value; }
        }


        // GET api/authoritylist
        public IEnumerable<AuthorityModel> Get()
        {
            return _bllAuthority.GetAuthorityList();
        }

        // GET api/authoritylist/5
        public IEnumerable<AuthorityModel> Get(string paramstring)
        {
            var model = JsonConvert.DeserializeObject<AuthorityModel>(paramstring);
            return model == null ? _bllAuthority.GetAuthorityList() : _bllAuthority.GetAuthorityList(model);
        }

        // POST api/authoritylist
        public void Post([FromBody]string value)
        {
        }

        // PUT api/authoritylist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/authoritylist/5
        public void Delete(int id)
        {
        }
    }
}
