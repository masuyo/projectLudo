using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pageLudo.Models
{
    public class UserListingData
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string EmailID { get; set; }

        // Friend request kezeléséhez
        public string AreWeFriends { get; set; }
        public string FriendedYou { get; set; }
        public string FriendedMe { get; set; }
        public string Role { get; set; }
    }
}