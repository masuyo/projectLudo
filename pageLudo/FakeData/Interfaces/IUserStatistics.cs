using pageLudo.FakeData.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pageLudo.FakeData.Interfaces
{
    interface IUserStatistics
    {
        //Adott játékosnak minden játéktípusban(Ludóban) winek száma, lose-ok száma
        List<GameWinrate> PlayerWinrate(string emailID);

        //    //Adott játékos adott játéktípusban adott játéktípushoz tartozó színekkel milyen arányban nyert
        //    List<Something> PlayerColorWinrate(string emailID, string gametype);

        //    //Adott játékos minden játékkal külön-külön mennyit játszott (játékok nevét meg a játékok számát szeretném összekötve visszakapni)
        //    List<Something> NumberOfPlayedGamesInEachTypeOfGame(string emailID);

        //    //Adott játékos átlagosan mennyi időt tölt az egyes játéktípusokban (játékkal : D) (ha csak körök száma van, az is jó, h átlagosan hány kört megy)
        //    List<Something> UserAverageTimeSpent(string emailID);

        //    //Adott játékos leghosszabb játéka az egyes játéktípusokban
        //    List<Something> UserLongestGame(string emailID);

        //    //Adott játékos legrövidebb játéka az egyes játéktípusokban
        //    List<Something> UserShortestGame(string emailID);
    }
}
