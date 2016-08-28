using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ThrowLudoAction : LudoAction
    {
        public ThrowLudoAction(GenericInterfacesandClasses.Player newDoer) : base(newDoer,LudoActionType.Throw)
        { 
        }
    }
}
