using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GenericInterfacesandClasses
{
    abstract public class Action
    {
        public readonly Player doer;
        public Action(Player newDoer)
        {
            doer = newDoer;
        }
    }
}
