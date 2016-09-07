using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.Login
{
    interface IPreLoginMsgFromServer
    {
        List<string> GameTypeList(); //return LUDO, CHESS, TICTACTOE 
    }
}

