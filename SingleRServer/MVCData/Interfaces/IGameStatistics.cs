using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRServer.MVCData.Interfaces
{
    interface IGameStatistics
    {
        /// <summary>
        /// játéktípusok sorrendben, legtöbbet játszott-tól a legkevesebbig
        /// </summary>
        /// <returns>
        /// Propertyk: GameName (pl. Ludo), NumberOfGames
        /// </returns>
        //List<GameNumber> GamePopularity();

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
        //List<Something> ColorPieChartData(string gametype);

        //Esetleg egy olyan, hogy aktuálisan hány Online játékos van a WPF kliensben?
        int NumberOfOnlineWPFUsers();
    }
}
