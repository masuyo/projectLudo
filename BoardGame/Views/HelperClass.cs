using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Views
{
    class HelperClass
    {
        public static IHubProxy HubProxy { get; set; }
        static string connString = "http://localhost:8080/signalr";
        public static HubConnection Connection { get; set; }

        private static string guID;
        private static string userName;
        public static string GUID
        {
            get { return guID; }

            set { guID = value; }
        }
        public static string UserName
        {
            get { return userName; }

            set { userName = value; }
        }

        public static string ConnString
        {
            get { return connString; }
        }
    }
}
