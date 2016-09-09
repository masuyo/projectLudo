using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces
{
    interface IPuppet
    {
        int ID { get; set; }
        int Poz { get; set; }
        TestPlayer Player { get; set; }
    }
}
