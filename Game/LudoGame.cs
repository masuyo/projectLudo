using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LudoGame : GenericInterfacesandClasses.Game<LudoPlayer>
    {
        public LudoGame(LudoPlayer player1, LudoPlayer player2, LudoPlayer player3, LudoPlayer player4, DateTime newcreationtime) : base(player1,player2,player3,player4)
        {
            Rounds = 0;
            creationTime = newcreationtime;
            nextplayer = Players.Where(akt => akt.sequence == 1).SingleOrDefault();

            LudoPlayer player = Players.Find(akt => akt.color == puppetColor.Red);
            player.Puppets[0] = 11;
            player.Puppets[1] = 12;
            player.Puppets[2] = 13;
            player.Puppets[3] = 14;

            player = Players.Find(akt => akt.color == puppetColor.Blue);
            player.Puppets[0] = 21;
            player.Puppets[1] = 22;
            player.Puppets[2] = 23;
            player.Puppets[3] = 24;

            player = Players.Find(akt => akt.color == puppetColor.Yellow);
            player.Puppets[0] = 31;
            player.Puppets[1] = 32;
            player.Puppets[2] = 33;
            player.Puppets[3] = 34;

            player = Players.Find(akt => akt.color == puppetColor.Green);
            player.Puppets[0] = 41;
            player.Puppets[1] = 42;
            player.Puppets[2] = 43;
            player.Puppets[3] = 44;
        }

        public LudoPlayer nextplayer { get; set; }
        public int Rounds { get; set; }
        public DateTime creationTime { get; private set; }
    }
}
