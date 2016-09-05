using BoardGame.Interfaces.RoomToGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Interfaces;

namespace BoardGame.TestClasses
{
    class UserConnectedToRoomServerMsgTest : IUserConnectedToRoomServerMsgTest
    {
        private IRoom selectedRoom;
        private string userName;
        public bool ConectionSuccess
        {
            get
            {
                return true;
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

        public UserConnectedToRoomServerMsgTest(string uname, IRoom selectedRoom)
        {
            this.userName = uname;
            this.selectedRoom = selectedRoom;
        }

    }
}
