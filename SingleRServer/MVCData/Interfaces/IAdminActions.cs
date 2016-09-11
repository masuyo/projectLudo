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
        // vissza kell kapnia: username, emailid, pw, role
        List<UserData> GetAllUsers();
    }
}
