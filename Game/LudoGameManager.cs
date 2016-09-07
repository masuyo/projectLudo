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
        public int Dice1 { get; private set; }
        public int Dice2 { get; private set; }

        public LudoGameManager(LudoPlayer player1, LudoPlayer player2, LudoPlayer player3, LudoPlayer player4)
        {
            _game = new LudoGame(player1, player2, player3, player4, DateTime.Now);

            _game.nextplayer = _game.Players.Where(akt => akt.sequence == 1).SingleOrDefault();

            randomgenerator = new Random();
            Dice1 = 0;
            Dice2 = 0;
        }

        public LudoGameManager(LudoGame newgame)
        {
            _game = newgame;
            randomgenerator = new Random();
            Dice1 = 0;
            Dice2 = 0;
        }

        public void DoAction(LudoAction action)
        {
            if (action.doer != _game.nextplayer) return;

            switch (action._actionType)
            {
                case LudoActionType.Throw:
                    Throw();
                    break;
                case LudoActionType.Move:
                        Move((action as MoveLudoAction));
                    break;
                case LudoActionType.Check:
                    NextPlayer();
                    break;
                default:
                    break;
            }
        }

        public LudoGame getGame()
        {
            return _game;
        }

        private void Throw()
        {
            if (Dice1 != 0 & Dice2 != 0) throw new InvalidOperationException("Player has already throw with the dices");
            Dice1 = randomgenerator.Next(1, 6);
            Dice2 = randomgenerator.Next(1, 6);
        }

        private void Move(MoveLudoAction action)
        {
            var doer = (LudoPlayer)action.doer;

            if (action.Puppet==0 && action.Amount==0 && ((Dice1==6 && Dice2==6) || (Dice1==1 && Dice2==1)))
            {
                doer.Puppets[action.Puppet] = 1;
                Dice1 = 0; Dice2 = 0;
            }else
            {
                int nextposition = doer.Puppets[action.Puppet] + action.Amount;
                if (nextposition > 44) throw new InvalidOperationException("Player can move out of the house");
                foreach (var puppet in doer.Puppets)
                {
                    if (nextposition == puppet) throw new InvalidOperationException("Player already has a puppet on that cell");
                }

                if (action.Amount == Dice1)
                {            
                    MoveTo(action.Puppet, nextposition, doer);
                    Dice1 = 0;
                }else if(action.Amount == Dice2)
                {
                    MoveTo(action.Puppet, nextposition, doer);
                    Dice2 = 0;
                }

                if (Dice1 == 0 && Dice2 == 0) NextPlayer();
            }
        }

        private void MoveTo(int puppet, int nextposition, LudoPlayer doer)
        { 
            for (int i=doer.Puppets[puppet]+1; i < nextposition+1; i++) {
                Step(i, doer.color);
            }
            doer.Puppets[puppet] = nextposition;
        }

        private void Step(int i, puppetColor color)
        {
            foreach (var player in _game.Players)
            {
                for(int j = 0; j<4;j++)
                {
                    int otherpuppetposition = 0;
                    if (player.Puppets[j] < 41)
                    {
                        switch (player.color)
                        {
                            case puppetColor.Red:
                                otherpuppetposition = (player.Puppets[j] + 0) % 40;
                                break;
                            case puppetColor.Yellow:
                                otherpuppetposition = (player.Puppets[j] + 20) % 40;
                                break;
                            case puppetColor.Blue:
                                otherpuppetposition = (player.Puppets[j] + 10) % 40;
                                break;
                            case puppetColor.Green:
                                otherpuppetposition = (player.Puppets[j] + 30) % 40;
                                break;
                            case puppetColor.Default:
                                break;
                            default:
                                break;
                        }
                        if (i == otherpuppetposition && color!=player.color) player.Puppets[j] = 0;
                    }
                }
            }
        }

        private void NextPlayer()
        {
            if (_game.nextplayer.sequence == 4) _game.nextplayer = _game.Players.Where(akt => akt.sequence == 1).SingleOrDefault();
            else _game.nextplayer = _game.Players.Where(akt => akt.sequence == _game.nextplayer.sequence + 1).SingleOrDefault();
            _game.Rounds++;
        }
    }
}
