using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces.Server
{
    public interface IChatServer
    {
        void GetMessage(string guid, string username, string text);
    }
}
