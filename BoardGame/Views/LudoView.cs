using SharedLudoLibrary.ClientClasses;
using SharedLudoLibrary.Interfaces;
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
        DiceView diceVM;
        ObservableCollection<string> serverMsgs;
        ObservableCollection<string> chatMsgs;
        string chatMsg;
        string userName;
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

        public string ChatMsg
        {
            get { return chatMsg; }

            set { SetProperty(ref chatMsg, value); }
        }
        public string UserName
        {
            get { return userName; }

            set { SetProperty(ref userName, value); }
        }
        static LudoView VM;
        private LudoView()
        {
            serverMsgs = new ObservableCollection<string>();
            chatMsgs = new ObservableCollection<string>();
            diceVM = new DiceView();

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

        //IStartGameInfo implementation
        IPlayer wpfPlayer;
        IPlayer[] otherPlayers;
        IGameInfo msgFromServer;
        public IPlayer WPFPlayer
        {
            get { return wpfPlayer; }

            set { SetProperty(ref wpfPlayer, value); }
        }
        public IPlayer[] OtherWPFPlayers
        {
            get { return otherPlayers; }

            set { SetProperty(ref otherPlayers, value); }
        }
        public IGameInfo GameSateInfo
        {
            get { return msgFromServer; }

            set { SetProperty(ref msgFromServer, value); }
        }

        public DiceView DVM
        {
            get { return diceVM; }

            set { SetProperty(ref diceVM, value); }
        }
    }
}
