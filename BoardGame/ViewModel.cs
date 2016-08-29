using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    class ViewModel : Bindable
    {
        ObservableCollection<ChatMsg> serverMsgs;
        ObservableCollection<ChatMsg> chatMsgs;
        string chatMsg = "asd";

        public ObservableCollection<ChatMsg> ServerMsgs
        {
            get { return serverMsgs; }

            set { SetProperty(ref serverMsgs, value); }
        }
        public ObservableCollection<ChatMsg> ChatMsgs
        {
            get { return chatMsgs; }

            set { SetProperty(ref chatMsgs, value); }
        }

        public string ChatMsg
        {
            get { return chatMsg; }

            set { SetProperty(ref chatMsg, value); }
        }

        static ViewModel VM;
        private ViewModel()
        {
            serverMsgs = new ObservableCollection<ChatMsg>();
            chatMsgs = new ObservableCollection<ChatMsg>();
        }
        public static ViewModel GetVM
        {
            get
            {
                if (VM == null)
                {
                    VM = new ViewModel();
                }
                return VM;
            }
        }
    }
}
