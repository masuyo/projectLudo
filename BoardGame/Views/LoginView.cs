using BoardGame.Interfaces.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BoardGame.Views
{
    enum GameType { LUDO, CHESS, TICTACTOE }
    class LoginView : Bindable
    {
        private string userName;
        private string passMessage;
        private string password;
        private string connectionID;
        private bool authenticationSuccess;

        public string UserName
        {
            get { return userName; }

            set { SetProperty(ref userName, value); }
        }
        public string PassMessage
        {
            get { return passMessage; }

            set { SetProperty(ref passMessage, value); }
        }
        public string Password
        {
            get { return password; }

            set { SetProperty(ref password, value); }
        }
        public string ConnectionID
        {
            get { return connectionID; }

            set { SetProperty(ref connectionID, value); }
        }
        public bool AuthenticationSuccess
        {
            get { return authenticationSuccess; }

            set { SetProperty(ref authenticationSuccess, value); }
        }

        public List<string> GameTypeList
        {
            get
            {
                return Enum.GetNames(typeof(GameType)).ToList();
            }
        }
        public string SelectedGameType
        {
            get
            {
                return GameType.LUDO.ToString();
            }
            set
            {
                SelectedGameType = value;
            }
        }

        public static ImageBrush GetBG
        {
            get
            {
                ImageBrush imgb = new ImageBrush();
                imgb.ImageSource = new BitmapImage(new Uri(@"Images\l2.png", UriKind.Relative));
                return imgb;
            }
        }

        static LoginView VM;
        private LoginView()
        {
            userName = "Enter username";
            passMessage = "Enter password";
            password = String.Empty;
        }
        public static LoginView GetVM
        {
            get
            {
                if (VM == null)
                {
                    VM = new LoginView();
                }
                return VM;
            }
        }

    }
}
