using BoardGame.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Interfaces;

namespace BoardGame.TestClasses
{
    class TestLudoServer : ILudoServer
    {
        public void Befriend(int playerID, int friendPlayerID)
        {
            throw new NotImplementedException();
        }

        public bool ConnectUserToRoom(IUser user, IRoom room)
        {
            throw new NotImplementedException();
        }

        public IRoom CreateRoom(IRoom newRoom)
        {
            throw new NotImplementedException();
        }

        public List<IRoom> GetAllRoomList()
        {
            List<IRoom> temp = new List<IRoom>();

            temp.Add(new TestRoom(0, 105, "LUDOOOOO", "LUDOOOOO")); // cheat:: doesn t show in list, LudoStart for test only
            temp.Add(new TestRoom(3, 100, "Room#100", String.Empty));
            temp.Add(new TestRoom(2, 101, "Room#111", "pswd"));
            temp.Add(new TestRoom(1, 102, "Room#102", "pswd"));
            temp.Add(new TestRoom(4, 103, "Room#113", String.Empty));
            temp.Add(new TestRoom(2, 104, "Room#110", "aaa"));

            return temp;

        }

        public List<string> GetGameTypes()
        {
            throw new NotImplementedException();
        }

        public string GetOverall(int playerID)
        {
            throw new NotImplementedException();
        }

        public List<IUser> GetPlayersInRoom(IRoom room)
        {
            throw new NotImplementedException();
        }

        public int Login(string userName, string encryptedPassword, string selectedGameType = "LUDO")
        {
            return 1; //if data correct return ID else return -1;
        }

        public IGameInfo Move(int playerID, int actPoz, int destPoz)
        {
            throw new NotImplementedException();
        }

        public IStartGameInfo Start(int playerID)
        {
            throw new NotImplementedException();
        }
    }
}
