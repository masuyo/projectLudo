using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces.Server
{
    interface ILudoServer
    {
        void GetGameTypes();
        void GetLogin(string userName, string encryptedPassword, string selectedGameType = "LUDO");
        void GetAllRoomList();
        void GetPlayersInRoom(IRoom room);
        void GetCreateRoom(IRoom newRoom);
        void GetConnectUserToRoom(IUser user, IRoom room);
        void GetStart(int playerID);
        void GetMove(int playerID, int actPoz, int destPoz);
        void GetOverall(int playerID);
        void Befriend(int playerID, int friendPlayerID);
    }
}
