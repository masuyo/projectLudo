using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.Interfaces
{
    interface IUser
    {
        int UserID { get; }
        string UserName { get; set; }
    }
}
