using SignalRServer.MVCData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRServer.MVCData.DataClasses;

namespace SignalRServer.MVCData.MethodClasses
{
    public class UserStatistics : IUserStatistics
    {
        public List<GameWinrate> NumberOfPlayedGamesInEachTypeOfGame(string emailID)
        {
            throw new NotImplementedException();
        }

        public List<GameWinrate> PlayerColorWinrate(string emailID, string gametype)
        {
            throw new NotImplementedException();
        }

        public List<GameWinrate> PlayerWinrate(string emailID)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { GameName = "Ludo", NumberOfWins = 34, NumberOfLosses = 55 });
            return gwr;
        }

        public List<GameWinrate> UserAverageTimeSpent(string emailID)
        {
            throw new NotImplementedException();
        }

        public List<GameWinrate> UserLongestGame(string emailID)
        {
            throw new NotImplementedException();
        }

        public List<GameWinrate> UserShortestGame(string emailID)
        {
            throw new NotImplementedException();
        }
    }
}