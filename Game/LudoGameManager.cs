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
        private puppetColor[] board;
        public int Dice1 { get; private set; }
        public int Dice2 { get; private set; }

        public LudoGameManager(LudoPlayer player1, LudoPlayer player2, LudoPlayer player3, LudoPlayer player4)
        {
            _game = new LudoGame(player1, player2, player3, player4, DateTime.Now);
            randomgenerator = new Random();
            Dice1 = 0;
            Dice2 = 0;
            board = new puppetColor[405];
            Refillboard();
        }

        private void Refillboard()
        {
            for (int i = 0; i < board.Count(); i++)
            {
                board[i] = puppetColor.Default;
            }
            foreach (var player in _game.Players)
            {
                foreach (int puppetposition in player.Puppets)
                {
                    board[puppetposition] = player.color;
                }
            }
        }

        public LudoGameManager(LudoGame newgame)
        {
            _game = newgame;
            randomgenerator = new Random();
            Dice1 = 0;
            Dice2 = 0;
            board = new puppetColor[405];
            Refillboard();
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
                    if ((action as MoveLudoAction).Amount == Dice1)
                    {
                        Move((action as MoveLudoAction));
                        Dice1 = 0;
                    }
                    else if ((action as MoveLudoAction).Amount == Dice2)
                    {    
                        Move((action as MoveLudoAction));
                        Dice2 = 0;
                    }
                    else return;

                    if (Dice1 == 0 && Dice2 == 0)
                    {
                        if ((action.doer as LudoPlayer).sequence == 4) _game.nextplayer = _game.Players.Where(akt => akt.sequence == 1).SingleOrDefault();
                        else _game.nextplayer = _game.Players.Where(akt => akt.sequence == (action.doer as LudoPlayer).sequence + 1).SingleOrDefault();
                        _game.Rounds++;
                    }
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

            switch (doer.color)
            {
                case puppetColor.Red:
                    break;
                case puppetColor.Yellow:
                    break;
                case puppetColor.Blue:
                    break;
                case puppetColor.Green:
                    break;
                case puppetColor.Default:
                    break;
                default:
                    break;
            }
        }

    }
}
