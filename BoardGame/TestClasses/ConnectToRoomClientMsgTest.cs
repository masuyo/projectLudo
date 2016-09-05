using BoardGame.Interfaces.RoomToGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Interfaces;

namespace BoardGame.TestClasses
{
    class ConnectToRoomClientMsgTest : IConnectToRoomClientMsg
    {
        IRoom room;
        public ConnectToRoomClientMsgTest(IRoom room)
        {
            this.room = room;
        }
        public IRoom ConnetToRoomMsg
        {
            get
            {
                return room;
            }
        }
    }
}
