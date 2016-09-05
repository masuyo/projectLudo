using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    public class ChatMsg : IChatMsg
    {
        private string name;
        private string sentBy;
        private string time;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string SentBy
        {
            get
            {
                return sentBy;
            }
        }

        public string Time
        {
            get
            {
                return time;
            }
        }
        public ChatMsg(string name, string sentBy)
        {
            this.name = name;
            this.sentBy = sentBy;
            this.time = DateTime.Now.ToShortDateString() + " >> " + DateTime.Now.ToLongTimeString();
        }
        public ChatMsg(string name, string sentBy, string dateTime)
        {
            this.name = name;
            this.sentBy = sentBy;
            this.time = dateTime;
        }
    }
}
