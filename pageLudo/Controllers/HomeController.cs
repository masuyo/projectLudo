using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //mindig false-ra ugrik
        public ActionResult Search(string searchString)
        {
            if (searchString == "keresek")
            {
                return RedirectToAction("MyProfile","User");
            }
            else
            {
                return RedirectToAction("Register", "User");
            }
        }

    }
}