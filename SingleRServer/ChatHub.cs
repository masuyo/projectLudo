using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRServer
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public override Task OnConnected()
        {
            Console.WriteLine("MVC Client connected:" + Context.ConnectionId);
           
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            
            Console.WriteLine("MVC Client disconnected\nConnectionId:\t" + Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }
    }
}
