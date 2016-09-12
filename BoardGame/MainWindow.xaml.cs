using BoardGame.TestClasses;
using BoardGame.Views;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
        LoginView VM;
        public MainWindow()
        {
            InitializeComponent();
            VM = LoginView.GetVM;
            if (HelperClass.Connection == null)
            {
                HelperClass.Connection = new HubConnection((HelperClass.ConnString));
                HelperClass.HubProxy = (HelperClass.Connection.CreateHubProxy("WPFHub"));
                HelperClass.HubProxy.On<string>("SendLogin", (guid) => this.Dispatcher.Invoke(() => { Login(guid); }));
                HelperClass.HubProxy.On("SendLoginError", () => this.Dispatcher.Invoke(() => { LoginError(); }));
                try
                {
                    HelperClass.Connection.Start();
                }
                catch (HttpClientException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            this.DataContext = VM;
            HelperClass.Connection.StateChanged += (e) => { if (e.NewState != ConnectionState.Connected) { MessageBox.Show(e.OldState.ToString() + " >> " + e.NewState.ToString()); } };
            // this.Background = LoginView.GetBG;
            
        }

        private void LoginError()
        {
            Dispatcher.Invoke(() => MessageBox.Show("Failed to login. Try again."));
            HelperClass.UserName = String.Empty;
            VM.UserName = String.Empty;
            VM.Password = String.Empty;
            //TODO :: pswd box pswd CLEAR  >>VM.Password = String.Empty; <<does not clears it 
            VM.PassMessage = "Enter password";
        }
        private void Login(string guid)
        {
            VM.AuthenticationSuccess = true;
            HelperClass.GUID = guid;
            HelperClass.UserName = VM.UserName;
            ConnectToGameWindow rooms = new ConnectToGameWindow();
            this.Close();
            rooms.ShowDialog();
        }

        private void Login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                if (!String.IsNullOrEmpty(VM.UserName) && VM.UserName.Length > 5 &&
                    !String.IsNullOrEmpty(VM.Password) && VM.Password.Length > 5)
                {
                    var sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(VM.Password));
                    string hashedPassword = new ASCIIEncoding().GetString(sha1data);

                    if (HelperClass.Connection?.State == ConnectionState.Connected)
                    {
                        HelperClass.HubProxy.Invoke("GetLogin", VM.UserName, hashedPassword, VM.SelectedGameType);
                    }
                }
                else
                {
                    MessageBox.Show("Username and password must contain at least 6 characters. ");
                    (sender as Label).Background = Brushes.Green;
                    (sender as Label).Content = "Login";
                }
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
                VM.PassMessage = String.Empty;
            }
        }
        private void Uname_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBox && !String.IsNullOrEmpty((sender as TextBox).Text))
            {
                VM.UserName = (sender as TextBox).Text;
            }
        }

        private void TXB_UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                VM.PassMessage = String.Empty;
            }
        }
        private void PSWD_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox && !String.IsNullOrEmpty((sender as PasswordBox).Password))
            {
                VM.PassMessage = String.Empty;
                VM.Password = (sender as PasswordBox).Password;
            }
            else if (sender is PasswordBox)
            {
                VM.PassMessage = "Enter password";
            }
        }
        private void ForgotPswd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //TODO
            Console.WriteLine("Forgot");
        }
        private void LblExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Login_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                (sender as Label).Background = Brushes.LightYellow;
                (sender as Label).Content = "Logging in";
            }
        }
    }
}
