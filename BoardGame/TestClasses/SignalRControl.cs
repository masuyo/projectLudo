using BoardGame.Views;
using Microsoft.AspNet.SignalR.Client;
using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.TestClasses
{
    class SignalRControl
    {
        //private static SignalRControl CONTROL;
        //private SignalRControl()
        //{
        //    HelperClass.Connection = new HubConnection((HelperClass.ConnString));
        //    HelperClass.HubProxy = (HelperClass.Connection.CreateHubProxy("WPFHub"));
        //    HelperClass.HubProxy.On<string>("SendLogin", (guid) => this.Dispatcher.Invoke(() => { Login(guid); }));
        //    HelperClass.HubProxy.On("SendLoginError", () => this.Dispatcher.Invoke(() => { LoginError(); }));

        //    HelperClass.HubProxy.On<List<IRoom>>("SendAllRoomList", (allRoom) => this.Dispatcher.Invoke(() => { AllRoom(allRoom); }));
        //    HelperClass.HubProxy.On<List<IUser>>("SendUsersInRoom", (allUserInRoom) => this.Dispatcher.Invoke(() => { AllUserInRoom(allUserInRoom); }));
        //    HelperClass.HubProxy.On<IRoom>("SendCreateRoom", (createdRoom) => this.Dispatcher.Invoke(() => { CreateRoom(createdRoom); }));
        //    HelperClass.HubProxy.On<bool>("SendConnectUserToRoom", (connectedToRoom) => this.Dispatcher.Invoke(() => { ConnectUserToRoom(connectedToRoom); }));
        //    HelperClass.HubProxy.On<IStartGameInfo>("SendStart", (startGameInfo) => this.Dispatcher.Invoke(() => { Start(startGameInfo); }));
        //}
        //public static SignalRControl GetSiganlRControl
        //{
        //    get
        //    {
        //        if (CONTROL == null)
        //        {
        //            CONTROL = new SignalRControl();
        //        }
        //        return CONTROL;
        //    }
        //}

        //private void LoginError()
        //{
        //    Dispatcher.Invoke(() => MessageBox.Show("Failed to login. Try again."));
        //    HelperClass.UserName = String.Empty;
        //    LoginView.GetVM.UserName = String.Empty;
        //    LoginView.GetVM.Password = String.Empty;
        //    //TODO :: pswd box pswd CLEAR  >>VM.Password = String.Empty; <<does not clears it 
        //    LoginView.GetVM.PassMessage = "Enter password";
        //}
        //private void Login(string guid)
        //{
        //    LoginView.GetVM.AuthenticationSuccess = true;
        //    HelperClass.GUID = guid;
        //    HelperClass.UserName = LoginView.GetVM.UserName;
        //    ConnectToGameWindow rooms = new ConnectToGameWindow();
        //    this.Close();
        //    rooms.ShowDialog();
        //}

        ////////////

        //private void Start(IStartGameInfo startGameInfo)
        //{
        //    Console.WriteLine("start");
        //    if (startGameInfo != null)
        //    {
        //        LudoWindow ludo = new LudoWindow(startGameInfo);
        //        this.Close();
        //        ludo.ShowDialog();
        //    }
        //    else
        //    {
        //        Dispatcher.Invoke(() => MessageBox.Show("Failed to start Ludo. Try again."));
        //    }
        //}

        //private void ConnectUserToRoom(bool connectedToRoom)
        //{
        //    Console.WriteLine("connect");
        //    if (connectedToRoom && HelperClass.Connection?.State == ConnectionState.Connected)
        //    {
        //        HelperClass.HubProxy.Invoke("GetUsersInRoom", HelperClass.GUID, RoomView.GetVM.SelectedRoom); //answer : call my "SendAllRoomList"
        //    }
        //    else
        //    {
        //        Dispatcher.Invoke(() => MessageBox.Show("Failed to connect. Try again."));
        //    }
        //}

        //private void CreateRoom(IRoom createdRoom)
        //{
        //    Console.WriteLine("create");
        //    if (createdRoom == null)
        //    {
        //        MessageBox.Show("Cannot create a room that already exists. Try again with a different name.");
        //    }
        //    else
        //    {
        //        RoomView.GetVM.SelectedRoom = new Room(createdRoom.Name, createdRoom.Password);
        //        RoomView.GetVM.Start = "Start Ludo";

        //        if (HelperClass.Connection?.State == ConnectionState.Connected)
        //        {
        //            HelperClass.HubProxy.Invoke("GetUsersInRoom", HelperClass.GUID, createdRoom); //answer : call my "AllUserInRoom"
        //        }
        //    }

        //}
        //private void AllUserInRoom(List<IUser> allUserInRoom)
        //{
        //    Console.WriteLine("usersinroom");
        //    RoomView.GetVM.UsersInRoom.Clear();
        //    foreach (IUser u in allUserInRoom)
        //    {
        //        RoomView.GetVM.UsersInRoom.Add(u);
        //    }
        //}

        //private void AllRoom(List<IRoom> allRoom)
        //{
        //    Console.WriteLine("sendallroom");
        //    foreach (IRoom ir in allRoom)
        //    {
        //        Console.WriteLine(ir.Name + " - " + ir.Password + " " + ir.AvailablePlaces);
        //    }

        //    RoomView.GetVM.RoomList.Clear();
        //    if (allRoom != null && allRoom.Count > 0)
        //    {
        //        foreach (IRoom r in allRoom)
        //        {
        //            Console.WriteLine(r.Name);
        //            RoomView.GetVM.RoomList.Add(r);
        //        }
        //    }
        //}
        
        



    }
}
