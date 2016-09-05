using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErikaTestSignalR
{
    class MyHub : Hub
    {
        public void SendChatMsg(string message)
        {
            //Console.WriteLine(">>" + message.Name + "(" + message.SentBy + ")" + "::" + message.Time);
            Clients.All.addMessage(message);
            // just one ludo group, not all clients get the ChatMsg

        }

        public override Task OnConnected()
        {
            Console.WriteLine("Client connected:" + Context.ConnectionId);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("Client disconnected" + Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}
