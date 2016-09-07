using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pageLudo.FakeData.MethodClasses;

namespace pageLudo.Controllers
{
    public class FriendController : Controller
    {
        // initiate friend request
        [HttpPost]
        public ActionResult Friending()
        {
            UserActions ua = new UserActions();
            ua.Friend(Session["LogedEmailID"].ToString(), TempData["AccessedEmailID"].ToString());
            return new EmptyResult();
        }
    }
}