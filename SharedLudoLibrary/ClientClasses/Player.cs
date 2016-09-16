using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.ClientClasses
{
    public enum PlayerColor { RED, GREEN, BLUE, YELLOW }
    public class Player : IPlayer
    {
        public Player()
        {

        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

       
        public int ID
        {
            get;set;
        }

        public PlayerColor Color
        {
            get; set;
        }
        public Player(int id, PlayerColor color)
        {
            ID = id;
            Color = color;
        }

    }
}
