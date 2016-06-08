using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyScada.Model;

namespace CyScada.Web.Controllers
{
    public class HistoryController : Controller
    {
        //
        // GET: /History/

        public ActionResult Index(string sideMenuId)
        {
            var user = Session["User"] as UserModel;
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Id = user.Id;
            ViewBag.Name = user.Name;
            ViewBag.SideMenuId = sideMenuId;
            return PartialView();
        }

    }
}
