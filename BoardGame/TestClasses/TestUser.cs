using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.TestClasses
{
    class TestUser : IUser
    {
        private int userID;
        private string userName;
        public TestUser(int userID, string userName)
        {
            this.userID = userID;
            this.userName = userName;
        }
        public int UserID { get { return userID; } }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
