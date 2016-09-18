using Newtonsoft.Json;
using SignalRServer.MVCData.DataClasses;
using SignalRServer.MVCData.MethodClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult Ludo()
        {
            GameStatistics gs = new GameStatistics();

            // 1. pie chart
            List<GameWinrate> gwrList = new List<GameWinrate>();
            gwrList = gs.ColorPieChartData("Ludo");

            ArrayList header = new ArrayList { "Colors", "Wins"};
            ArrayList data = new ArrayList { header };
            foreach (var item in gwrList)
            {
                data.Add(new ArrayList { item.ColorName, item.NumberOfWins});
            }

            string dataStr = JsonConvert.SerializeObject(data, Formatting.None);
            ViewBag.GameData = new HtmlString(dataStr);

            // 2. pie chart
            List<GameWinrate> gwrList2 = new List<GameWinrate>();
            gwrList2 = gs.GamePopularity();

            ArrayList header2 = new ArrayList { "Game", "Number of games" };
            ArrayList data2 = new ArrayList { header2 };
            foreach (var item in gwrList2)
            {
                data2.Add(new ArrayList { item.GameName, item.NumberOfGames });
            }

            string dataStr2 = JsonConvert.SerializeObject(data2, Formatting.None);
            ViewBag.GameData2 = new HtmlString(dataStr2);
            return View();
        }

        //public ActionResult Chess()
        //{
        //    return View();
        //}
    }
}