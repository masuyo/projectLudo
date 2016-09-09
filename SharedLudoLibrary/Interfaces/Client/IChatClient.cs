using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces.Client
{
    public interface IChatClient
    {
        void SendMessage(string playerName, string text);
    }
}
