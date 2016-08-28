using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GenericInterfacesandClasses
{
    abstract public class Game
    {
        public Game(params Player[] newplayers)
        {
            Players.AddRange(newplayers);
        }
        public List<Player> Players { get; set; }
    }
}
