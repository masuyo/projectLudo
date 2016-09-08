using BoardGame.TestClasses;
using BoardGame.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        LoginView VM;
        public MainWindow()
        {
            InitializeComponent();
            VM = LoginView.GetVM;
            this.DataContext = VM;

           // this.Background = LoginView.GetBG;

        }



        private void Login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Logging in...");
            if (sender is Label)
            {
                if (!String.IsNullOrEmpty(VM.UserName) && VM.UserName.Length > 5 &&
                    !String.IsNullOrEmpty(VM.Password) && VM.Password.Length > 5)
                {
                    var sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(VM.Password));
                    string hashedPassword = new ASCIIEncoding().GetString(sha1data);

                    TestLudoServer TLS = new TestLudoServer();
                    VM.UserID = TLS.Login(VM.UserName, hashedPassword, VM.SelectedGameType);

                    if (VM.UserID != -1)
                    {
                        VM.AuthenticationSuccess = true;
                        ConnectToGameWindow rooms = new ConnectToGameWindow(VM.UserName);
                        this.Close();
                        rooms.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Failed to login.");
                    }
                }
                else
                {
                    Console.WriteLine(VM.UserName + "  " + VM.Password);
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
        private void ForgotPswd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Forgot");
        }

        private void Pswd_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is PasswordBox && String.IsNullOrEmpty((sender as PasswordBox).Password))
            {
                VM.PassMessage = "Enter password";
            }

        }
        private void Uname_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBox && !String.IsNullOrEmpty((sender as TextBox).Text))
            {
                VM.UserName = (sender as TextBox).Text;
            }
        }
        private void LblExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
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
                Console.WriteLine(VM.Password);
            }
        }
    }
}
