using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyScada.Model;

namespace CyScada.Web.Controllers
{
    public class RealTimeDisplay2Controller : Controller
    {
        //
        // GET: /RealTimeDisplay/

        public ActionResult Index(string MenuId)
        {
            var user = Session["User"] as UserModel;
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Id = user.Id;
            ViewBag.Name = user.Name;
            ViewBag.SideMenuId = MenuId;
            return View();
        }

    }
}
