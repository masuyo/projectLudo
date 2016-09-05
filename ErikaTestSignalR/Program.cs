using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ErikaTestSignalR
{
    class Program
    {
        public static IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:8080";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting server...");
            Task.Run(() => StartServer());

            //System.MissingMemberException' occurred in Microsoft.Owin.Hosting.dll
            //You have to include Microsoft.Owin.Host.HttpListener.dll in your project references - nuGet
            Console.ReadKey();
                        
        }
        private static void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException)
            {
                Console.WriteLine("A server is already running at " + ServerURI);
                return;
            }
            Console.WriteLine("Server started at " + ServerURI);
        }
    }
}
