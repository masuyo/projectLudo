using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Views
{
    class LudoView : Bindable
    {
        ObservableCollection<string> serverMsgs;
        ObservableCollection<string> chatMsgs;
        string userName;
        string chatMsg;
        public ObservableCollection<string> ServerMsgs
        {
            get { return serverMsgs; }

            set { SetProperty(ref serverMsgs, value); }
        }
        public ObservableCollection<string> ChatMsgs
        {
            get { return chatMsgs; }

            set { SetProperty(ref chatMsgs, value); }
        }

        public string UserName { get { return userName; } set { SetProperty(ref userName, value); } }
        public string ChatMsg
        {
            get { return chatMsg; }

            set { SetProperty(ref chatMsg, value); }
        }

        static LudoView VM;
        private LudoView()
        {
            serverMsgs = new ObservableCollection<string>();
            chatMsgs = new ObservableCollection<string>();
        }
        public static LudoView GetVM
        {
            get
            {
                if (VM == null)
                {
                    VM = new LudoView();
                }
                return VM;
            }
        }
    }
}
