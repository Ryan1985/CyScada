using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using CyScada.BLL;
using CyScada.Model;
using CyScada.Web.Common;
using CyScada.Web.Models;
using Newtonsoft.Json;

namespace CyScada.Web.Controllers
{
    public class LoginController : Controller
    {
        protected BllLogin BllLogin = new BllLogin();
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        ////
        //// GET: /Login/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Login/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Login/Create

        [System.Web.Mvc.HttpPost]
        public ActionResult Index([FromBody] AccountViewModel account)
        {
            try
            {
                var user = new UserModel
                {
                    LoginName = account.UserName,
                    Password = DESEncrypt.Encrypt(account.Password)
                };
                var result = BllLogin.Login(user);
                if (!string.IsNullOrEmpty(result))
                {
                    ViewBag.ErrorInfo = result;
                    Session["User"] = null;
                    return View();
                }

                user.TokenKey = Guid.NewGuid().ToString();

                var cookie = FormsAuthentication.GetAuthCookie("Username", false);
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                    ticket.Expiration, ticket.IsPersistent, JsonConvert.SerializeObject(user));
                cookie.Value = FormsAuthentication.Encrypt(newTicket);
                Response.Cookies.Set(cookie);

                Session["User"] = user;
                return RedirectToAction("Index","EquipmentList");
            }
            catch
            {
                return View();
            }
        }

        ////
        //// GET: /Login/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Login/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Login/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Login/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
