using BoardGame.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Interfaces;

namespace BoardGame.TestClasses
{
    class TestLudoClient : ILudoClient
    {
        public void GameStart(IGameInfo gameInfo)
        {
            throw new NotImplementedException();
        }

        public void GameStateRefresh(IGameInfo gameInfo)
        {
            throw new NotImplementedException();
        }
    }
}
