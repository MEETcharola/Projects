using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTermWorkSubmission.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult About()
        {
            ViewBag.Message = "Our application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact page.";

            return View();
        }
    }
}