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
using System.Windows.Threading;

namespace BoardGame
{
    /// <summary>
    /// Interaction logic for LudoWindow.xaml
    /// </summary>
    public partial class LudoWindow : Window
    {
        LudoView VM;
        DispatcherTimer dt;
        List<IPlayer> players;
        private void Init(IStartGameInfo startGameInfo)
        {
            //this.Dispatcher.Invoke(() => Ludo.IsEnabled = startGameInfo.WPFPlayer.ID == startGameInfo.MsgFromServer.ActivePlayerID);

            //HelperClass.HubProxy.Invoke("GetMessage", HelperClass.GUID, HelperClass.UserName, startGameInfo.WPFPlayer.ID == startGameInfo.MsgFromServer.ActivePlayerID);

            Ludo.Init(startGameInfo);

            VM.WPFPlayer = new Player(startGameInfo.WPFPlayer.ID, startGameInfo.WPFPlayer.Name, startGameInfo.WPFPlayer.Color);
            players = new List<IPlayer>();
            foreach (IPlayer p in startGameInfo.OtherWPFPlayers)
            {
                players.Add(p); 
            }
            players.Add(startGameInfo.WPFPlayer);

            VM.UserName = players.Where(p => p.ID == startGameInfo.MsgFromServer.ActivePlayerID).First().Name;
            VM.ActiveColor = players.Where(p => p.ID == startGameInfo.MsgFromServer.ActivePlayerID).First().Color;
            VM.OtherWPFPlayers = new Player[] {
                new Player(startGameInfo.OtherWPFPlayers[0].ID, startGameInfo.OtherWPFPlayers[0].Name, startGameInfo.OtherWPFPlayers[0].Color),
                new Player(startGameInfo.OtherWPFPlayers[1].ID, startGameInfo.OtherWPFPlayers[1].Name, startGameInfo.OtherWPFPlayers[1].Color),
                new Player(startGameInfo.OtherWPFPlayers[2].ID, startGameInfo.OtherWPFPlayers[2].Name, startGameInfo.OtherWPFPlayers[2].Color)
            };
            VM.GameSateInfo = startGameInfo.MsgFromServer;
        }
        public LudoWindow(IStartGameInfo startGameInfo)
        {
            InitializeComponent();
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(100);
            dt.Tick += Dt_Tick;


            VM = LudoView.GetVM;
            Init(startGameInfo);
            this.DataContext = VM;

            Ludo.PuppetMove += Ludo_PuppetMove;

            HelperClass.HubProxy.On<string, string, DateTime>("SendMessage", (uname, text, time) => this.Dispatcher.Invoke(() => { SendMessage(uname, text, time); }));
            HelperClass.HubProxy.On<GameInfo>("SendMove", (gameinfo) => this.Dispatcher.Invoke(() => { SendMove(gameinfo); }));
            HelperClass.HubProxy.On<string>("SendOverall", (linkToPage) => this.Dispatcher.Invoke(() => { SendOverall(linkToPage); }));
            HelperClass.HubProxy.On<bool>("SendDice", (roll) => this.Dispatcher.Invoke(() => { Dice(roll); }));


            this.Dispatcher.Invoke(() => VM.ServerMsgs.Add(VM.WPFPlayer.Name + ":: Connecting to server..."));
            this.Dispatcher.Invoke(() => VM.ServerMsgs.Add("Connected to server."));
            if (!String.IsNullOrEmpty(VM.GameSateInfo.Msg)) { VM.ServerMsgs.Add(startGameInfo.MsgFromServer.Msg); }


            HelperClass.Connection.Closed += Connection_Closed;
            HelperClass.Connection.StateChanged += Connection_StateChanged;

        }
        int time = 0;
        private void RotateDice1()
        {
            if (VM.GameSateInfo.Dice1 == 1)
            {
                rotateX.Angle = 270; rotateY.Angle = 90; rotateZ.Angle = 0;//1
            }
            else if (VM.GameSateInfo.Dice1 == 2)
            {
                rotateX.Angle = 90; rotateY.Angle = 0; rotateZ.Angle = 90;//2
            }
            else if (VM.GameSateInfo.Dice1 == 3)
            {
                rotateX.Angle = 180; rotateY.Angle = 0; rotateZ.Angle = 90;//3
            }
            else if (VM.GameSateInfo.Dice1 == 4)
            {
                rotateX.Angle = 270; rotateY.Angle = 270; rotateZ.Angle = 90;//4
            }
            else if (VM.GameSateInfo.Dice1 == 5)
            {
                rotateX.Angle = 270; rotateY.Angle = 0; rotateZ.Angle = 90;//5
            }
            else
            {
                rotateX.Angle = 0; rotateY.Angle = 0; rotateZ.Angle = 0;//6
            }
        }
        private void RotateDice2()
        {
            if (VM.GameSateInfo.Dice1 == 2)
            {
                rotate2X.Angle = 270; rotate2Y.Angle = 90; rotate2Z.Angle = 0;//1
            }
            else if (VM.GameSateInfo.Dice2 == 2)
            {
                rotate2X.Angle = 90; rotate2Y.Angle = 0; rotate2Z.Angle = 90;//2
            }
            else if (VM.GameSateInfo.Dice2 == 3)
            {
                rotate2X.Angle = 180; rotate2Y.Angle = 0; rotate2Z.Angle = 90;//3
            }
            else if (VM.GameSateInfo.Dice2 == 4)
            {
                rotate2X.Angle = 270; rotate2Y.Angle = 270; rotate2Z.Angle = 90;//4
            }
            else if (VM.GameSateInfo.Dice2 == 5)
            {
                rotate2X.Angle = 270; rotate2Y.Angle = 0; rotate2Z.Angle = 90;//5
            }
            else
            {
                rotate2X.Angle = 0; rotate2Y.Angle = 0; rotate2Z.Angle = 0;//6
            }
        }
        private void Dt_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() => rotateX.Angle = new Random().Next(360));
            this.Dispatcher.Invoke(() => rotateY.Angle = new Random().Next(360));
            this.Dispatcher.Invoke(() => rotateZ.Angle = new Random().Next(360));
            this.Dispatcher.Invoke(() => rotate2X.Angle = new Random().Next(360));
            this.Dispatcher.Invoke(() => rotate2Y.Angle = new Random().Next(360));
            this.Dispatcher.Invoke(() => rotate2Z.Angle = new Random().Next(360));
            time++;

