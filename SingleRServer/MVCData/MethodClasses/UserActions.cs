using SignalRServer.MVCData.Interfaces;
using System;
using System.Collections.Generic;
using SignalRServer.MVCData.DataClasses;

using Entities;
using Repository.TableRepositories;

namespace SignalRServer.MVCData.MethodClasses
{
    public class UserActions : IUserActions
    {
        public void Friend(string BeMyFriendEmailID, string IMightBecomeYourFriendEmailID)
        {
          
        }

        public void FriendAccept(string IWillBeYourFriendEmailID, string ThanksForAcceptingMeAsYourFriendEmailID)
        {

        }

        public bool Register(string Username, string Password, string EmailID)
        {
            using (UsersRepository repo = new UsersRepository())
            {
                if (repo.GetByEmailID(EmailID) != null) return false;
                repo.Insert(new User() { Username = Username, Password = Password, EmailID = EmailID });
                return true;
            }
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
            using (DatabaseEntities ED = new DatabaseEntities())
            {
                UsersRepository userrepo = new UsersRepository();
                User searcher = userrepo.GetByEmailID(searcherEmailID);
                User searched = userrepo.GetByEmailID(emailID);

                FriendConnectionsRepository friendrepo = new FriendConnectionsRepository();
                string arewefriends = "false";
                string friendedyou = "false";
                string friendedme = "false";
                foreach (var item in friendrepo.GetAll())
                {
                    if (item.UserID == searcher.UserID && item.FriendUserID == searched.UserID)
                    {
                        friendedyou = "true";
                    }
                    if (item.UserID == searched.UserID && item.FriendUserID == searcher.UserID)
                    {
                        friendedme = "true";
                    }
                }
                if (friendedyou == "true" && friendedme == "true") arewefriends = "true";

                return new UserData() { Username = searched.Username, AreWeFriends = arewefriends, FriendedMe = friendedme, FriendedYou = friendedyou, EmailID = searched.EmailID };
            }
        }

        // AreWeFriends akkor true, ha végigmész a db friend tábláján, és mindkét usernél megtalálod a másikat
        public List<UserData> UsernameSearch(string username, string searcherEmailID)
        {
            List<UserData> searchResultList = new List<UserData>();

            using (DatabaseEntities ED = new DatabaseEntities())
            {
                UsersRepository userrepo = new UsersRepository();
                foreach (var item in userrepo.GetByName(username))
                {
                    searchResultList.Add(EmaildIDSearch(item.EmailID, searcherEmailID));
                }
            }

            return searchResultList;
        }
    }
}