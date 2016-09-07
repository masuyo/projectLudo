using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GenericInterfacesandClasses
{
    abstract public class Game<TPlayer> where TPlayer:Player
    {
        public Game(params TPlayer[] newplayers)
        {
            Players = new List<TPlayer>();
            Players.AddRange(newplayers);
        }

        public List<TPlayer> Players { get; set; }
    }
}
