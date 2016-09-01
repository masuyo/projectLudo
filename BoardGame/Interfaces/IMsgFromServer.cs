using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces
{
    interface IMsgFromServer
    {
        int ActivePlayerID { get; }
        string Msg { get; }
        List<IMan> MenList { get; }
        bool End { get; }
        bool OnManHit { get; } //redraw full visual
        bool Reroll { get; }

        int Dice1 { get; }
        int Dice2 { get; }


    }
}
