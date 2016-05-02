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
    public class SideMenuListController : ApiController
    {
        private BllSideMenu _bllSideMenu = new BllSideMenu();

        public BllSideMenu BllSideMenu
        {
            get { return _bllSideMenu; }
            set { _bllSideMenu = value; }
        }

        // GET api/sidemenulist
        public IEnumerable<SideMenuModel> Get()
        {
            var sideMenuList = _bllSideMenu.GetMenuListFlat();
            return sideMenuList;
        }

        // GET api/sidemenulist/5
        public IEnumerable<SideMenuModel> Get(string paramstring)
        {
            var model = JsonConvert.DeserializeObject<SideMenuModel>(paramstring);
            return model == null ? _bllSideMenu.GetMenuListFlat() : _bllSideMenu.GetMenuListFlat(model);
        }

        // POST api/sidemenulist
        public void Post([FromBody]string value)
        {

        }

        // PUT api/sidemenulist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sidemenulist/5
        public void Delete(int id)
        {
        }
    }
}
