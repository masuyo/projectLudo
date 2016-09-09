using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces.Server
{
    interface IChatServer
    {
        void ConnectToRoom(int userID, IRoom room); //csak egy groupban levoknek kuldi le a msg t
        void Message(int playerID, string text);
    }
}
