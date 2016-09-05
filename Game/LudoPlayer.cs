using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LudoPlayer : GenericInterfacesandClasses.Player
    {
        public LudoPlayer(string newname = "LudoPlayer", puppetColor newcolor = puppetColor.Default, int pos1 = 0, int pos2 = 0, int pos3 = 0, int pos4 = 0, int seq = 0) : base(newname)
        {
            color = newcolor;
            Puppets = new int[4];
            Puppets[0] = pos1;
            Puppets[1] = pos2;
            Puppets[2] = pos3;
            Puppets[3] = pos4;
            sequence = seq;
        }

        public puppetColor color { get; set; }
        public int sequence { get; set; }
        public int[] Puppets {get;set;}
    }
}
