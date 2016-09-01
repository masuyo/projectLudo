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
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index","Home");
        }

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

                    //var repo = new Repository.TableRepositories.UsersRepository(DE);
                    // kikeresi az adatbázisból a beadott adatok alapján a usert, ha megtalálja, Sessiont kap
                    // TODO: repositoryba kell egy lekérdezés Email és Password alapján, true, ha legalább az email megvan,
                    // én csekkolom, h a jelszó egyezik-e

                    var obj = new LoginUser(){ UserID = 1, Username = "Cressida", Password = "123456", EmailID = "cressida@citromail.hu" };
                    //var obj = ude.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    //if (obj != null)
                    //{
                    Session["LogedUserID"] = obj.UserID.ToString();
                    Session["LogedUsername"] = obj.Username.ToString();
                    return RedirectToAction("AfterLogin");
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