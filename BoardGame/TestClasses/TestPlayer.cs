using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.TestClasses
{
    enum PlayerColor { RED, GREEN, BLUE, YELLOW }
    class TestPlayer : IPlayer
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

        public TestPlayer(int id, PlayerColor color)
        {
            this.id = id;
            this.color = color;
        }

    }
}
