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
        ObservableCollection<IChatMsg> serverMsgs;
        ObservableCollection<IChatMsg> chatMsgs;
        string chatMsg;
        public ObservableCollection<IChatMsg> ServerMsgs
        {
            get { return serverMsgs; }

            set { SetProperty(ref serverMsgs, value); }
        }
        public ObservableCollection<IChatMsg> ChatMsgs
        {
            get { return chatMsgs; }

            set { SetProperty(ref chatMsgs, value); }
        }

        public string ChatMsg
        {
            get { return chatMsg; }

            set { SetProperty(ref chatMsg, value); }
        }

        static LudoView VM;
        private LudoView()
        {
            serverMsgs = new ObservableCollection<IChatMsg>();
            chatMsgs = new ObservableCollection<IChatMsg>();
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
