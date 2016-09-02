using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.Login
{
    interface IPreLoginMsgFromServer
    {
        List<string> GameType { get; } //return LUDO, CHESS, TICTACTOE 
    }
}
