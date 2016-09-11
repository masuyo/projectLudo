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
        public ActionResult FriendAccepting()
        {
            UserActions ua = new UserActions();
            ua.FriendAccept(Session["LogedEmailID"].ToString(), Session["AccessedEmailID"].ToString());
            return View("~/Views/Search/ProfileSearchResult.cshtml");
        }


        // initiate friend request
        [HttpPost]
        public ActionResult Friending()
        {
            UserActions ua = new UserActions();
            ua.Friend(Session["LogedEmailID"].ToString(), Session["AccessedEmailID"].ToString());
            return View("~/Views/Search/ProfileSearchResult.cshtml");
        }

        // elég az egyik oldalnak kezdeményeznie
        [HttpPost]
        public ActionResult Unfriending()
        {
            UserActions ua = new UserActions();
            ua.Unfriend(Session["LogedEmailID"].ToString(), Session["AccessedEmailID"].ToString());
            return View("~/Views/Search/ProfileSearchResult.cshtml");
        }
    }
}