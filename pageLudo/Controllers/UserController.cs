using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users u)
        {
            if (ModelState.IsValid)
            {
                // hozzáadja a felhasználót az adatbázishoz
                using (UserDatabaseEntities ude = new UserDatabaseEntities())
                {
                    ude.Users.Add(u);
                    ude.SaveChanges();
                    ModelState.Clear();
                    u = null;
                    ViewBag.Message = "Registration done";
                }
            }
            return View(u);
        }
    }
}