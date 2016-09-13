using SignalRServer.MVCData.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRServer.MVCData.Interfaces
{
    interface IUserStatistics
    {
        /// <summary>
        /// Adott játékosnak minden játéktípusban(Ludóban) winek száma, lose-ok száma
        /// </summary>
        /// <param name="emailID"></param>
        /// <returns>
        /// Propertyk:
        /// GameName, NumberOfWins, NumberOfLosses
        /// </returns>
        List<GameWinrate> PlayerWinrate(string emailID);

        /// <summary>
        /// Adott játékos adott játéktípusban adott játéktípushoz tartozó színekkel milyen arányban nyert
        /// </summary>
        /// <returns>
        /// Propertyk: 
        /// Ludo esetén 4 elemű lista (kicsit más, mint az IGameStatisticsban, 
        /// mert ez halmozott chart lesz (= beleférnek a lossok is), az meg pie chart
        /// ColorName, NumberOfWins, NumberOfLosses
        /// </returns>
        //  List<Something> PlayerColorWinrate(string emailID, string gametype);

        /// <summary>
        /// Adott játékos minden játékkal külön-külön mennyit játszott
        /// </summary>
        /// <returns>
        /// Propertyk:
        /// GameName, NumberOfGames
        /// </returns>
        //  List<Something> NumberOfPlayedGamesInEachTypeOfGame(string emailID);

        /// <summary>
        /// Adott játékos átlagosan mennyi időt tölt az egyes játéktípusokban (játékkal : D)
        /// (ha csak körök száma van, az is jó, h átlagosan hány kört megy)
        /// </summary>
        /// <returns>
        /// GameName, AverageNumberOfTurns
        /// </returns> 
        //  List<Something> UserAverageTimeSpent(string emailID);

        /// <summary>
        /// Adott játékos leghosszabb játéka az egyes játéktípusokban
        /// </summary>
        /// <returns>
        /// GameName, NumberOfTurnsOfTheLongestGame
        /// </returns>
        //    List<Something> UserLongestGame(string emailID);

        /// <summary>
        /// Adott játékos legrövidebb játéka az egyes játéktípusokban
        /// </summary>
        /// <returns>
        /// GameName, NumberOfTurnsOfTheShortestGame
        /// </returns>
        //    List<Something> UserShortestGame(string emailID);
    }
}
