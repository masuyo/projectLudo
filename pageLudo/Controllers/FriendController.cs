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
        public ActionResult FriendAccept()
        {
            // metódus mindent true-ra állít
            return new ContentResult() { Content = "Visszajelölted ezt a felhasználót, most már barátok vagytok." };
        }


        // initiate friend request
        [HttpGet]
        public ActionResult Friending()
        {
            // a meghívott metódus true-ra állítja a FriendedYou propertyt
            UserActions ua = new UserActions();
            ua.Friend(Session["LogedEmailID"].ToString(), Session["AccessedEmailID"].ToString());
            return new ContentResult() { Content = "Barátnak jelölted ezt a felhasználót." };
        }

        // elég az egyik oldalnak kezdeményeznie
        [HttpGet]
        public ActionResult Unfriending()
        {
            // a meghívott metódus false-ra állítja mindhárom változót
            return new ContentResult() { Content = "Megszakítottad a barátságot ezzel a felhasználóval." };
        }
    }
}