using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.GenericInterfacesandClasses;

namespace Game.LudoActions
{
    public class CheckLudoAction : LudoAction
    {
        public CheckLudoAction(Player newDoer) : base(newDoer, LudoActionType.Check)
        {
        }
    }
}
