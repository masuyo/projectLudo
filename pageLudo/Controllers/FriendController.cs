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
        [HttpPost]
        public ActionResult FriendAccepting()
        {
            UserActions ua = new UserActions();
            // metódus mindent true-ra állít
            ua.FriendAccept(Session["LogedEmailID"].ToString(), Session["AccessedEmailID"].ToString());
            return View();
        }


        // initiate friend request
        [HttpPost]
        public ActionResult Friending()
        {
            // a meghívott metódus true-ra állítja a FriendedYou propertyt
            UserActions ua = new UserActions();
            ua.Friend(Session["LogedEmailID"].ToString(), Session["AccessedEmailID"].ToString());
            return View();
        }

        // elég az egyik oldalnak kezdeményeznie
        [HttpPost]
        public ActionResult Unfriending()
        {
            UserActions ua = new UserActions();
            // a meghívott metódus false-ra állítja mindhárom változót
            ua.Unfriend(Session["LogedEmailID"].ToString(), Session["AccessedEmailID"].ToString());
            return View();
        }
    }
}