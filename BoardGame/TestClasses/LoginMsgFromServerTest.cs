using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Views;

namespace BoardGame.TestClasses
{
    class LoginMsgFromServerTest
    {
        public bool AuthenticationSuccess
        {
            get
            {
                return true;
            }
        }

        public string ConnectionID
        {
            get
            {
                return "";
            }
        }

        public string UserName
        {
            get
            {
                return LoginView.GetVM.UserName;
            }
        }
    }
}
