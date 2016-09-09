using Entities;
using pageLudo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using SignalRServer.MVCData.MethodClasses;
using Google.DataTable.Net.Wrapper.Extension;
using Google.DataTable.Net.Wrapper;
using SignalRServer.MVCData.DataClasses;
using System.Collections;
using Newtonsoft.Json;

namespace pageLudo.Controllers
{
    public class UserController : Controller
    {
        // itt kell megírnom a profil editet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(ChangeUser u)
        {
            if (ModelState.IsValid)
            {
                UserActions ua = new UserActions();
                if(ua.ProfileSetting(Session["LogedEmailID"].ToString(), u.Username,u.Password,u.EmailID))
                {
                    ViewBag.Message = "User settings saved";
                }
                else
                {
                    ViewBag.Message = "User setting failed";
                }
                ModelState.Clear();
                u = null;
            }

            return View(u);
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            UserStatistics us = new UserStatistics();
            List<GameWinrate> gwrList = us.PlayerWinrate(Session["LogedEmailID"].ToString());

            // fkin serialization
            ArrayList header = new ArrayList { "Game", "Wins", "Losses"};
            ArrayList data1 = new ArrayList {"Ludo",673,6335};

            ArrayList data = new ArrayList { header, data1 };

            string dataStr = JsonConvert.SerializeObject(data, Formatting.None);
            ViewBag.Data = new HtmlString(dataStr);
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
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
                UserActions ua = new UserActions();
                UserData ud = ua.Login(u.EmailID, u.Password);
                if (ud != null)
                {
                    Session["LogedUserID"] = ud.UserID.ToString();
                    Session["LogedUsername"] = ud.Username.ToString();
                    Session["LogedEmailID"] = ud.EmailID.ToString();
                }
                else
                {
                    ModelState.AddModelError("", "Failed!");
                }

                //adminhoz kell, ha a sessionrole admin, akkor mást fog megjeleníthetővé tenni a layout
                //Session["LogedUserRole"] = ud.Role.ToString();
                return RedirectToAction("AfterLogin");
                //}
            }
            else
            {
                ModelState.AddModelError("", "Failed!");
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
                UserActions ua = new UserActions();
                if (ua.Register(u.Username, u.Password, u.EmailID))
                {
                    ViewBag.Message = "Registration successful";
                }
                else
                {
                    ViewBag.Message = "Registration failed: email already taken";
                }
                ModelState.Clear();
                u = null;
            }

            return View(u);
        }
    }
}