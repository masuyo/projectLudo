using BoardGame.TestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces
{
    interface IPlayer
    {
        int ID { get; }
        string Name { get; } //from user table
        PlayerColor Color { get; }

    }
}
