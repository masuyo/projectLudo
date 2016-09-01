using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces
{
    interface IMsgToServer
    {
        IMan ManMsg { get; set; }
        int PlayerID { get; set; }
    }
}
