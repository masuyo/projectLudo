using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRServer.Interfaces4MVC
{
    interface IUserActions
    {
        //reg
        bool Register(string Username, string Password, string EmailID); //itt kéne akkor kapnia még egy guIDot is gondolom

        //Login - azért kérem vissza a full usert, mert a nevét a későbbiekben felhasználom az oldalon meg az id-jét sessionhöz, de ha van jobb ötleted, szólj
        Something UserCheck(string EmailID, string Password);

        //Friending - ehhez mondjuk kell az oldalra még egy kereső is (userek közt), és nem tudom, hogy nyerem vissza a keresett user azonosítóját még, ill hogy kérdezem le tőled, valszeg email alapján lesz majd az is
        bool Friend(string BeMyFriendEmailID, string IMightBecomeYourFriendEmailID);

        //Friend accept - oda-vissza meglegyen, tessék, mókás változónevek : D
        bool FriendAccept(string IWillBeYourFriendEmailID, string ThanksForAcceptingMeAsYourFriendEmailID);

        //Unfriend - egyik fél dönti el, mint a válásnál, és megtörténik xD
        bool Unfriend(string YouAreNotMyFriendAnymoreEmailID, string IDidntWantYouAnywayEmailID);
    }
}
