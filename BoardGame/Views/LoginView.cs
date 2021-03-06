﻿using BoardGame.TestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BoardGame.Views
{
    enum GameType { LUDO, CHESS, TICTACTOE, AAA, SSSSS, DDDD }
    class LoginView : Bindable
    {
        private string userName;
        private string passMessage;
        private string password;
        private bool authenticationSuccess;
        private string selectedGameType;
        public string LoginMSG
        {
            get
            {
                if (authenticationSuccess)
                {
                    return "Logging in";
                }
                return "Login";
            }
        }

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
        
        public bool AuthenticationSuccess
        {
            get { return authenticationSuccess; }

            set { SetProperty(ref authenticationSuccess, value); OnPropertyChanged("LoginMSG"); }
        }

        public List<string> GameTypeList
        {
            get
            {
                return new TestLudoServer().GetGameTypes();
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
                selectedGameType = value;
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
            authenticationSuccess = false;
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
