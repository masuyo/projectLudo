using BoardGame.Interfaces.Login;
using BoardGame.TestClasses;
using BoardGame.Views;
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
        LoginView VM;
        public MainWindow()
        {
            InitializeComponent();
            VM = LoginView.GetVM;
            this.DataContext = VM;

            this.Background = LoginView.GetBG;

        }



        private void Login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Logging in...");
            if (sender is Label)
            {

                if (!String.IsNullOrEmpty(VM.UserName) && VM.UserName.Length > 5 &&
                    !String.IsNullOrEmpty(VM.Password) && VM.Password.Length > 5)
                {
                    //send
                    LoginMsgToServerTest testCTS = new LoginMsgToServerTest();

                    //recieve
                    LoginMsgFromServerTest testSTC = new LoginMsgFromServerTest();
                    if (testSTC.AuthenticationSuccess)
                    {
                        ConnectToGameWindow rooms = new ConnectToGameWindow(testSTC.UserName);
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
            if (sender is PasswordBox && !String.IsNullOrEmpty((sender as PasswordBox).Password))
            {
                VM.PassMessage = String.Empty;
                VM.Password = (sender as PasswordBox).Password;
                Console.WriteLine(VM.Password);
            }
            else
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
    }
}
