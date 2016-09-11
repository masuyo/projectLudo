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

            HelperClass.HubProxy.On<List<IRoom>>("SendAllRoomList", (allRoom) => this.Dispatcher.Invoke(() => { AllRoom(allRoom); }));
            HelperClass.HubProxy.On<List<IUser>>("SendUsersInRoom", (allUserInRoom) => this.Dispatcher.Invoke(() => { AllUserInRoom(allUserInRoom); }));
            HelperClass.HubProxy.On<IRoom>("SendCreateRoom", (createdRoom) => this.Dispatcher.Invoke(() => { SendCreateRoom(createdRoom); }));
            HelperClass.HubProxy.On<bool>("SendConnectUserToRoom", (connectedToRoom) => this.Dispatcher.Invoke(() => { SendConnectUserToRoom(connectedToRoom); }));




            HelperClass.Connection.StateChanged += (e) => { if (e.NewState != ConnectionState.Connected) { MessageBox.Show(e.OldState.ToString() + " >> " + e.NewState.ToString()); } };


            if (HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetAllRoomList", HelperClass.GUID); //answer : call my "SendAllRoomList"
            }
        }

        private void SendConnectUserToRoom(bool connectedToRoom)
        {
            if (connectedToRoom)
            {
                VM.UsersInRoom.Add(new User(HelperClass.UserName));
            }
        }

        private void SendCreateRoom(IRoom createdRoom)
        {
            if (HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetAllRoomList", HelperClass.GUID); //answer : call my "SendAllRoomList"
            }
            VM.SearchRoomList.Clear();
            foreach (Room r in VM.RoomList)
            {
                if (r.AvailablePlaces > 0)
                {
                    VM.SearchRoomList.Add(r);
                }
            }
        }

        private void AllUserInRoom(List<IUser> allUserInRoom)
        {
            foreach (IUser u in allUserInRoom)
            {
                VM.UsersInRoom.Add(u);
            }
        }
        private void AllRoom(List<IRoom> allRoom)
        {
            if (allRoom != null && allRoom.Count > 0)
            {
                foreach (IRoom r in allRoom)
                {
                    VM.RoomList.Add(r);
                }
            }
        }
              
        private void LBL_Start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                if (!String.IsNullOrEmpty(VM.SelectedRoom.Name) && VM.SelectedRoom.AvailablePlaces == 0)
                {

                    if (true)//connD.ConectionSuccess)
                    {
                        LudoWindow ludo = new LudoWindow();
                        this.Close();
                        ludo.ShowDialog();
                    }
                    //else
                    //{
                    //    MessageBox.Show("Failed to connect.");
                    //}
                }
                else
                {
                    MessageBox.Show("Add player to start Ludo.");
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
            //if (VM.RoomList.Contains(VM.SelectedRoom))
            //{

            //}
            if (HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetConnectUserToRoom", HelperClass.GUID, new User(HelperClass.UserName), new Room(VM.SelectedRoom.AvailablePlaces, VM.SelectedRoom.ID, VM.SelectedRoom.Name, VM.SelectedRoom.Password));
            }
            VM.Start = "Start Ludo";

            MessageBox.Show(VM.SelectedRoom + "" + VM.SelectedRoomPassword);
        }
        private void LBL_Connect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (HelperClass.Connection?.State == ConnectionState.Connected)
            {
                HelperClass.HubProxy.Invoke("GetConnectUserToRoom", HelperClass.GUID, new User(HelperClass.UserName), new Room(VM.SelectedRoom.AvailablePlaces, VM.SelectedRoom.ID, VM.SelectedRoom.Name, VM.SelectedRoom.Password));
            }
            foreach (IUser u in new TestLudoServer().GetPlayersInRoom((VM.SelectedRoom)))
            {
                //VM.UsersInRoom = new ObservableCollection<IUser>();
                VM.UsersInRoom.Add(u);
            }
            if (true)//connD.ConectionSuccess)
            {
                //Init(AddListItems()); // serversideListChanged
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
    }
}
