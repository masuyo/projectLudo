using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.GenericInterfacesandClasses;

namespace Game
{
    public class MoveLudoAction : LudoAction
    {
        public int Puppet { get; set; }
        public int Amount { get;  set; }

        public MoveLudoAction(Player newDoer,int puppet,int amount) : base(newDoer,LudoActionType.Move)
        {
            if (puppet < 0 || puppet > 4) throw new ArgumentException("Not valid puppet number");
            Puppet = puppet;
            if (amount < 0 || amount > 6) throw new ArgumentException("Not valid amount number");
            Amount = amount;
        }
    }
}
