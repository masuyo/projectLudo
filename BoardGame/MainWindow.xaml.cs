using BoardGame.TestClasses;
using BoardGame.Views;
using Microsoft.AspNet.SignalR.Client;
using SharedLudoLibrary.ClientClasses;
using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                HelperClass.Connection.TraceLevel = TraceLevels.All;
                HelperClass.Connection.TraceWriter = Console.Out;

                HelperClass.HubProxy = (HelperClass.Connection.CreateHubProxy("WPFHub"));
                HelperClass.HubProxy.On<string>("SendLogin", (guid) => this.Dispatcher.Invoke(() => { Login(guid); }));
                HelperClass.HubProxy.On("SendLoginError", () => this.Dispatcher.Invoke(() => { LoginError(); }));
                HelperClass.HubProxy.On<string>("SendForgot", (linkToPage) => this.Dispatcher.Invoke(() => { Forgot(linkToPage); }));

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
            HelperClass.Connection.StateChanged += Connection_StateChanged;
            // this.Background = LoginView.GetBG;

        }

        private void Forgot(string linkToPage)
        {
            if (!String.IsNullOrEmpty(linkToPage))
            {
                Process.Start(linkToPage);
                Environment.Exit(0);
            }
        }

        private void Connection_StateChanged(StateChange e)
        {
            if (e.NewState != ConnectionState.Connected) { MessageBox.Show(e.OldState.ToString() + " >> " + e.NewState.ToString()); }
        }

        private void LoginError()
        {
            VM.AuthenticationSuccess = false;
            Dispatcher.Invoke(() => MessageBox.Show("Failed to login. Try again."));
            HelperClass.UserName = String.Empty;
            VM.UserName = String.Empty;
            VM.Password = String.Empty;
            pswb_bx.Clear();
            VM.PassMessage = "Enter password";
        }
        private void Login(string guid)
        {
            VM.AuthenticationSuccess = true;
            HelperClass.GUID = guid;
            HelperClass.UserName = VM.UserName;
            ConnectToGameWindow rooms = new ConnectToGameWindow();
            Hide();
            ShowInTaskbar = false;

            rooms.Show();
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
            HelperClass.HubProxy.Invoke("GetForgot");
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
                VM.AuthenticationSuccess = true;
            }
        }

        private void pswb_bx_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Enter)
            {
                Login_MouseDown(new Label(), null);
            }
        }
    }
}
