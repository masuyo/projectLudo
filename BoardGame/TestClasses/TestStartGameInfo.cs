using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.TestClasses
{
    class TestStartGameInfo : IStartGameInfo
    {
        public IGameInfo MsgFromServer
        {
            get
            {
                TestGameInfo tmsg = new TestGameInfo();
                return tmsg;
            }
        }

        public IPlayer[] otherWPFPlayers
        {
            get
            {
                IPlayer[] ret = new TestPlayer[3] {
                    new TestPlayer(1, PlayerColor.BLUE),
                    new TestPlayer(2, PlayerColor.YELLOW),
                    new TestPlayer(3, PlayerColor.GREEN)
                };
                return ret;
            }
        }

        public IPlayer WPFPlayer
        {
            get
            {
                return new TestPlayer(0, PlayerColor.RED);
            }
        }
    }
}
