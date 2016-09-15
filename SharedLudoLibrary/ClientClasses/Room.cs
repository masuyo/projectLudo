using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.ClientClasses
{
    public class Room : IRoom
    {
        

        public Room()
        {
                
        }

        public int AvailablePlaces
        {
            get;set;
        }

        public int ID
        {
            get;set;
        }

        public string Name
        {
            get;set;
        }

        public string Password
        {
            get;set;
        }

        public Room(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public Room(int places, int id, string name, string password)
        {
            AvailablePlaces = places;
            ID = id;
            Name = name;
            Password = password;
        }
    }
}
