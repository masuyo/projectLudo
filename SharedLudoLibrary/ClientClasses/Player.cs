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

        private int id;
        public int ID
        {
            get { return id; }
        }

        private PlayerColor color;
        public PlayerColor Color
        {
            get { return color; }
        }

        public Player(int id, PlayerColor color)
        {
            this.id = id;
            this.color = color;
        }

    }
}
