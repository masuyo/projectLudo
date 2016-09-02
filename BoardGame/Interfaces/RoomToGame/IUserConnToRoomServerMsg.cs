using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.RoomToGame
{
    interface IUserConnToRoomServerMsg : IRoomListFromServer
    {
        string Username { get; }
        bool CoonectionSuccess { get; }


    }
}
