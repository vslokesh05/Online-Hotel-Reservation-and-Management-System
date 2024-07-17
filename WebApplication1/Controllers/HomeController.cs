using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Online Hotel Reservation System.";



            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Contact : 9897969594";



            return View();
        }
    }
}
