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
    public partial class MainWindow : Window
    {
        public IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:8080";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            WriteToConsole("Starting server...");
            ButtonStart.IsEnabled = false;
            Task.Run(() => StartServer());
        }
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            SignalR.Dispose();
            Close();
        }
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
    class Startup
    {
        //itt történik hogy összekapcsolja a hubokat, neked nem kell 
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    public class MyHub : Hub
    {
        //ez egy hub, te kliens oldalon ilyenre tudsz csatlakozni, és ezeket a methodokat tudod hívogatni     
        public void Send(string message)
        {
            //neked lesz egy ilyen addMessage metódusod, én ezen keresztül tudok neked üzenetet küldeni
            Console.WriteLine(message);
            Clients.All.addMessage(message);
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

    }
}

