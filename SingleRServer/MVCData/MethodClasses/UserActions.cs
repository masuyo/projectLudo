using SignalRServer.MVCData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRServer.MVCData.DataClasses;

namespace SignalRServer.MVCData.MethodClasses
{
    public class UserActions : IUserActions
    {
        List<UserData> ud = new List<UserData>();
        List<UserData> searchResultList = new List<UserData>();

        public void Friend(string BeMyFriendEmailID, string IMightBecomeYourFriendEmailID)
        {
            foreach (var u in searchResultList)
            {
                if (u.EmailID == BeMyFriendEmailID && u.FriendedYou != "true")
                {
                    u.FriendedYou = "true";
                }
            }
        }

        public void FriendAccept(string IWillBeYourFriendEmailID, string ThanksForAcceptingMeAsYourFriendEmailID)
        {
            foreach (var u in searchResultList)
            {
                if (u.EmailID == ThanksForAcceptingMeAsYourFriendEmailID)
                {
                    u.AreWeFriends = "true";
                }
            }
        }

        public bool Register(string Username, string Password, string EmailID)
        {
            throw new NotImplementedException();
        }

        public bool Unfriend(string YouAreNotMyFriendAnymoreEmailID, string IDidntWantYouAnywayEmailID)
        {
            throw new NotImplementedException();
        }

        public UserData UserCheck(string EmailID, string Password)
        {
            throw new NotImplementedException();
        }

        public UserData EmaildIDSearch(string emailID, string searcherEmailID)
        {
            return new UserData() { Username = "Engem kerestél", EmailID = "keres@email.com", AreWeFriends = "false" };
        }

        // AreWeFriends akkor true, ha végigmész a db friend tábláján, és mindkét usernél megtalálod a másikat
        public List<UserData> UsernameSearch(string username, string searcherEmailID)
        {
            //csak teszteléshez rakom kívülre
            //List<UserData> ud = new List<UserData>();
            ud.Add(new UserData() { Username = "Adam", EmailID = "adam@email.com" , AreWeFriends = "true", FriendedYou= "true", FriendedMe= "true" });
            ud.Add(new UserData() { Username = "Adam", EmailID = "adam2@email.com", AreWeFriends = "false", FriendedYou = "true", FriendedMe = "false" });
            ud.Add(new UserData() { Username = "Adam", EmailID = "adam3@email.com", AreWeFriends = "false", FriendedYou = "true", FriendedMe = "false" });
            ud.Add(new UserData() { Username = "Kate", EmailID = "kate@email.com", AreWeFriends = "true", FriendedYou = "true", FriendedMe = "true" });
            ud.Add(new UserData() { Username = "Kate", EmailID = "kate2@email.com", AreWeFriends = "true", FriendedYou = "true", FriendedMe = "true" });
            ud.Add(new UserData() { Username = "Kate", EmailID = "kate3@email.com", AreWeFriends = "true", FriendedYou = "true", FriendedMe = "true" });
            ud.Add(new UserData() { Username = "Kate", EmailID = "kate4@email.com", AreWeFriends = "true", FriendedYou = "true", FriendedMe = "true" });
            ud.Add(new UserData() { Username = "Neville", EmailID = "neville@email.com", AreWeFriends = "false", FriendedYou = "false", FriendedMe = "false" });
            ud.Add(new UserData() { Username = "Eve", EmailID = "eve@email.com", AreWeFriends = "true", FriendedYou = "true", FriendedMe = "false" });

            //teszteléshez
            //List<UserData> searchResultList = new List<UserData>();
            foreach (var u in ud)
            {
                if (string.Equals(u.Username,username, StringComparison.OrdinalIgnoreCase))
                {
                    searchResultList.Add(u);
                }
            }
            return searchResultList;
        }
    }
}