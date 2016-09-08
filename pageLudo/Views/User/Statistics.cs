using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using pageLudo.FakeData.MethodClasses;
using pageLudo.FakeData.DataClasses;

namespace pageLudo.User
{
    public class Statistics : Controller
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] GameWinRateGetChartData(string sessionEmailID)
        {
            UserStatistics us = new UserStatistics();
            List<GameWinrate> gwr = new List<GameWinrate>();
            gwr = us.PlayerWinrate(sessionEmailID);

            var chartData = new object[gwr.Count + 1];
            chartData[0] = new object[] { "Game", "Win", "Lose" };

            int i = 0;
            foreach (var row in gwr)
            {
                i++;
                chartData[i] = new object[] { row.GameName, row.NumberOfWins, row.NumberOfLosses };
            }

            return chartData;
        }
    }
}