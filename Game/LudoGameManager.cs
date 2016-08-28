using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.GenericInterfacesandClasses;

namespace Game
{
    public class LudoGameManager : IGameManager<LudoGame, LudoAction>
    {
        private LudoGame _game;

        public LudoGameManager(LudoPlayer player1, LudoPlayer player2, LudoPlayer player3, LudoPlayer player4)
        {
            _game = new LudoGame(player1,player2,player3,player4,DateTime.Now);
        }

        public LudoGameManager(LudoGame newgame)
        {
            _game = newgame;
        }

        public void DoAction(LudoAction action)
        {
            throw new NotImplementedException();
        }

        public LudoGame getGame()
        {
            return _game;
        }
    }
}
