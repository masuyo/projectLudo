using pageLudo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Chat()
        {
            LoginUser lu = new LoginUser();
            lu.Username = Session["LogedUsername"].ToString();
            return View("Chat",lu);
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}