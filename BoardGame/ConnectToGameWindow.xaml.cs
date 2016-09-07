using BoardGame.Interfaces;
using BoardGame.TestClasses;
using BoardGame.Views;
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
        public ConnectToGameWindow(string username)
        {
            InitializeComponent();
            VM = RoomView.GetVM;
            this.DataContext = VM;
            VM.UserName = username;

            ImageBrush imgb = new ImageBrush();
            imgb.ImageSource = new BitmapImage(new Uri(@"Images\l3.png", UriKind.Relative));
            imgb.Opacity = 0.4;
            grid_bg.Background = imgb;

            Init(AddListItems());
        }

        private List<IRoom> AddListItems()
        {
            TestLudoServer TLS = new TestLudoServer();
            return TLS.GetAllRoomList();
        }
        private void Init(List<IRoom> list)
        {
            foreach (IRoom r in list)
            {
                VM.RoomList.Add(r);
            }

            VM.SelectedRoom = VM.RoomList[0];
        }

        private void LBL_Start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                if (!String.IsNullOrEmpty(VM.SelectedRoom.Name) && VM.SelectedRoom.AvailablePlaces == 0)
                {
                    
                    if (true)//connD.ConectionSuccess)
                    {
                        LudoWindow ludo = new LudoWindow(VM.UserName);
                        this.Close();
                        ludo.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Failed to connect.");
                    }
                }
                else
                {
                    MessageBox.Show("Add player to start Ludo.");
                }
            }
        }

        private void TXB_Search_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VM.SearchKeyWord = String.Empty;
        }

        private void TXB_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrEmpty(VM.SearchKeyWord))
                {
                    VM.SearchRoomList.Clear();
                    foreach (TestRoom r in VM.RoomList)
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
                        foreach (TestRoom r in q)
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
            MessageBox.Show(VM.SelectedRoom + "" + VM.SelectedRoomPassword);
        }
        private void LBL_Connect_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            if (true)//connD.ConectionSuccess)
            {
                Init(AddListItems()); // serversideListChanged

            }
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
    }
}
