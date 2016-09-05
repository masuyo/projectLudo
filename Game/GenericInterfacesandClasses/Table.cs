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
            _password = password;
            Creator = newcreator;
            Players = new List<TPlayer>();
            Players.Add(Creator);
        }

        public string Name { get; private set; }

        protected string _password;

        public IGameManager<TGame, TAction> Gamemanager { get; protected set; }

        public TPlayer Creator { get; private set; }

        public List<TPlayer> Players { get; private set; }

        abstract public void Start();

        abstract public void AddPlayer(TPlayer newplayer,string password);
    }
}
