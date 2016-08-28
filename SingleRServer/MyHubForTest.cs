using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleRServer
{
    class MyHubForTest : Hub
    {

        public void Send(string message)
        {
            Clients.All.clientmethod(message);
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
