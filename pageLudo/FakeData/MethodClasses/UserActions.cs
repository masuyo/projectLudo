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

        public UserData EmaildIDSearch(string emailID, string searcherEmailID)
        {
            return new UserData() { Username = "Engem kerestél", EmailID = "engemkeresett@email.com"};
        }

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

        List<UserData> IUserActions.UsernameSearch(string username, string searcherEmailID)
        {
            throw new NotImplementedException();
        }
    }
}