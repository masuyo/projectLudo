using pageLudo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser u)
        {
            if (ModelState.IsValid)
            {
                using (UserDatabaseEntities ude = new UserDatabaseEntities())
                {
                    // kikeresi az adatbázisból a beadott adatok alapján a usert, ha megtalálja, Sessiont kap
                    var obj = ude.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["LogedUserID"] = obj.UserID.ToString();
                        Session["LogedUsername"] = obj.Username.ToString();
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

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
                    ViewBag.Message = "Registration successful";
                }
            }
            return View(u);
        }
    }
}