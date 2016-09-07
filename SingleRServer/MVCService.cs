using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRServer.Interfaces4MVC;

using Repository;
using Entities;
using SignalRServer.MVCData.DataClasses;

namespace SignalRServer
{
    public class MVCService : IUserActions
    {
        DatabaseEntities DE;

        public MVCService()
        {
            DE = new DatabaseEntities();
        }

        public UserData EmaildIDSearch(string emailID, string searcherEmailID)
        {
            throw new NotImplementedException();
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
            Repository.TableRepositories.UsersRepository repo = new Repository.TableRepositories.UsersRepository(DE);
            return repo.Register(Username, Password, EmailID);
        }

        public bool Unfriend(string YouAreNotMyFriendAnymoreEmailID, string IDidntWantYouAnywayEmailID)
        {
            throw new NotImplementedException();
        }

        public Something UserCheck(string EmailID, string Password)
        {
            throw new NotImplementedException();
        }

        public List<UserData> UsernameSearch(string username, string searcherEmailID)
        {
            throw new NotImplementedException();
        }

        void IUserActions.Friend(string BeMyFriendEmailID, string IMightBecomeYourFriendEmailID)
        {
            throw new NotImplementedException();
        }

        void IUserActions.FriendAccept(string IWillBeYourFriendEmailID, string ThanksForAcceptingMeAsYourFriendEmailID)
        {
            throw new NotImplementedException();
        }

        UserData IUserActions.UserCheck(string EmailID, string Password)
        {
            throw new NotImplementedException();
        }
    }
}
