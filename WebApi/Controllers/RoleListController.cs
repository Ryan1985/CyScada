using System.Collections.Generic;
using System.Web.Http;
using CyScada.BLL;
using CyScada.Model;
using Newtonsoft.Json;

namespace CyScada.Web.WebApi.Controllers
{
    public class RoleListController : ApiController
    {
        private BllRole _bllRole = new BllRole();

        public BllRole BLLRole
        {
            get { return _bllRole; }
            set { _bllRole = value; }
        }


        // GET api/rolelist
        public IEnumerable<RoleModel> Get()
        {
            return _bllRole.GetRoleList();
        }

        // GET api/rolelist/5
        public IEnumerable<RoleModel> Get(string paramstring)
        {
            var model = JsonConvert.DeserializeObject<RoleModel>(paramstring);
            return model == null ? _bllRole.GetRoleList() : _bllRole.GetRoleList(model);
        }

        // POST api/rolelist
        public void Post([FromBody]string value)
        {

        }

        // PUT api/rolelist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/rolelist/5
        public void Delete(int id)
        {
        }
    }
}
