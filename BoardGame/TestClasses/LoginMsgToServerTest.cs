using BoardGame.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.TestClasses
{
    class LoginMsgToServerTest 
    {
        LoginView VM;

        public LoginMsgToServerTest()
        {
            VM = LoginView.GetVM;
        }

        public string EncryptedPassword
        {
            get
            {
                var sha1 = new SHA1CryptoServiceProvider();
                byte[] sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(VM.Password));

                //var hashedPassword = ASCIIEncoding.GetString(sha1data);


                throw new NotImplementedException();
            }
        }

        public string SelectedGameType
        {
            get
            {               
                return VM.SelectedGameType;
            }
        }

        public string UserName
        {
            get
            {
                return VM.UserName;
            }
        }
    }
}
