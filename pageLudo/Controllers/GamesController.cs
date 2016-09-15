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
            return View();
        }

        //public ActionResult Chess()
        //{
        //    return View();
        //}
    }
}