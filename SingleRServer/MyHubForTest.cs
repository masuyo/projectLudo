using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace SignalRServer
{
    public class MyHubForTest : Hub
    {

        public void Send(string message)
        {
            Clients.All.getMessage(message);
        }

        public void Complex(string message)
        {

            Clients.All.ComplexMethod(new User() {Username=message,EmailID="nagyonemail@email.com"});
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
