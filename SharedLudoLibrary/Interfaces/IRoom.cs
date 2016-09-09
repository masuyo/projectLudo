using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces
{
    public interface IRoom
    {
        int ID { get; }
        string Name { get; }
        string Password { get; }
        int AvailablePlaces { get; }
    }
}
