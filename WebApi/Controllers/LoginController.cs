using System.Web.Http;
using CyScada.Model;
using CyScada.BLL;

namespace CyScada.Web.WebApi.Controllers
{

    public class LoginController : ApiController
    {
        protected BllLogin BllLogin = new BllLogin();

        // GET api/login
        public string Get()
        {
            return string.Empty;
        }

        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public void Post([FromBody]UserModel user)
        {
            //try
            //{
            //    var cookie = FormsAuthentication.GetAuthCookie("Userinfo", false);
            //    var ticket = FormsAuthentication.Decrypt(cookie.Value);
            //    var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, "");
            //    cookie.Value = FormsAuthentication.Encrypt(newTicket);
            //    FormsAuthentication.SetAuthCookie(cookie.Value, true);
            //    var response = Request.CreateResponse();
            //    Response.Cookies.Set(cookie);
            //    return string.Empty;
            //}
            //catch(Exception ex)
            //{
            //    return ex.Message;
            //}
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
