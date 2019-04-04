using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiProject.Controllers
{
    public class HomeController : Controller
    { 
        // FRONT END 

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Teacher()
        {
            return View();
        }

        public ActionResult Student()
        {
            return View();
        }

        public ActionResult AboutMe()
        {
            return View();
        }
    }
}
