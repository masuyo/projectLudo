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
        /// <summary>
        /// Adott játékos minden játékkal külön-külön mennyit játszott
        /// </summary>
        /// <returns>
        /// Propertyk:
        /// GameName, NumberOfGames
        /// </returns>
        public List<GameWinrate> NumberOfPlayedGamesInEachTypeOfGame(string emailID)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { GameName = "Ludo", NumberOfGames = 35 });
            return gwr;
        }

        /// <summary>
        /// Adott játékos adott játéktípusban adott játéktípushoz tartozó színekkel milyen arányban nyert
        /// </summary>
        /// <returns>
        /// Propertyk: 
        /// Ludo esetén 4 elemű lista (kicsit más, mint az IGameStatisticsban, 
        /// mert ez halmozott chart lesz (= beleférnek a lossok is), az meg pie chart
        /// ColorName, NumberOfWins, NumberOfLosses
        /// </returns>
        public List<GameWinrate> PlayerColorWinrate(string emailID, string gametype)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { ColorName = "Blue piece", NumberOfWins = 3, NumberOfLosses = 7 });
            gwr.Add(new GameWinrate() { ColorName = "Red piece", NumberOfWins = 2, NumberOfLosses = 8 });
            gwr.Add(new GameWinrate() { ColorName = "Yellow piece", NumberOfWins = 1, NumberOfLosses = 4 });
            // höhö, green peace
            gwr.Add(new GameWinrate() { ColorName = "Green piece", NumberOfWins = 2, NumberOfLosses = 8 });
            return gwr;
        }

        public List<GameWinrate> PlayerWinrate(string emailID)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { GameName = "Ludo", NumberOfWins = 34, NumberOfLosses = 55 });
            return gwr;
        }

        /// <summary>
        /// Adott játékos átlagosan mennyi időt tölt az egyes játéktípusokban (játékkal : D)
        /// (ha csak körök száma van, az is jó, h átlagosan hány kört megy)
        /// </summary>
        /// <returns>
        /// GameName, AverageNumberOfTurns
        /// </returns> 
        public List<GameWinrate> UserAverageTimeSpent(string emailID)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { GameName = "Ludo", AverageNumberOfTurns = 20 });
            return gwr;
        }

        /// <summary>
        /// Adott játékos leghosszabb játéka az egyes játéktípusokban
        /// </summary>
        /// <returns>
        /// GameName, NumberOfTurnsOfTheLongestGame
        /// </returns>
        public List<GameWinrate> UserLongestGame(string emailID)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { GameName = "Ludo", NumberOfTurnsOfTheLongestGame = 52 });
            return gwr;
        }

        /// <summary>
        /// Adott játékos legrövidebb játéka az egyes játéktípusokban
        /// </summary>
        /// <returns>
        /// GameName, NumberOfTurnsOfTheShortestGame
        /// </returns>
        public List<GameWinrate> UserShortestGame(string emailID)
        {
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr.Add(new GameWinrate() { GameName = "Ludo", NumberOfTurnsOfTheShortestGame = 16 });
            return gwr;
        }
    }
}