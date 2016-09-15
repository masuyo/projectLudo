using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.ClientClasses
{
    public class User : IUser
    {
        private string userName;
        public User()
        {

        }
        public User(string userName)
        {
            this.userName = userName;
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}