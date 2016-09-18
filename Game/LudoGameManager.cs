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

        public bool OnManHit { get; set; }
        public int Dice1 { get; private set; }
        public int Dice2 { get; private set; }
        public bool Reroll { get; private set; }

        public LudoGameManager(LudoPlayer player1, LudoPlayer player2, LudoPlayer player3, LudoPlayer player4)
        {
            _game = new LudoGame(player1, player2, player3, player4, DateTime.Now);

            _game.Nextplayer = _game.Players.Where(akt => akt.sequence == 1).SingleOrDefault();

            randomgenerator = new Random();
            Dice1 = 0;
            Dice2 = 0;
            OnManHit = false;
            Reroll = false;
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

            OnManHit = false;
            Reroll = false;
            if (_game.Winner != null) throw new InvalidOperationException("Game Already Over");
            if (action.doer != _game.Nextplayer) return;

            switch (action._actionType)
            {
                case LudoActionType.Throw:
                    Throw();
                    Console.WriteLine("throw ludo action");
                    break;
                case LudoActionType.Move:
                        Move((action as MoveLudoAction));
                    Console.WriteLine("move ludo action");
                    break;
                case LudoActionType.Check:
                    NextPlayer();
                    Console.WriteLine("check ludo action");
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
            Dice1 = 6;
            Dice2 = 6;
        }

        private void Move(MoveLudoAction action)
        {
            var doer = (LudoPlayer)action.doer;

            if (doer.Puppets[action.Puppet]==0)
            {
                if ((Dice1 == 6 && Dice2 == 6) || (Dice1 == 1 && Dice2 == 1))
                {
                    doer.Puppets[action.Puppet] = action.Amount;
                    Dice1 = 0; Dice2 = 0;
                    Reroll = true;
                }
                else throw new ArgumentException("Can not move from the start without two 6 or 1 dices");
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

                if (doer.Puppets.All(akt => akt > 40))
                {
                    _game.Winner = doer;
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
            switch (color)
            {
                case puppetColor.Red:
                    i = (i + 0) % 40;
                    break;
                case puppetColor.Yellow:
                    i = (i + 20) % 40;
                    break;
                case puppetColor.Blue:
                    i = (i + 10) % 40;
                    break;
                case puppetColor.Green:
                    i = (i + 30) % 40;
                    break;
                case puppetColor.Default:
                    break;
                default:
                    break;
            }

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
                        if (i == otherpuppetposition && color != player.color) { OnManHit = true; player.Puppets[j] = 0; }
                    }
                }
            }
        }

        private void NextPlayer()
        {
            Dice1 = 0;
            Dice2 = 0;
            if (_game.Nextplayer.sequence == 4) _game.Nextplayer = _game.Players.Where(akt => akt.sequence == 1).SingleOrDefault();
            else _game.Nextplayer = _game.Players.Where(akt => akt.sequence == _game.Nextplayer.sequence + 1).SingleOrDefault();
            _game.Rounds++;
        }     
    }
}
