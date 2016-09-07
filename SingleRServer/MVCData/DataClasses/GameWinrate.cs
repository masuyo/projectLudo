using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRServer.MVCData.DataClasses
{
    public class GameWinrate
    {
        public string GameName { get; set; }
        public int NumberOfWins { get; set; }
        public int NumberOfLosses { get; set; }
    }
}