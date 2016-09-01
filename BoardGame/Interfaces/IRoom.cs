using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces
{
    class IRoom
    {
        int id;
        public int ID { get; }
        public string Name { get; }
        public string Password { get; }
    }
}
