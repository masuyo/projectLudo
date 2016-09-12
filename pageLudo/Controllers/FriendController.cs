using pageLudo.Models;
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
        [HttpPost]
        public ActionResult FriendAccepting(UserListingData u)
        {
            UserActions ua = new UserActions();
            ua.FriendAccept(Session["LogedEmailID"].ToString(), u.EmailID);
            return View("~/Views/Search/ProfileSearchResult.cshtml",u);
        }


        // initiate friend request
        [HttpPost]
        public ActionResult Friending(UserListingData u)
        {
            UserActions ua = new UserActions();
            ua.Friend(u.EmailID, Session["LogedEmailID"].ToString());
            return View("~/Views/Search/ProfileSearchResult.cshtml",u);
        }

        // elég az egyik oldalnak kezdeményeznie
        [HttpPost]
        public ActionResult Unfriending(UserListingData u)
        {
            UserActions ua = new UserActions();
            ua.Unfriend(Session["LogedEmailID"].ToString(), u.EmailID);
            return View("~/Views/Search/ProfileSearchResult.cshtml",u);
        }
    }
}