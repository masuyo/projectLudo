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
        private Random randomgenerator;
        public int dice1 { get; set; }
        public int dice2 { get; set; }

        public LudoGameManager(LudoPlayer player1, LudoPlayer player2, LudoPlayer player3, LudoPlayer player4)
        {
            _game = new LudoGame(player1, player2, player3, player4, DateTime.Now);
            randomgenerator = new Random();
            dice1 = 0;
            dice2 = 0;
        }

        public LudoGameManager(LudoGame newgame)
        {
            _game = newgame;
            randomgenerator = new Random();
            dice1 = 0;
            dice2 = 0;
        }

        public void DoAction(LudoAction action)
        {
            if (action.doer != _game.nextplayer) throw new ArgumentException("Non available move(other player has the turn)");

            switch (action._actionType)
            {
                case LudoActionType.Throw:
                    Throw();
                    break;
                case LudoActionType.Move:
                    if ((action as MoveLudoAction).Amount == dice1)
                    {
                        dice1 = 0;
                        Move((action as MoveLudoAction));
                    }
                    else if ((action as MoveLudoAction).Amount == dice2)
                    {
                        dice2 = 0;
                        Move((action as MoveLudoAction));          
                    }
                    else throw new ArgumentException("Not valid amount to move");
                    break;
                default:
                    break;
            }
        }

        public LudoGame getGame()
        {
            return _game;
        }

        private void Move(MoveLudoAction action)
        {
            var doer = (LudoPlayer)action.doer;
            
            switch (action.Puppet)
            {
                case 1:
                    doer.puppet1position += action.Amount;
                    break;
                case 2:
                    doer.puppet2position += action.Amount;
                    break;
                case 3:
                    doer.puppet3position += action.Amount;
                    break;
                case 4:
                    doer.puppet4position += action.Amount;
                    break;
                default:
                    break;
            }
            
            if(dice1==0 && dice2 == 0)
            {
                if (doer.sequence == 4) _game.nextplayer = _game.Players.Where(akt => akt.sequence == 1).SingleOrDefault();
                else _game.nextplayer = _game.Players.Where(akt => akt.sequence == doer.sequence + 1).SingleOrDefault();
                _game.Rounds++;
            }
        }

        private void Throw()
        {
            if (dice1 != 0 & dice2 != 0) throw new InvalidOperationException("Player has already throw with the dices");
            dice1 = randomgenerator.Next(1, 6);
            dice2 = randomgenerator.Next(1, 6);
        }
    }
}
