using BoardGame.Interfaces.Ludo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.Server
{
    interface ILudoServer
    {
        /// <summary>
        /// before login
        /// </summary>
        /// <returns>string List with game types</returns>
        List<string> GetGameTypes();
        /// <summary>
        /// when client signs in Login() checks data, lets user in if params OK
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="encryptedPassword"></param>
        /// <param name="selectedGameType"></param>
        /// <returns>int userID or empty string if login failed</returns>
        int Login(string userName, string encryptedPassword, string selectedGameType = "LUDO");
        /// <summary>
        /// gets all rooms 
        /// </summary>
        /// <returns>IRoom list of rooms</returns>
        List<IRoom> GetAllRoomList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns>IUser list of users connected to room</returns>
        List<IUser> GetPlayersInRoom(IRoom room);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRoom"></param>
        /// <returns></returns>
        IRoom CreateRoom(IRoom newRoom);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        bool ConnectUserToRoom(IUser user, IRoom room);
        IGameInfo Move(int playerID, int actPoz, int destPoz);
        string GetOverall(int playerID);
        void Befriend(int playerID, int friendPlayerID);

    }
}
