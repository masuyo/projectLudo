using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pageLudo.FakeData.DataClasses
{
    public class UserData
    {
        public string Username { get; set; }
        public string EmailID { get; set; }

        // Friend request kezeléséhez
        public string AreWeFriends { get; set; }
        public string FriendedYou { get; set; }
        public string FriendedMe { get; set; }
    }
}