using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces.Client
{
    interface IChatClient
    {
        void BroadcastMessage(string playerName, string text);
    }

}
