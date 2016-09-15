using pageLudo.Models;
using SignalRServer.MVCData.DataClasses;
using SignalRServer.MVCData.MethodClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class FriendController : Controller
    {
        UserData resultUser;
        UserListingData convResultUser;
        UserListingData loadUser;

        [HttpPost]
        public ActionResult FriendAccepting(string clickedEmailID)
        {
            UserActions ua = new UserActions();
            ua.FriendAccept(Session["LogedEmailID"].ToString(), clickedEmailID);
            loadUser = new UserListingData();
            loadUser = UserConvert(clickedEmailID, Session["LogedEmailID"].ToString());
            return View("~/Views/Search/ProfileSearchResult.cshtml",loadUser);
        }


        // initiate friend request
        [HttpPost]
        public ActionResult Friending(string clickedEmailID)
        {
            UserActions ua = new UserActions();
            ua.Friend(clickedEmailID,Session["LogedEmailID"].ToString());
            loadUser = new UserListingData();
            loadUser = UserConvert(clickedEmailID, Session["LogedEmailID"].ToString());
            return View("~/Views/Search/ProfileSearchResult.cshtml", loadUser);
        }

        // elég az egyik oldalnak kezdeményeznie
        [HttpPost]
        public ActionResult Unfriending(string clickedEmailID)
        {
            UserActions ua = new UserActions();
            ua.Unfriend(Session["LogedEmailID"].ToString(), clickedEmailID);
            loadUser = new UserListingData();
            loadUser = UserConvert(clickedEmailID, Session["LogedEmailID"].ToString());
            return View("~/Views/Search/ProfileSearchResult.cshtml", loadUser);
        }

        public UserListingData UserConvert(string clickedEmailAD, string logedInEmailAD)
        {
            UserActions ua = new UserActions();
            resultUser = new UserData();
            resultUser = ua.EmaildIDSearch(clickedEmailAD, logedInEmailAD);
            convResultUser = new UserListingData();
            convResultUser.Username = resultUser.Username;
            convResultUser.EmailID = resultUser.EmailID;
            convResultUser.AreWeFriends = resultUser.AreWeFriends;
            convResultUser.FriendedYou = resultUser.FriendedYou;
            convResultUser.FriendedMe = resultUser.FriendedMe;
            return convResultUser;
        }
    }
}