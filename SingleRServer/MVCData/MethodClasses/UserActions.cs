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
            using (UsersRepository userrepo = new UsersRepository())
            {
                using (FriendConnectionsRepository friendrepo = new FriendConnectionsRepository())
                {
                    User bemyfrienduser = userrepo.GetByEmailID(BeMyFriendEmailID);
                    User imightbecomeyourfirenduser = userrepo.GetByEmailID(IMightBecomeYourFriendEmailID);

                    if (bemyfrienduser != null && imightbecomeyourfirenduser != null)
                    {
                        FriendConnections friendconnection = new FriendConnections() { UserID = imightbecomeyourfirenduser.UserID, FriendUserID = bemyfrienduser.UserID };
                        if (friendrepo.GetByUserIDs(friendconnection.UserID, friendconnection.FriendUserID) != null) return;
                        else friendrepo.Insert(friendconnection);
                    }
                }
            }
        }

        public void FriendAccept(string IWillBeYourFriendEmailID, string ThanksForAcceptingMeAsYourFriendEmailID)
        {
            using (UsersRepository userrepo = new UsersRepository())
            {
                using (FriendConnectionsRepository friendrepo = new FriendConnectionsRepository())
                {
                    User iwillbeyourfrienduser = userrepo.GetByEmailID(IWillBeYourFriendEmailID);
                    User thanksforacceptingmeasyourfrienduser = userrepo.GetByEmailID(ThanksForAcceptingMeAsYourFriendEmailID);

                    if (iwillbeyourfrienduser != null && thanksforacceptingmeasyourfrienduser != null)
                    {
                        FriendConnections friendconnection = new FriendConnections() { UserID = iwillbeyourfrienduser.UserID, FriendUserID = thanksforacceptingmeasyourfrienduser.UserID };
                        if (friendrepo.GetByUserIDs(friendconnection.UserID, friendconnection.FriendUserID) != null) return;
                        else friendrepo.Insert(friendconnection);
                    }
                }
            }
        }

        public bool Register(string Username, string Password, string EmailID)
        {
            using (UsersRepository repo = new UsersRepository())
            {
                if (repo.GetByEmailID(EmailID) != null || repo.GetByName(Username) != null) return false;

                string guid;
                do
                {
                    guid = Guid.NewGuid().ToString();
                    if (repo.GetByGuid(guid) == null) break;
                } while (true);

                repo.Insert(new User() { Username = Username, Password = Password, EmailID = EmailID, Status = "offline", Token = "token", Role = "user", Guid = guid });
                return true;
            }
        }

        public bool Unfriend(string YouAreNotMyFriendAnymoreEmailID, string IDidntWantYouAnywayEmailID)
        {
            using (UsersRepository userrepo = new UsersRepository())
            {
                using (FriendConnectionsRepository friendrepo = new FriendConnectionsRepository())
                {
                    User youarenotmyfriendanymoreuser = userrepo.GetByEmailID(YouAreNotMyFriendAnymoreEmailID);
                    User ididntwantyouanywayuser = userrepo.GetByEmailID(IDidntWantYouAnywayEmailID);

                    if (youarenotmyfriendanymoreuser != null && ididntwantyouanywayuser != null)
                    {
                        FriendConnections friendconnection1 = new FriendConnections() { UserID = youarenotmyfriendanymoreuser.UserID, FriendUserID = ididntwantyouanywayuser.UserID };
                        FriendConnections friendconnection2 = new FriendConnections() { UserID = ididntwantyouanywayuser.UserID, FriendUserID = youarenotmyfriendanymoreuser.UserID };

                        friendconnection1 = friendrepo.GetByUserIDs(friendconnection1.UserID, friendconnection1.FriendUserID);
                        if (friendconnection1 == null) return false;
                        friendconnection2 = friendrepo.GetByUserIDs(friendconnection2.UserID, friendconnection2.FriendUserID);
                        if (friendconnection2 == null) return false;

                        friendrepo.Delete(friendconnection1);
                        friendrepo.Delete(friendconnection2);
                        return true;
                    }
                    else return false;
                }
            }
        }


        public UserData UserCheck(string EmailID, string Password)
        {
            throw new NotImplementedException();
        }

        public UserData EmaildIDSearch(string emailID, string searcherEmailID)
        {
            using (UsersRepository userrepo = new UsersRepository())
            {
                using (FriendConnectionsRepository friendrepo = new FriendConnectionsRepository())
                {

                    User searcher = userrepo.GetByEmailID(searcherEmailID);
                    User searched = userrepo.GetByEmailID(emailID);

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
        }

        // AreWeFriends akkor true, ha végigmész a db friend tábláján, és mindkét usernél megtalálod a másikat
        public List<UserData> UsernameSearch(string username, string searcherEmailID)
        {
            List<UserData> searchResultList = new List<UserData>();

            using (UsersRepository userrepo = new UsersRepository())
            {
                using (FriendConnectionsRepository friendrepo = new FriendConnectionsRepository())
                {
                    User searcher = userrepo.GetByEmailID(searcherEmailID);
                    if (searcherEmailID == null) return null;

                    List<User> users = new List<User>();
                    username=username.ToLower();

                    foreach (var user in userrepo.GetAll())
                    {
                        if (user.Username.ToLower().Contains(username)) users.Add(user);
                    }

                    foreach (var user in users)
                    {
                        UserData userdata = new UserData() { Username = user.Username, UserID = user.UserID, EmailID = user.EmailID, Role = user.Role };
                        if (friendrepo.GetByUserIDs(searcher.UserID, user.UserID) != null) userdata.FriendedYou = "true";
                        else userdata.FriendedYou = "false";
                        if (friendrepo.GetByUserIDs(user.UserID, searcher.UserID) != null) userdata.FriendedMe = "true";
                        else userdata.FriendedMe= "false";
                        if (userdata.FriendedMe == "true" && userdata.FriendedYou == "true") userdata.AreWeFriends = "true";
                        else userdata.AreWeFriends = "false";

                        searchResultList.Add(userdata);
                    }

                    return searchResultList;
                }
            }
        }

        public UserData Login(string emailID, string password)
        {
            using (UsersRepository repo = new UsersRepository())
            {
                User user = repo.GetByEmailID(emailID);
                if (user != null)
                {
                    UserData userdata = new UserData() { UserID = user.UserID, Username = user.Username, EmailID = user.EmailID };
                    if (user.Password == password) return userdata;
                    else return null;
                }
                else return null;
            }
        }

        // hasonló a regisztrációhoz, de le kell csekkolni, hogy valami null-e, mert az nem mehet a db-be fel változásként
        // ill a sessionemail alapján ki kell előtte keresni, h tudd, kit kell módosítani
        public bool ProfileSetting(string sessionEmailID, string username, string password, string emailID)
        {
            using (UsersRepository userrepo = new UsersRepository())
            {
                User user = userrepo.GetByEmailID(sessionEmailID);
                if (user == null) return false;
                if (username != null) userrepo.UpdateName(user.UserID, username);
                if (password != null) userrepo.UpdatePassword(user.UserID, password);
                if (emailID != null) userrepo.UpdateEmailID(user.UserID, emailID);

                return true;
            }
        }
    }
}