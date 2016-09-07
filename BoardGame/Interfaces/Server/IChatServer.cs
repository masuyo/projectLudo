using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.Server
{
    interface IChatServer
    {
        bool ConnectToRoom(int userID, IRoom room);
        bool SendMessage(int playerID, string text);
    }
}
