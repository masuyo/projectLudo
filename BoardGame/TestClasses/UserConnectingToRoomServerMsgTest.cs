using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Interfaces;

namespace BoardGame.TestClasses
{
    class UserConnectingToRoomServerMsgTest 
    {
        private List<IRoom> roomList;
        private IRoom selectedRoom;
        private string userName;

        public List<IRoom> RoomList
        {
            get
            {
                return roomList;
            }
        }

        public IRoom SelectedRoom
        {
            get
            {
                return selectedRoom;
            }
        }

        public string Username
        {
            get
            {
                return userName;
            }
        }
        public UserConnectingToRoomServerMsgTest(string uname, IRoom selectedRoom, List<IRoom> roomList)
        {
            this.userName = uname;
            this.selectedRoom = selectedRoom;
            this.roomList = roomList;
        }
    }
}
