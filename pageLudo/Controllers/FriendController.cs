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
        [ActionName("Friending")]
        public void Friending()
        {
            UserActions ua = new UserActions();
            ua.Friend(Session["LogedEmailID"].ToString(), Session["AccessedEmailID"].ToString());
        }
    }
}