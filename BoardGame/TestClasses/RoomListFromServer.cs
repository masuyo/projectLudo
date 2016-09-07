using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Interfaces;

namespace BoardGame.TestClasses
{
    class RoomListFromServer
    {
        private List<IRoom> roomList;
        public List<IRoom> RoomList
        {
            get
            {
                return roomList;
            }
        }

        public RoomListFromServer(List<IRoom> roomList)
        {
            this.roomList = roomList;
        }
    }
}
