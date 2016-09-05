using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.RoomToGame
{
    interface IUserConnectingToRoomServerMsg : IRoomListFromServer
    {
        string Username { get; }
        IRoom SelectedRoom { get; }
        
        
    }
}
