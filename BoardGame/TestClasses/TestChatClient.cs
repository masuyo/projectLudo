using BoardGame.Interfaces.Client;
using BoardGame.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BoardGame.TestClasses
{
    class TestChatClient : IChatClient
    //Additional information: This type of CollectionView does not support changes to its SourceCollection
    //from a thread different from the Dispatcher thread.
    {
        public void BroadcastMessage(string playerName, string text)
        {
            LudoView.GetVM.ChatMsgs.Add(playerName + ": " + text);
        }

    }
}
