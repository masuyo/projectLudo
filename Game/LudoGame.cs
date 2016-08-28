using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LudoGame : GenericInterfacesandClasses.Game
    {
        public LudoGame(LudoPlayer player1, LudoPlayer player2, LudoPlayer player3, LudoPlayer player4, DateTime newcreationtime) : base(player1,player2,player3,player4)
        {
            Rounds = 0;
            creationTime = newcreationtime;
        }

        public GenericInterfacesandClasses.Player nextplayer { get; set; }
        public int Rounds { get; set; }
        public DateTime creationTime { get; private set; }
    }
}
