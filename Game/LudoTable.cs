using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LudoTable : Table<LudoGame,LudoPlayer,LudoAction>
    {
        public LudoTable(LudoPlayer newcreator, string newname, string password = "") : base(newcreator, newname, password)
        {
            Cheks = new Dictionary<LudoPlayer, bool>();
            Cheks.Add(newcreator, false);
            Startable = false;
            Started = false;
        }

        public bool Startable { get; private set; }

        public Dictionary<LudoPlayer,bool> Cheks { get; private set; }
        public bool Started { get; private set; }

        public override void AddPlayer(LudoPlayer newplayer, string password)
        {
            if (password == this._password)
            {
                Players.Add(newplayer);
                Cheks.Add(newplayer, false);
            }
            else throw new ArgumentException("Wrong password");           
        }

        public override void LeavePlayer(LudoPlayer player)
        {
            Players.Remove(player);
            Cheks.Remove(player);
        }

        public void SetCheck(LudoPlayer player, bool value)
        {

            Cheks[player] = value;
            Startable = StartCheck();
        }

        public void SetColor(LudoPlayer player, puppetColor color)
        {
            Players.Find(akt => player == akt).color = color;
            Cheks[player] = false;
            Startable = false;
        }

        public override void Start()
        {
            if (Startable)
            {
                Random random = new Random();
                foreach (var item in Players)
                {
                    item.sequence = random.Next();
                }

                Players.OrderBy(akt => akt.sequence);

                foreach (var item in Players)
                {
                    item.sequence = Players.IndexOf(item) + 1;
                }
                Gamemanager = new LudoGameManager(Players[0], Players[1], Players[2], Players[3]);
                Started = true;
            }
        }

        private bool StartCheck()
        {
            if (Players.Count != 4) return false;

            foreach (var ready in Cheks)
            {
                if (ready.Value == false) return false;
            }

            List<puppetColor> colors = new List<puppetColor>();
            foreach (var player in Players)
            {
                if (player.color == puppetColor.Default) return false;

                foreach (var color in colors)
                {
                    if (player.color == color) return false;
                }

                colors.Add(player.color);
            }

            return true;
        }

        public LudoGame getGame()
        {
            if (Started) return Gamemanager.getGame();
            else return null;
        }
    }
}
