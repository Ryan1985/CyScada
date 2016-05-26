using System.Collections.Generic;
using System.Web.Http;
using CyScada.Model;
using CyScada.BLL;

namespace CyScada.Web.WebApi.Controllers
{
    [ApiAuth]
    public class SideMenuController : ApiController
    {
        private BllLogin _bllLogin = new BllLogin();
        private BllSideMenu _bllSideMenu = new BllSideMenu();

        public BllSideMenu BllSideMenu
        {
            get { return _bllSideMenu; }
            set { _bllSideMenu = value; }
        }

        public BllLogin BllLogin
        {
            get { return _bllLogin; }
            set { _bllLogin = value; }
        }


        // GET api/sidemenu
        public IEnumerable<SideMenuModel> Get()
        {
            return new List<SideMenuModel>();
        }

        // GET api/sidemenu/5
        public IList<SideMenuModel> Get(int userId)
        {
            var user = _bllLogin.GetUserInfo(new UserModel {Id = userId});
            var sideMenuList = _bllSideMenu.GetMenuList(user);
            return sideMenuList;
        }

        public IList<SideMenuModel> Get(int userId,string themeType)
        {
            var user = _bllLogin.GetUserInfo(new UserModel { Id = userId });
            var sideMenuList = _bllSideMenu.GetMenuList(user);
            return sideMenuList;
        }

        // POST api/sidemenu
        public string Post([FromBody]SideMenuModel model)
        {
            var result = _bllSideMenu.SaveSideMenu(model);
            return result;
        }

        // PUT api/sidemenu/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sidemenu/5
        public void Delete(int id)
        {
            _bllSideMenu.DeleteSideMenu(id);
        }
    }
}
