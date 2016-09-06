using pageLudo.FakeData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pageLudo.FakeData.DataClasses;

namespace pageLudo.FakeData.MethodClasses
{
    public class UserActions : IUserActions
    {

        public bool Friend(string BeMyFriendEmailID, string IMightBecomeYourFriendEmailID)
        {
            throw new NotImplementedException();
        }

        public bool FriendAccept(string IWillBeYourFriendEmailID, string ThanksForAcceptingMeAsYourFriendEmailID)
        {
            throw new NotImplementedException();
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
            return new UserData() { Username = "Engem kerestél", EmailID = "keres@email.com" };
        }
        List<UserData> ud = new List<UserData>();
        public List<UserData> UsernameSearch(string username)
        {
            List<UserData> ud = new List<UserData>();
            ud.Add(new UserData() { Username = "Adam", EmailID = "adam@email.com" });
            ud.Add(new UserData() { Username = "Adam", EmailID = "adam2@email.com" });
            ud.Add(new UserData() { Username = "Adam", EmailID = "adam3@email.com" });
            ud.Add(new UserData() { Username = "Kate", EmailID = "kate@email.com" });
            ud.Add(new UserData() { Username = "Kate", EmailID = "kate2@email.com" });
            ud.Add(new UserData() { Username = "Kate", EmailID = "kate3@email.com" });
            ud.Add(new UserData() { Username = "Kate", EmailID = "kate4@email.com" });

            List<UserData> searchResult = new List<UserData>();
            foreach (var u in ud)
            {
                if (u.Username == username)
                {
                    searchResult.Add(u);
                }
            }
            return searchResult;
        }
    }
}