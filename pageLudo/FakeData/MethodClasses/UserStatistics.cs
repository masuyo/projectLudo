using pageLudo.FakeData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pageLudo.FakeData.DataClasses;

namespace pageLudo.FakeData.MethodClasses
{
    public class UserStatistics : IUserStatistics
    {
        public List<GameWinrate> PlayerWinrate(string emailID)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { GameName = "Ludo", NumberOfWins = 34, NumberOfLosses = 55 });
            return gwr;
        }
    }
}