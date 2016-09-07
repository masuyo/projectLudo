using SignalRServer.MVCData.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRServer.MVCData.Interfaces
{
    public interface IUserActions
    {
        /* search - user kikeresése email vagy név alapján; vissza kéne kapnom mindenképp usert: név, email, 
         * friendek vagyunk-e (velem, aki kerestem, ehhez gondolom, kell majd még egy lekérdezés?), 
         * utóbbinak az emailjét fogom beadni; gondolom, ha nem talál semmit, akkor dobjon nekem egy nullt?, 
         * nem tudom, egyelőre eszerint írtam meg nálam
         */

        List<UserData> UsernameSearch(string username, string searcherEmailID);

        //ugyanazt csinálja, mint az előző, csak emailben keres
        UserData EmaildIDSearch(string emailID, string searcherEmailID);

        //reg
        //itt kéne akkor kapnia még egy guIDot is gondolom
        bool Register(string Username, string Password, string EmailID);

        //Login - azért kérem vissza a full usert, mert a nevét a későbbiekben felhasználom az oldalon 
        //meg az id-jét sessionhöz, de ha van jobb ötleted, szólj
        UserData UserCheck(string EmailID, string Password);

        //Friending 
        void Friend(string BeMyFriendEmailID, string IMightBecomeYourFriendEmailID);

        //Friend accept - oda-vissza meglegyen, tessék, mókás változónevek : D
        void FriendAccept(string IWillBeYourFriendEmailID, string ThanksForAcceptingMeAsYourFriendEmailID);

        //Unfriend - egyik fél dönti el, mint a válásnál, és megtörténik xD
        bool Unfriend(string YouAreNotMyFriendAnymoreEmailID, string IDidntWantYouAnywayEmailID);

    }
}