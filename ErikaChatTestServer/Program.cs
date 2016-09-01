using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ErikaChatTestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteToConsole("Starting server...");
            StartServer();


            
        }
        public IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:8080";

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            SignalR.Dispose();
            Close();
        }

        /// <summary>
        /// Starts the server and checks for error thrown when another server is already 
        /// running. This method is called asynchronously from Button_Start.
        /// </summary>
        private void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException)
            {
                WriteToConsole("A server is already running at " + ServerURI);
                this.Dispatcher.Invoke(() => ButtonStart.IsEnabled = true);
                return;
            }
            this.Dispatcher.Invoke(() => ButtonStop.IsEnabled = true);
            WriteToConsole("Server started at " + ServerURI);
        }
        ///This method adds a line to the RichTextBoxConsole control, using Dispatcher.Invoke if used
        /// from a SignalR hub thread rather than the UI thread.
        public void WriteToConsole(String message)
        {
            if (!(RichTextBoxConsole.CheckAccess()))
            {
                this.Dispatcher.Invoke(() =>
                    WriteToConsole(message)
                );
                return;
            }
            RichTextBoxConsole.AppendText(message + "\r");
        }
    }
    /// <summary>
    /// Used by OWIN's startup process. 
    /// </summary>
    class Startup
    {

        //itt történik hogy összekapcsolja a hubokat, neked nem kell 
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    /// <summary>
    /// Echoes messages sent using the Send message by calling the
    /// addMessage method on the client. Also reports to the console
    /// when clients connect and disconnect.
    /// </summary>
    public class MyHub : Hub
    {

        //ez egy hub, te kliens oldalon ilyenre tudsz csatlakozni, és ezeket a methodokat tudod hívogatni
        //itt most te csak a send metódussal tudsz nekem üzenetet küldeni
        public void Send(string name, string message)
        {

            //neked lesz egy ilyen addMessage metódusod, én ezen keresztül tudok neked üzenetet küldeni
            Clients.All.addMessage(name, message);
        }
        public override Task OnConnected()
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client connected: " + Context.ConnectionId));

            return base.OnConnected();
        }
        public override Task OnDisconnected()
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client disconnected: " + Context.ConnectionId));

            return base.OnDisconnected();
        }

    }
}