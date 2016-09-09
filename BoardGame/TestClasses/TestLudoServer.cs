using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Views;
using SharedLudoLibrary.Interfaces.Server;
using SharedLudoLibrary.Interfaces;
using SharedLudoLibrary.ClientClasses;

namespace BoardGame.TestClasses
{
    class TestLudoServer : ILudoServer
    {
        Random rnd = new Random();
        public List<IRoom> GetAllRoomList()
        {
            List<IRoom> temp = new List<IRoom>();

            temp.Add(new Room(0, 105, "LUDOOOOO", "LUDOOOOO")); // cheat:: doesn t show in list, LudoStart for test only
            temp.Add(new Room(3, 100, "Room#100", String.Empty));
            temp.Add(new Room(2, 101, "Room#111", "pswd"));
            temp.Add(new Room(1, 102, "Room#102", "pswd"));
            temp.Add(new Room(4, 103, "Room#113", String.Empty));
            temp.Add(new Room(2, 104, "Room#110", "aaa"));

            return temp;

        }

        public List<string> GetGameTypes()
        {
            return Enum.GetNames(typeof(GameType)).ToList();
        }

        public List<IUser> GetPlayersInRoom(IRoom room)
        {
            int i = rnd.Next(1, 5);
            List<IUser> users = new List<IUser>();
            while (i > 0)
            {
                users.Add(new User(i, "user##" + i));
                i--;
            }
            return users;
        }
    }
}
