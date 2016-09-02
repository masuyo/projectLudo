using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BoardGame
{
    /// <summary>
    /// Interaction logic for LudoWindow.xaml
    /// </summary>
    public partial class LudoWindow : Window
    {        
        ViewModel VM;

        public String UserName { get; set; }

        //ezen keresztül fogod elérni a hubomat, amit a szerver oldalon osztályt csináltam, lásd lentebb
        public IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://localhost:8080/signalr";
        public HubConnection Connection { get; set; }


        public LudoWindow(string username)
        {
            InitializeComponent();
            UserName = username;
            VM = ViewModel.GetVM;
            this.DataContext = VM;

            //connect to server with ConnectAsync(); display what's happening
            VM.ServerMsgs.Add(new ChatMsg(UserName, "Connecting to server..."));            
            ConnectAsync();
        }
        private void ButtonSend_Click(object sender, RoutedEventArgs e) //server method name : SendChatClientText, params: string name, string sentBy
        {
            ///WPF client defines method call on server side
            HubProxy.Invoke("SendChatClientText", VM.ChatMsg, UserName);//VM.ChatMsgs.Add(new ChatMsg(VM.ChatMsg, UserName)
            VM.ChatMsg = String.Empty;
        }
        private void TestMethod(string name, string msg)
        {
            VM.ChatMsgs.Add(new ChatMsg(msg, UserName));
            Console.WriteLine("test");
        }
        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI);
            Connection.Closed += Connection_Closed;

            HubProxy = Connection.CreateHubProxy("MyHub");

            HubProxy.On<string, string>("addMessage", (name, message) => TestMethod(name, message));
            Console.WriteLine("hubtest");
            // WPF client defines its method
            //HubProxy.On<string, string>("addMessage", (name, message) =>
            //    this.Dispatcher.Invoke(() =>
            //        RichTextBoxConsole.AppendText(String.Format("{0}: {1}\r", name, message))
            //    )
            //);
            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException)
            {
                VM.ServerMsgs.Add(new ChatMsg("server", "Unable to connect: Start server before connecting clients."));
                return;
            }
            VM.ServerMsgs.Add(new ChatMsg("server", "Connected to server at " + ServerURI + "\r"));
        }
        
        void Connection_Closed()
        {
            //Hide chat UI; show login UI
            //var dispatcher = Application.Current.Dispatcher;
            //dispatcher.Invoke(() => StatusText.Content = "You have been disconnected.");
            //VM.ServerMsgs.Add(new ChatMsg("server:" + ServerURI + "\r", "You have been disconnected. "));
        }

        private void WPFClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }
        
        private void TXB_Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VM.ChatMsgs.Add(new ChatMsg(VM.ChatMsg, "sentby"));
            }
        }
    }
}
