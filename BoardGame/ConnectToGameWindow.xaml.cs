using BoardGame.TestClasses;
using BoardGame.Views;
using Microsoft.AspNet.SignalR.Client;
using SharedLudoLibrary.ClientClasses;
using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for ConnectToGameWindow.xaml
    /// </summary>
    public partial class ConnectToGameWindow : Window
    {
        RoomView VM;
        public ConnectToGameWindow()
        {
            InitializeComponent();
            VM = RoomView.GetVM;
            this.DataContext = VM;
            // VM.UserName = HelperClass.UserName;

            ImageBrush imgb = new ImageBrush();
            imgb.ImageSource = new BitmapImage(new Uri(@"Images\l3.png", UriKind.Relative));
            imgb.Opacity = 0.4;
            // grid_bg.Background = imgb;

            HelperClass.HubProxy.On<List<Room>>("SendAllRoomList", (allRoom) => this.Dispatcher.Invoke(() => { AllRoom(allRoom); }));
            HelperClass.HubProxy.On<List<User>>("SendUsersInRoom", (allUserInRoom) => this.Dispatcher.Invoke(() => { AllUserInRoom(allUserInRoom); }));
            HelperClass.HubProxy.On<Room>("SendCreateRoom", (createdRoom) => this.Dispatcher.Invoke(() => { CreateRoom(createdRoom); }));
            HelperClass.HubProxy.On<bool>("SendConnectUserToRoom", (connectedToRoom) => this.Dispatcher.Invoke(() => { ConnectUserToRoom(connectedToRoom); }));
            HelperClass.HubProxy.On<StartGameInfo>("SendStart", (startGameInfo) => this.Dispatcher.Invoke(() => { Start(startGameInfo); }));


            AllRoom(new List<Room>());


            HelperClass.Connection.StateChanged += Connection_StateChanged;

            if (HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetAllRoomList", HelperClass.GUID); //answer : call my "SendAllRoomList"
            }
        }

        private void Connection_StateChanged(StateChange e)
        {
            if (e.NewState != ConnectionState.Connected) { MessageBox.Show(e.OldState.ToString() + " >> " + e.NewState.ToString()); }
        }

        private void Start(StartGameInfo startGameInfo)
        {
            Console.WriteLine("start");
            if (startGameInfo != null)
            {
                LudoWindow ludo = new LudoWindow(startGameInfo);
                Hide();
                ShowInTaskbar = false;

                ludo.Show();
            }
            else
            {
                Dispatcher.Invoke(() => MessageBox.Show("Failed to start Ludo. Try again."));
            }
        }

        private void ConnectUserToRoom(bool connectedToRoom)
        {
            Console.WriteLine("connect");
            if (connectedToRoom && HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetUsersInRoom", HelperClass.GUID, VM.SelectedRoom); //answer : call my "SendAllRoomList"
            }
            else
            {
                Dispatcher.Invoke(() => MessageBox.Show("Failed to connect. Try again."));
            }
        }

        private void CreateRoom(Room createdRoom)
        {
            Console.WriteLine("create");
            if (createdRoom == null)
            {
                MessageBox.Show("Cannot create a room that already exists. Try again with a different name.");
            }
            else
            {
                VM.SelectedRoom = new Room(createdRoom.Name, createdRoom.Password);
                VM.Start = "Start Ludo";

                if (HelperClass.Connection?.State == ConnectionState.Connected)
                {
                    HelperClass.HubProxy.Invoke("GetUsersInRoom", HelperClass.GUID, createdRoom); //answer : call my "AllUserInRoom"
                }
            }

        }
        private void AllUserInRoom(List<User> allUserInRoom)
        {
            Console.WriteLine("usersinroom");
            VM.UsersInRoom.Clear();
            foreach (User u in allUserInRoom)
            {
                VM.UsersInRoom.Add(u);
            }
        }

        private void AllRoom(List<Room> allRoom)
        {
            Console.WriteLine("sendallroom");
            foreach (Room ir in allRoom)
            {
                Console.WriteLine(ir.Name + " - " + ir.Password + " " + ir.AvailablePlaces);
            }

            VM.RoomList.Clear();
            if (allRoom != null && allRoom.Count > 0)
            {
                foreach (Room r in allRoom)
                {
                    
                    Console.WriteLine(r.Name);
                    VM.RoomList.Add(r);
                }
            }

            //kamu
            for (int i = 0; i < 10; i++)
            {
                VM.SearchRoomList.Add(new Room("jfshfdsdhio", (i * 1231).ToString()));
            }

        }

        private void LBL_Start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                if (HelperClass.Connection?.State == ConnectionState.Connected)
                {
                    HelperClass.HubProxy.Invoke("GetStart", HelperClass.GUID, HelperClass.UserName); //answer : call my "SendStart(IStartGameInfo startGameInfo);"
                }
            }
        }


        private void TXB_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrEmpty(VM.SearchKeyWord))
                {
                    VM.SearchRoomList.Clear();
                    foreach (Room r in VM.RoomList)
                    {
                        if (r.AvailablePlaces > 0)
                        {
                            VM.SearchRoomList.Add(r);
                        }
                    }
                }
                else
                {
                    var q = from akt in VM.RoomList
                            where akt.Name.ToLower().Contains(VM.SearchKeyWord.ToLower())
                            select akt;
                    if (q.ToList() != null && q.ToList().Count > 0)
                    {
                        VM.SearchRoomList.Clear();
                        foreach (Room r in q)
                        {
                            if (r.AvailablePlaces > 0)
                            {
                                VM.SearchRoomList.Add(r);
                            }
                        }
                    }
                    else
                    {
                        VM.SearchRoomList.Clear();
                    }
                }
            }
        }

        private void LBL_New_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetCreateRoom", HelperClass.GUID, new Room(VM.SelectedRoomName, VM.SelectedRoomPassword)); //answer : call my "SendAllRoomList"
            }
        }
        private void LBL_Connect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetConnectUserToRoom", HelperClass.GUID, new User(HelperClass.UserName), new Room(VM.SelectedRoom.AvailablePlaces, VM.SelectedRoom.ID, VM.SelectedRoom.Name, VM.SelectedRoom.Password));
            }            
        }
        private void TXB_Search_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetAllRoomList", HelperClass.GUID);
            }
            VM.SearchKeyWord = String.Empty;
        }
        private void LBL_Hover_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Label)
            {
                //FontFamily="Snap ITC"
                (sender as Label).FontFamily = new FontFamily("Perpetua Titling MT");
            }
        }
        private void LBL_Hover_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Label)
            {
                (sender as Label).FontFamily = new FontFamily("Segoe UI");
            }
        }

        private void LBL_ExitRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VM.UsersInRoom.Clear();
            VM.UsersInRoom = new ObservableCollection<IUser>();
        }

        private void PSWDBX_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox)
            {
                VM.SelectedRoomPassword = (sender as PasswordBox).Password;
            }
        }
    }
}
