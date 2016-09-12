using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces
{
    public interface IStartGameInfo
    {
        IPlayer WPFPlayer { get; }
        IPlayer[] OtherWPFPlayers { get; }
        IGameInfo MsgFromServer { get; }
    }
}
