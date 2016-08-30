using Entities;
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
        public ActionResult Login(LoginUser u) // Username helyett emaillel lép be
        {
            if (ModelState.IsValid)
            {
                using (DatabaseEntities DE = new DatabaseEntities())
                {

                    var repo = new Repository.TableRepositories.UsersRepository(DE);
                    // kikeresi az adatbázisból a beadott adatok alapján a usert, ha megtalálja, Sessiont kap
                    // TODO: repositoryba kell egy lekérdezés Email és Password alapján, ami visszaadja az objectet,
                    // illetve megoldandó, h tudja, h a pw vagy az email volt-e rossz

                    //var obj = ude.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    //if (obj != null)
                    //{
                    //    Session["LogedUserID"] = obj.UserID.ToString();
                    //    Session["LogedUsername"] = obj.Username.ToString();
                    //    return RedirectToAction("AfterLogin");
                    //}
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
        public ActionResult Register(RegisterUser u)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseEntities DE = new DatabaseEntities())
                {
                    var repo = new Repository.TableRepositories.UsersRepository(DE);
                    if (repo.Register(u.Username, u.Password, u.EmailID))
                    {
                        ViewBag.Message = "Registration successful";
                    }
                    ModelState.Clear();
                    u = null;
                }
            }
            return View(u);
        }
    }
}