using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.ClientClasses
{
    public class StartGameInfo : IStartGameInfo
    {
        public StartGameInfo()
        {
           
        }

        public IGameInfo MsgFromServer
        {
            get
            {
                GameInfo tmsg = new GameInfo();
                return tmsg;
            }
        }

        public IPlayer[] OtherWPFPlayers
        {
            get
            {
                IPlayer[] ret = new Player[3] {
                    new Player(1, PlayerColor.BLUE),
                    new Player(2, PlayerColor.YELLOW),
                    new Player(3, PlayerColor.GREEN)
                };
                return ret;
            }
        }

        public IPlayer WPFPlayer
        {
            get
            {
                return new Player(0, PlayerColor.RED);
            }
        }
    }
}
