using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces.Client
{
    public interface ILudoClient
    {
        void SendGameTypes(List<string> gameTypesList);
        void SendLogin(int ID);
        
        //Login Hiba esetén
        void SendLoginError();

        void SendAllRoomList(List<IRoom> allRoomList);
        void SendUsersInRoom(List<IUser> usersInRoom);
        void SendCreateRoom(IRoom createdRoom);
        void SendConnectUserToRoom(bool connectedToRoom);

        void SendStart(IStartGameInfo startGameInfo);
        void SendMove(IGameInfo gameInfo);
        void SendOverall(string linkToPage);
    }
}
