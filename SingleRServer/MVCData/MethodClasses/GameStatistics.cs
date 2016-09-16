using SignalRServer.MVCData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRServer.MVCData.DataClasses;

namespace SignalRServer.MVCData.MethodClasses
{
    public class GameStatistics : IGameStatistics
    {
        /// <summary>
        /// Adott játéktípusban melyik színnel milyen százalékos eloszlással nyertek
        /// </summary>
        /// <returns>
        /// Propertyk: 
        /// Ludo esetében pl 4 elemű lista a 4 szín miatt: 
        /// ColorName, NumberOfWins
        /// Sakk esetében 2 elemű lenne
        /// stb.
        /// </returns>
        public List<GameWinrate> ColorPieChartData(string gametype)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { ColorName = "Blue winners", NumberOfWins = 62 });
            gwr.Add(new GameWinrate() { ColorName = "Red winners", NumberOfWins = 45 });
            gwr.Add(new GameWinrate() { ColorName = "Yellow winners", NumberOfWins = 33 });
            gwr.Add(new GameWinrate() { ColorName = "Green winners", NumberOfWins = 40 });
            return gwr;
        }

        // Propertyk: GameName(pl.Ludo), NumberOfGames
        public List<GameWinrate> GamePopularity()
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { GameName = "Ludo", NumberOfGames = 153 });
            return gwr;
        }

        public int NumberOfOnlineWPFUsers()
        {
            int onlinePlayers = 15;
            return onlinePlayers;
        }
    }
}