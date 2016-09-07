using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRServer.Interfaces4MVC
{
    interface IGameStatisctics
    {
        //játéktípusok sorrendben, legtöbbet játszott-tól a legkevesebbig
        List<Something> GamePopularity(); //Gamenumbernek gondoltam egy név meg egy indított játékok száma propertyt, aztán sorbarendezés stb., de lehet százalékos eloszlás is, ahogy gondolod

        //Adott játéktípusban melyik színnel milyen százalékos eloszlással nyertek
        List<Something> ColorPieChartData(string gametype);

        //Esetleg egy olyan, hogy aktuálisan hány Online játékos van a WPF kliensben?
        int NumberOfOnlineWPFUsers();
    }
}
