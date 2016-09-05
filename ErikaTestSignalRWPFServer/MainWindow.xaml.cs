using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ErikaTestSignalRWPFServer
{
    /// <summary>
    /// WPF host for a SignalR server. The host can stop and start the SignalR
    /// server, report errors when trying to start the server on a URI where a
    /// server is already being hosted, and monitor when clients connect and disconnect. 
    /// The hub used in this server is a simple echo service, and has the same 
    /// functionality as the other hubs in the SignalR Getting Started tutorials.
    /// For simplicity, MVVM will not be used for this sample.
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        public IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:8080";

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calls the StartServer method with Task.Run to not
        /// block the UI thread. 
        /// </summary>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            WriteToConsole("Starting server...");
            ButtonStart.IsEnabled = false;
            Task.Run(() => StartServer());
        }

        /// <summary>
        /// Stops the server and closes the form. Restart functionality omitted
        /// for clarity.
        /// </summary>
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

        //public void Send(IChatMsg message)
        //{

        //    //neked lesz egy ilyen addMessage metódusod, én ezen keresztül tudok neked üzenetet küldeni
        //    Console.WriteLine(message.Name); Clients.All.addMessage(message.Name, message.SentBy);
        //}
        public void Send(string message)
        {

            //neked lesz egy ilyen addMessage metódusod, én ezen keresztül tudok neked üzenetet küldeni
            Console.WriteLine(message);
            Clients.All.addMessage(message, "");//Clients.All.addMessage(message);
        }

        public override Task OnConnected()
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client connected: " + Context.ConnectionId));

            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client disconnected: " + Context.ConnectionId));

            return base.OnDisconnected(stopCalled);
        }
        //public override Task OnDisconnected()
        //{
        //    //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
        //    Application.Current.Dispatcher.Invoke(() =>
        //        ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client disconnected: " + Context.ConnectionId));

        //    return base.OnDisconnected();
        //}

    }
}

