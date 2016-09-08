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
            //UserStatistics us = new UserStatistics();
            //var list = us.PlayerWinrate(Session["LogedEmailID"].ToString());

            //var json = list.ToGoogleDataTable()
            //               .NewColumn(new Column(ColumnType.String, "Name"), x => x.Name)
            //               .NewColumn(new Column(ColumnType.Number, "Count"), x => x.Count)
            //               .Build()
            //               .GetJson();
            //Console.WriteLine(json);
            //Statistics stc = new Statistics();
            //pageLudo.User.Statistics.GameWinRateGetChartData(Session["LogedEmailID"].ToString());

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