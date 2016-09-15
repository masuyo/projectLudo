using SharedLudoLibrary.ClientClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces
{
    public interface IStartGameInfo
    {
        Player WPFPlayer { get; }
        Player[] OtherWPFPlayers { get; }
        GameInfo MsgFromServer { get; }
    }
}
