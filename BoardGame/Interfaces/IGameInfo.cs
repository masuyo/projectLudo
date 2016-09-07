using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces
{
    interface IGameInfo
    {
        int ActivePlayerID { get; }
        string Msg { get; }
        List<IPuppet> PuppetList { get; }
        bool End { get; }
        bool OnManHit { get; } //redraw full visual
        bool Reroll { get; }

        int Dice1 { get; }
        int Dice2 { get; }
        
    }
}