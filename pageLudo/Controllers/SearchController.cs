using pageLudo.FakeData.MethodClasses;
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
            List<FakeData.DataClasses.UserData> getUsers = new List<FakeData.DataClasses.UserData>();
            getUsers = ua.UsernameSearch(searchString, loginEmailID);

            //igaz esetén Username alapján keres
            if (!Regex.IsMatch(searchString, searchEmailRegEx))
            {
                if (getUsers.Count() != 0)
                {
                    // visszakapott adatok
                    if (getUsers.Count() >= 1)
                    {
                        return RedirectToAction("ProfileSearchResult", "Search");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // nem található ilyen felhasználó a rendszerben
                    return RedirectToAction("Register","User");
                }
            }
            else
            {
                if (true)
                {
                    // visszakapott adatok

                    // keresett user profilja
                    return RedirectToAction("NoSearchResult", "Search");
                }
                //else
                //{
                //    // nem található ilyen felhasználó a rendszerben
                //    return RedirectToAction("NoSearchResult", "Search");
                //}
            }
        }
    }
}