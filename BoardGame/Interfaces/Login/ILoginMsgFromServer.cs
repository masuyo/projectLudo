using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.Login
{
    interface ILoginMsgFromServer
    {   
        bool AuthenticationSuccess { get; }
        string UserName { get; } //?
        string ConnectionID { get; } 

    }
}
