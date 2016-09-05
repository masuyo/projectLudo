using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErikaTestSignalR
{
    interface IChatMsg
    {
        string Name { get; }
        string SentBy { get; }
        string Time { get; }
    }
}
