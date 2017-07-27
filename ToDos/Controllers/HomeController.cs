using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Why create To Dos?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Getting in touch with feedback on the website.";

            return View();
        }
    }
}