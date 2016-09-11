using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pageLudo.Models
{
    // admin számára
    public class UserHandlingData
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string EmailID { get; set; }
        
        // adminjog kiosztásához
        public string Role { get; set; }
    }
}