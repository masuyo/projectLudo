using SignalRServer.MVCData.Interfaces;
using SignalRServer.MVCData.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.TableRepositories;
using Entities;

namespace SignalRServer.MVCData.MethodClasses
{
    public class AdminActions : IAdminActions
    {
        public bool DeleteUser(string userEmailID)
        {
            throw new NotImplementedException();
        }

        public List<UserData> GetAllUsers()
        {
            List<UserData> allUsers = new List<UserData>();
            using (UsersRepository userrepo = new UsersRepository())
            {
                foreach (var user in userrepo.GetAll())
                {
                    allUsers.Add(new UserData() { Username = user.Username, EmailID = user.EmailID, Role = user.Role });
                }
            }
            return allUsers;
        }

        public bool UserSetting(string userEmailID, string username, string password, string emailID, string role)
        {
            using (UsersRepository userrepo = new UsersRepository())
            {
                User user = userrepo.GetByEmailID(userEmailID);
                if (user == null) return false;
                if (username != null) userrepo.UpdateName(user.UserID, username);
                if (password != null)
                {
                    //HASH
                    userrepo.UpdatePassword(user.UserID, password);
                }
                if (emailID != null) userrepo.UpdateEmailID(user.UserID, emailID);
                if (role != null) userrepo.UpdateRole(user.UserID, role);
                // role update goes here
                return true;
            }
        }
    }
}