            if (time > 30)
            {
                dt.Stop();
                RotateDice1(); RotateDice2();
                time = 0;
                //Ludo.IsEnabled = true;
            }
        }

        private void Dice(bool roll)
        {
            if (roll)
            {
                dt.Start();
            }
        }

        private void Ludo_PuppetMove(int from, int to, int puppetID)
        {
            HelperClass.HubProxy.Invoke("GetMove", HelperClass.GUID, puppetID, from, to);

            //HelperClass.HubProxy.Invoke("GetMessage", HelperClass.GUID, HelperClass.UserName, VM.GameSateInfo.ActivePlayerID + "::");
            HelperClass.HubProxy.Invoke("GetMessage", HelperClass.GUID, HelperClass.UserName, puppetID +"::"+ from + ">> " + to);
            //Console.WriteLine(from + ">> " + to);
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
            //this.Dispatcher.Invoke(() => Ludo.IsEnabled = gameinfo.ActivePlayerID == VM.WPFPlayer.ID);
            //HelperClass.HubProxy.Invoke("GetMessage", HelperClass.GUID, HelperClass.UserName, gameinfo.ActivePlayerID == VM.WPFPlayer.ID);
            //HelperClass.HubProxy.Invoke("GetMessage", HelperClass.GUID, HelperClass.UserName, VM.GameSateInfo.PuppetList.Where(p =>p.Player.Color == VM.WPFPlayer.Color).ToString());
            HelperClass.HubProxy.Invoke("GetMessage", HelperClass.GUID, HelperClass.UserName, VM.GameSateInfo.ActivePlayerID + "**");

            Dispatcher.Invoke(() => VM.UserName = players.Where(p => p.ID == gameinfo.ActivePlayerID).First().Name);
            Dispatcher.Invoke(() => VM.ActiveColor = players.Where(p => p.ID == gameinfo.ActivePlayerID).First().Color);

            Dispatcher.Invoke(() => VM.GameSateInfo.Dice1 = gameinfo.Dice1);
            Dispatcher.Invoke(() => VM.GameSateInfo.Dice2 = gameinfo.Dice2);
            Dispatcher.Invoke(() => VM.GameSateInfo.PuppetList = gameinfo.PuppetList);
            
            if (!String.IsNullOrEmpty(gameinfo.Msg) && (gameinfo.Msg.ToLower().Contains("server"))) { VM.ServerMsgs.Add(gameinfo.Msg); }
            if (gameinfo.OnManHit && !String.IsNullOrEmpty(gameinfo.Msg))
            {
                HelperClass.HubProxy.Invoke("GetMessage", HelperClass.GUID, String.Empty, gameinfo.Msg);
            }
            Dispatcher.Invoke(() => Ludo.MovePuppets(gameinfo.PuppetList));
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

        private void Dice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (VM.WPFPlayer.ID == VM.GameSateInfo.ActivePlayerID)
            //{
                HelperClass.HubProxy.Invoke("GetDice", HelperClass.GUID);
                //Ludo.IsEnabled = false;
            //}
        }
    }
}
