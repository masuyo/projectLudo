using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.ClientClasses
{
    class User : IUser
    {
        private int userID;
        private string userName;
        public User(int userID, string userName)
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