using BoardGame.Interfaces.Login;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoardGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum GameType { LUDO, CHESS, TICTACTOE }
        public MainWindow()
        {
            InitializeComponent();
            //ImageBrush imgb = new ImageBrush();
            //imgb.ImageSource = new BitmapImage(new Uri(@"Images\bg3.jpg", UriKind.Relative));
            //this.Background = imgb;
            ImageBrush imgb = new ImageBrush();
            imgb.ImageSource = new BitmapImage(new Uri(@"Images\l2.png", UriKind.Relative));
            this.Background = imgb;

            lsb_Enum.ItemsSource = Enum.GetNames(typeof(GameType)).ToList();
            lsb_Enum.SelectedItem = GameType.LUDO.ToString();
                 

        }

      

        private void Login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Logging in...");
            if (sender is Label)
            {
                Label sndr = (sender as Label);
                sndr.IsEnabled = false;
                sndr.Background = Brushes.Green;
                sndr.Content = String.Format("Logging in as {0}.", uname);
                ///TODO : check uname and pswd ...
                ///TODO : send pswd and uname
                ///

                ConnectToGameWindow rooms = new ConnectToGameWindow(uname);
                rooms.ShowDialog();
            }
        }
        private void Txb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).Text = "";
            }
            if (sender is PasswordBox)
            {
                lbl_hdn.Visibility = Visibility.Hidden;
            }
        }
        private void ForgotPswd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Forgot");
        }

        string pass = "";
        private void Pswd_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is PasswordBox && (sender as PasswordBox).Password.ToString() != String.Empty)
            {
                lbl_hdn.Visibility = Visibility.Hidden;
                pass = (sender as PasswordBox).Password.ToString();
            }
        }
        string uname = "";
        private void Uname_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBox && (sender as TextBox).Text != String.Empty)
            {
                uname = (sender as TextBox).Text;
            }
        }
        private void LblPswd_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Label && pass == String.Empty)
            {
                (sender as Label).Visibility = Visibility.Visible;
            }
        }

        private void LblExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
