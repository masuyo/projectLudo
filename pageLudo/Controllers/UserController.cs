using pageLudo.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using SignalRServer.MVCData.MethodClasses;
using SignalRServer.MVCData.DataClasses;
using System.Collections;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

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
                string hashedPassword = null;

                if (u.Password != null)
                {
                    var sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(u.Password));
                    hashedPassword = new ASCIIEncoding().GetString(sha1data);
                }

                if (ua.ProfileSetting(Session["LogedEmailID"].ToString(), u.Username,u.Password, hashedPassword))
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
            // win-loss játékonként
            UserStatistics us = new UserStatistics();
            List<GameWinrate> gwrList = us.PlayerWinrate(Session["LogedEmailID"].ToString());

            ArrayList header = new ArrayList { "Game", "Wins", "Losses"};
            ArrayList data = new ArrayList {header};
            foreach (var item in gwrList)
            {
                data.Add(new ArrayList { item.GameName, item.NumberOfWins, item.NumberOfLosses });
            }

            string dataStr = JsonConvert.SerializeObject(data, Formatting.None);
            ViewBag.Data = new HtmlString(dataStr);

            // NumberOfPlayedGamesInEachTypeOfGame
            List<GameWinrate> gwrList2 = us.NumberOfPlayedGamesInEachTypeOfGame(Session["LogedEmailID"].ToString());
            ArrayList header2 = new ArrayList { "Game", "Number of games" };
            ArrayList data2 = new ArrayList { header2 };
            foreach (var item in gwrList2)
            {
                data2.Add(new ArrayList { item.GameName, item.NumberOfGames });
            }

            string dataStr2 = JsonConvert.SerializeObject(data2, Formatting.None);
            ViewBag.Data2 = new HtmlString(dataStr2);

            // UserAverageTimeSpent
            List<GameWinrate> gwrList3 = us.UserAverageTimeSpent(Session["LogedEmailID"].ToString());
            ArrayList header3 = new ArrayList { "Game", "Average number of turns" };
            ArrayList data3 = new ArrayList { header3 };
            foreach (var item in gwrList3)
            {
                data3.Add(new ArrayList { item.GameName, item.AverageNumberOfTurns });
            }

            string dataStr3 = JsonConvert.SerializeObject(data3, Formatting.None);
            ViewBag.Data3 = new HtmlString(dataStr3);


            // UserLongestGame
            List<GameWinrate> gwrList4 = us.UserLongestGame(Session["LogedEmailID"].ToString());
            ArrayList header4 = new ArrayList { "Game", "Number of turns of the longest game" };
            ArrayList data4 = new ArrayList { header4 };
            foreach (var item in gwrList4)
            {
                data4.Add(new ArrayList { item.GameName, item.NumberOfTurnsOfTheLongestGame });
            }

            string dataStr4 = JsonConvert.SerializeObject(data4, Formatting.None);
            ViewBag.Data4 = new HtmlString(dataStr4);

            // UserShortestGame
            List<GameWinrate> gwrList5 = us.UserShortestGame(Session["LogedEmailID"].ToString());
            ArrayList header5 = new ArrayList { "Game", "Number of turns of the longest game" };
            ArrayList data5 = new ArrayList { header5 };
            foreach (var item in gwrList5)
            {
                data5.Add(new ArrayList { item.GameName, item.NumberOfTurnsOfTheShortestGame });
            }

            string dataStr5 = JsonConvert.SerializeObject(data5, Formatting.None);
            ViewBag.Data5 = new HtmlString(dataStr5);

            // PlayerColorWinrate
            List<GameWinrate> gwrList6 = us.PlayerColorWinrate(Session["LogedEmailID"].ToString(),"Ludo");
            ArrayList header6 = new ArrayList { "Color", "Number of wins", "Number of losses"};
            ArrayList data6 = new ArrayList { header6 };
            foreach (var item in gwrList6)
            {
                data6.Add(new ArrayList { item.ColorName, item.NumberOfWins, item.NumberOfLosses });
            }

            string dataStr6 = JsonConvert.SerializeObject(data6, Formatting.None);
            ViewBag.Data6 = new HtmlString(dataStr6);

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
        public ActionResult Login(LoginUser u) // emaillel lép be
        {
            if (ModelState.IsValid)
            {
                var sha1 = new SHA1CryptoServiceProvider();
                byte[] sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(u.Password));
                string hashedPassword = new ASCIIEncoding().GetString(sha1data);

                UserActions ua = new UserActions();
                UserData ud = ua.Login(u.EmailID, hashedPassword);
                if (ud != null)
                {
                    Session["LogedUserID"] = ud.UserID.ToString();
                    Session["LogedUsername"] = ud.Username.ToString();
                    Session["LogedEmailID"] = ud.EmailID.ToString();
                    Session["Role"] = ud.Role.ToString();
                }
                else
                {
                    ModelState.AddModelError("WrongUNorPW", "Incorrect e-mail address or password");
                    return View();
                }

                //adminhoz kell, ha a sessionrole admin, akkor mást fog megjeleníthetővé tenni a layout
                //Session["LogedUserRole"] = ud.Role.ToString();
                return RedirectToAction("AfterLogin");
                //}
            }
            return View();
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
                // pw hash
                var sha1 = new SHA1CryptoServiceProvider();
                byte[] sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(u.Password));
                string hashedPassword = new ASCIIEncoding().GetString(sha1data);

                if (ua.Register(u.Username, hashedPassword, u.EmailID))
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