using SharedLudoLibrary.ClientClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces
{
    public interface IPlayer
    {
        int ID { get; }
        string Name { get; } //from user table
        PlayerColor Color { get; }
    }
}
