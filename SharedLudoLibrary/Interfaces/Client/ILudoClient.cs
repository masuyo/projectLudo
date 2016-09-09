using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces.Client
{
    interface ILudoClient
    {
        void SendGameTypes(List<string> gameTypesList);
        void SendLogin(int ID);

        void SendAllRoomList(List<IRoom> allRoomList);
        void SendPlayersInRoom(List<IUser> playersInRoom);
        void SendCreateRoom(IRoom createdRoom);
        void SendConnectUserToRoom(bool connectedToRoom);

        void SendStart(IStartGameInfo startGameInfo);
        void SendMove(IGameInfo gameInfo);
        void SendOverall(string linkToPage);
    }
}
