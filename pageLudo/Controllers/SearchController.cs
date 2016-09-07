using pageLudo.FakeData.MethodClasses;
using pageLudo.FakeData.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult MultipleProfileSearchResult()
        {
            return View();
        }

        public ActionResult ProfileSearchResult()
        {
            return View();
        }

        public ActionResult NoSearchResult()
        {
            return View();
        }

        [HttpGet]
        // db-ben megnézi, h adott searchString username vagy email, de előtte csekkolja, hogy melyik van beadva
        public ActionResult Search(string searchString)
        {
            string searchEmailRegEx = @"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$";
            string loginEmailID = (string)Session["LogedEmailID"];

            UserActions ua = new UserActions();
            List<UserData> getUsers = new List<UserData>();
            getUsers = ua.UsernameSearch(searchString, loginEmailID);
            UserData resultUser = new UserData();
            resultUser = ua.EmaildIDSearch(searchString, loginEmailID);

            //igaz esetén Username alapján keres
            if (!Regex.IsMatch(searchString, searchEmailRegEx))
            {
                if (getUsers.Count() != 0)
                {
                    if (getUsers.Count() == 1)
                    {
                        // visszakapott adatok
                        Session["AccessedUsername"] = getUsers[0].Username.ToString();
                        Session["AccessedEmailID"] = getUsers[0].EmailID.ToString();
                        Session["AccessedFriendState"] = getUsers[0].AreWeFriends;
                        return RedirectToAction("ProfileSearchResult", "Search");
                    }
                    else
                    {
                        return RedirectToAction("MultipleProfileSearchResult", "Search");
                    }
                }
                else
                {
                    // nem található ilyen felhasználó a rendszerben
                    return RedirectToAction("NoSearchResult","Search");
                }
            }
            else
            {
                if (resultUser != null)
                {
                    // visszakapott adatok
                    Session["AccessedUsername"] = resultUser.Username.ToString();
                    Session["AccessedEmailID"] = resultUser.EmailID.ToString();
                    Session["AccessedFriendState"] = resultUser.AreWeFriends;

                    // keresett user profilja
                    return RedirectToAction("ProfileSearchResult", "Search");
                }
                else
                {
                    // nem található ilyen felhasználó a rendszerben
                    return RedirectToAction("NoSearchResult", "Search");
                }
            }
        }
    }
}