using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyScada.Web.Controllers
{
    public class testController : Controller
    {
        //
        // GET: /test/

        public ActionResult Index()
        {
            return PartialView();
        }


        //
        // GET: /test/

        public ActionResult Detail()
        {
            return PartialView();
        }

    }
}
