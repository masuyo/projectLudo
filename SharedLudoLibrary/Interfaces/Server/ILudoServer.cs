using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces.Server
{
    public interface ILudoServer
    {
        void GetGameTypes();
        void GetLogin(string userName, string encryptedPassword, string selectedGameType = "LUDO");
        void GetAllRoomList(string guid);
        void GetUsersInRoom(string guid, IRoom room);
        void GetCreateRoom(string guid, IRoom newRoom);
        void GetConnectUserToRoom(string guid, IUser user, IRoom room);
        void GetStart(string guid, int userID);
        void GetMove(string guid, int playerID, int actPoz, int destPoz);
        void GetOverall(string guid, int playerID);
        void Befriend(string guid, int playerID, int friendPlayerID);
    }
}
