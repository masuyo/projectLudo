using Entities;
using pageLudo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using pageLudo.FakeData.MethodClasses;
using pageLudo.FakeData.DataClasses;

namespace pageLudo.Controllers
{
    public class UserController : Controller
    {
        //public ActionResult Friending()
        //{
        //  
        //}
        public ActionResult MyProfile()
        {
            //Statistics stc = new Statistics();
            pageLudo.User.Statistics.GameWinRateGetChartData(Session["LogedEmailID"].ToString());

            return View();
        }

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
                    // TODO: repositoryba kell egy lekérdezés Email és Password alapján

                    var obj = new LoginUser(){ UserID = 1, Username = "Cressida", Password = "123456", EmailID = "cressida@citromail.hu", Role = "admin"};
                    //var obj = ude.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    //if (obj != null)
                    //{
                    Session["LogedUserID"] = obj.UserID.ToString();
                    Session["LogedUsername"] = obj.Username.ToString();
                    Session["LogedEmailID"] = obj.EmailID.ToString();

                    //adminhoz kell, ha a sessionrole admin, akkor mást fog megjeleníthetővé tenni a layout
                    Session["LogedUserRole"] = obj.Role.ToString();
                    return RedirectToAction("AfterLogin");
                    //}
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            GameStatistics gs = new GameStatistics();
            int numberOfOnlineWpfUsers = gs.NumberOfOnlineWPFUsers();
            TempData["onlineUsers"] = numberOfOnlineWpfUsers;
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
                    var useractioner = new UserActions();
                    if (useractioner.Register(u.Username, u.Password, u.EmailID))
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