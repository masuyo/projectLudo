using SharedLudoLibrary.ClientClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces
{
    public interface IGameInfo
    {
        int ActivePlayerID { get; set; }
        string Msg { get; set; }
        List<Puppet> PuppetList { get; set; }
        bool End { get; set; }
        bool OnManHit { get; set; } //redraw full visual
        bool Reroll { get; set; }

        int Dice1 { get; set; }
        int Dice2 { get; set; }

    }
}