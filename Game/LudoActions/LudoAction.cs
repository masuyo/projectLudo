using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract public  class LudoAction : GenericInterfacesandClasses.Action
    {
        public readonly LudoActionType _actionType;
        public LudoAction(Game.GenericInterfacesandClasses.Player newDoer,LudoActionType newactiontype) : base(newDoer)
        {
            _actionType = newactiontype;
        }
    }
}
