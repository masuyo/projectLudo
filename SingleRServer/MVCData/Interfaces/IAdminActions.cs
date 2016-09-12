using SignalRServer.MVCData.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRServer.MVCData.Interfaces
{
    interface IAdminActions
    {
        // vissza kell kapnia: username, emailid, role; ill kéne egy admin user is admine role-lal a db-be *-*
        List<UserData> GetAllUsers();

        bool UserSetting(string userEmailID, string username, string password, string emailID, string role);

        bool DeleteUser(string userEmailID);
    }
}
