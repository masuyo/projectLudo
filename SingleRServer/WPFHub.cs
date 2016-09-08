using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Repository.TableRepositories;
using Entities;
using Game;

namespace SignalRServer
{
    public class WPFHub : Hub
    {
        private static ConcurrentDictionary<string, string> connections;
        private static ConcurrentDictionary<string, LudoPlayer> players;


        //belépésnél ( Hupproxy.Invoke("Login",email,password) ) üzenet érkezik a szervernek 
        public void Login(string email,string password)
        {
            using (UsersRepository repo = new UsersRepository())
            {
                User user = repo.GetByEmailID(email);
                if (user != null && user?.Password == password)
                {
                    if (!connections.TryAdd(Context.ConnectionId, user.Guid)) {
                        Clients.Caller.LoginError(); return;
                        }
                    LudoPlayer newplayer = new LudoPlayer() { Name = user.Username };
                    //players.AddOrUpdate(user.Guid, newplayer,);


                    //TODO: SetGuid method Hubproxy.ON<string>
                    //mentse el a hozzá tartozó Guidot, ezzel tudom azonosítani ha esetleg (disconnect, recconect, valami történik)
                    Clients.Caller.SetGuid(user.Guid);
                }
            }

            //TODO: LoginError method Hubproxy.ON<string>
            //ha hibás jelszó, felhasználó név van megava ezt az üzenetet kapja
            Clients.Caller.LoginError(); return;
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
