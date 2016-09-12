using SignalRServer.MVCData.Interfaces;
using SignalRServer.MVCData.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.TableRepositories;

namespace SignalRServer.MVCData.MethodClasses
{
    public class AdminActions : IAdminActions
    {
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
    }
}
