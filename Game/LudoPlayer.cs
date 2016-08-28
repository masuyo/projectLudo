using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LudoPlayer : GenericInterfacesandClasses.Player
    {
        public LudoPlayer(string newname="LudoPlayer",puppetColor newcolor=puppetColor.Default,int pos1=0,int pos2=0,int pos3=0,int pos4=0) : base(newname)
        {
            color = newcolor;
            puppet1position = pos1;
            puppet2position = pos2;
            puppet3position = pos3;
            puppet4position = pos4;
        }

        public puppetColor color { get; set; }

        public int puppet1position { get; set; }
        public int puppet2position { get; set; }
        public int puppet3position { get; set; }
        public int puppet4position { get; set; }
    }
}
