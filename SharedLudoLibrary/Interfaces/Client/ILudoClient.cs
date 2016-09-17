using SharedLudoLibrary.ClientClasses;
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

        void SendAllRoomList(List<Room> allRoomList);
        void SendUsersInRoom(List<User> usersInRoom);
        void SendCreateRoom(Room createdRoom);
        void SendConnectUserToRoom(bool connectedToRoom);

        void SendStart(StartGameInfo startGameInfo);
        void SendMove(GameInfo gameInfo);
        void SendOverall(string linkToPage);
        void SendForgot(string linkToPage);
    }
}
