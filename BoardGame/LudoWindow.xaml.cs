using BoardGame.TestClasses;
using BoardGame.Views;
using Microsoft.AspNet.SignalR.Client;
using SharedLudoLibrary.ClientClasses;
using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        LudoView VM;

        public LudoWindow(IStartGameInfo startGameInfo)
        {
            InitializeComponent();
            VM = LudoView.GetVM;

            VM.WPFPlayer = new Player(startGameInfo.WPFPlayer.ID, startGameInfo.WPFPlayer.Color);
            VM.UserName = startGameInfo.WPFPlayer.Name;
            VM.OtherWPFPlayers = new Player[] {
                new Player(startGameInfo.OtherWPFPlayers[0].ID, startGameInfo.OtherWPFPlayers[0].Color),
                new Player(startGameInfo.OtherWPFPlayers[1].ID, startGameInfo.OtherWPFPlayers[1].Color),
                new Player(startGameInfo.OtherWPFPlayers[2].ID, startGameInfo.OtherWPFPlayers[2].Color)
            };
            VM.MsgFromServer = startGameInfo.MsgFromServer; //startGameInfo.MsgFromServer

            this.DataContext = VM;

            Ludo.PuppetMove += Ludo_PuppetMove;

            HelperClass.HubProxy.On<string, string, DateTime>("SendMessage", (uname, text, time) => this.Dispatcher.Invoke(() => { SendMessage(uname, text, time); }));
            HelperClass.HubProxy.On<GameInfo>("SendMove", (gameinfo) => this.Dispatcher.Invoke(() => { SendMove(gameinfo); }));
            HelperClass.HubProxy.On<string>("SendOverall", (linkToPage) => this.Dispatcher.Invoke(() => { SendOverall(linkToPage); }));



            this.Dispatcher.Invoke(() => VM.ServerMsgs.Add("Connected to server."));
            this.Dispatcher.Invoke(() => VM.ServerMsgs.Add(VM.UserName + ":: Connecting to server..."));
            if (!String.IsNullOrEmpty(VM.MsgFromServer.Msg)) { VM.ServerMsgs.Add(startGameInfo.MsgFromServer.Msg); }


            HelperClass.Connection.Closed += Connection_Closed;
            HelperClass.Connection.StateChanged += Connection_StateChanged;

        }

        private void Ludo_PuppetMove(int from, int to)
        {
            HelperClass.HubProxy.Invoke("GetMove", HelperClass.GUID, VM.MsgFromServer.ActivePlayerID, from, to);
        }

        private void Connection_StateChanged(StateChange e)
        {
            if (e.NewState != ConnectionState.Connected) { MessageBox.Show(e.OldState.ToString() + " >> " + e.NewState.ToString()); }
        }

        private void SendOverall(string linkToPage)
        {
            if (!String.IsNullOrEmpty(linkToPage))
            {
                Process.Start(linkToPage);
                Environment.Exit(0); //other hidden windows ? 
            }
        }

        private void SendMove(GameInfo gameinfo)
        {
            Ludo.MovePuppets(gameinfo.PuppetList);
        }

        private void SendMessage(string uname, string text, DateTime date)
        {
            Dispatcher.Invoke(() => VM.ChatMsgs.Add(date.ToShortDateString() + " - " + date.ToShortTimeString() + ":\n" + uname + ": " + text));
        }

        private void TXB_Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                HelperClass.HubProxy.Invoke("GetMessage", HelperClass.GUID, HelperClass.UserName, VM.ChatMsg); // call my senmessage()
                VM.ChatMsg = String.Empty;
            }
        }

        private void LBL_Friend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HelperClass.HubProxy.Invoke("Befriend", HelperClass.GUID, HelperClass.UserName, VM.UserName);
            Dispatcher.Invoke(() => MessageBox.Show("Added"));
        }

        void Connection_Closed()
        {
            this.Dispatcher.Invoke(() =>
            VM.ServerMsgs.Add("Disconnected from server.")
            );
        }
        private void WPFClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (HelperClass.Connection != null)
            {
                HelperClass.Connection.Stop();
                HelperClass.Connection.Dispose();
            }
        }

    }
}
