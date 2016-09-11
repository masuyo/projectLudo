using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game
{
    abstract  public class Table<TGame,TPlayer,TAction>
    {
        public Table(TPlayer newcreator,string newname,string password="")
        {
            Name = newname;
            Password = password;
            Creator = newcreator;
            Players = new List<TPlayer>();
            Players.Add(Creator);
        }

        public string Name { get; private set; }

        private string password;

        public IGameManager<TGame, TAction> Gamemanager { get; protected set; }

        public TPlayer Creator { get; private set; }

        public List<TPlayer> Players { get; private set; }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        abstract public void Start();

        abstract public void AddPlayer(TPlayer newplayer,string password);

        abstract public void LeavePlayer(TPlayer player);
    }
}
