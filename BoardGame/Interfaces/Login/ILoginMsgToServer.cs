using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.Login
{  
    interface ILoginMsgToServer
    {
        string UserName { get; } //from user table
        string EncryptedPassword { get; } //from user table
        string SelectedGameType { get; } //return LUDO 

       // void Login(string userName, string encryptedPassword, string selectedGameType="LUDO");

    }
}
