using pageLudo.Models;
using SignalRServer.MVCData.DataClasses;
using SignalRServer.MVCData.MethodClasses;
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
        // többtalálatos keresési eredmény kilistázásához
        UserListingModel ulm;

        public ActionResult Details(string emailID)
        {
            //List<UserListingData> userList = new List<UserListingData>();
            var userList = Session["ResultList"] as List<UserListingData>;
            UserListingData user = userList.SingleOrDefault(x => x.EmailID == emailID);
            Session["AccessedUsername"] = user.Username;
            Session["AccessedEmailID"] = user.EmailID.ToString();
            Session["AccessedFriendState"] = "false";
            Session["AccessedFriendedYou"] = "false";
            Session["AccessedFriendedMe"] = "false";
            return View("ProfileSearchResult", user);
        }

        // a kilistázott userekből ezzel lehet egy adott user profiljára kattintani

        public ActionResult ProfileSearchResult(string emailID)
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

            // adatbázishoz kinyeréshez
            UserActions ua = new UserActions();
            List<UserData> getUsers = new List<UserData>();
            UserData resultUser = new UserData();

            //igaz esetén Username alapján keres
            if (!Regex.IsMatch(searchString, searchEmailRegEx))
            {
                getUsers = ua.UsernameSearch(searchString, loginEmailID);
                if (getUsers.Count() != 0)
                {
                    if (getUsers.Count() == 1)
                    {
                        // visszakapott adatok
                        UserListingData convResultUser = new UserListingData();
                        convResultUser.Username = getUsers[0].Username;
                        convResultUser.EmailID = getUsers[0].EmailID;
                        convResultUser.AreWeFriends = getUsers[0].AreWeFriends;
                        convResultUser.FriendedYou = getUsers[0].FriendedYou;
                        convResultUser.FriendedMe = getUsers[0].FriendedMe;

                        Session["AccessedUsername"] = convResultUser.Username.ToString();
                        Session["AccessedEmailID"] = convResultUser.EmailID.ToString();
                        Session["AccessedFriendState"] = convResultUser.AreWeFriends.ToString();
                        Session["AccessedFriendedYou"] = convResultUser.FriendedYou.ToString();
                        Session["AccessedFriendedMe"] = convResultUser.FriendedMe.ToString();

                        return View("ProfileSearchResult", convResultUser);
                    }
                    else
                    {
                        List<UserListingData> luld = new List<UserListingData>();
                        foreach (var user in getUsers)
                        {
                            luld.Add(new UserListingData { Username = user.Username, EmailID = user.EmailID });
                        }
                        //MultipleProfileSearchResult(luld);
                        ulm = new UserListingModel();
                        ulm.List = luld;
                        Session["ResultList"] = ulm.List;
                        //ulm.EditObject = null;
                        return View("MSRView", ulm);
                        //return View("MultipleProfileSearchResult", "Search");
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
                    UserListingData convResultUser = new UserListingData();
                    resultUser = ua.EmaildIDSearch(searchString, loginEmailID);
                    convResultUser.Username = resultUser.Username;
                    convResultUser.EmailID = resultUser.EmailID;
                    convResultUser.AreWeFriends = resultUser.AreWeFriends;
                    convResultUser.FriendedYou = resultUser.FriendedYou;
                    convResultUser.FriendedMe = resultUser.FriendedMe;
                    // visszakapott adatok
                    Session["AccessedUsername"] = convResultUser.Username.ToString();
                    Session["AccessedEmailID"] = convResultUser.EmailID.ToString();
                    Session["AccessedFriendState"] = convResultUser.AreWeFriends.ToString();
                    Session["AccessedFriendedYou"] = convResultUser.FriendedYou.ToString();
                    Session["AccessedFriendedMe"] = convResultUser.FriendedMe.ToString();

                    // keresett user profilja
                    return View("ProfileSearchResult", resultUser);
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