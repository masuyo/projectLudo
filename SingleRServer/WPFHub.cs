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
        private static ConcurrentDictionary<string, string> connections = new ConcurrentDictionary<string, string>();
        private static ConcurrentDictionary<string, LudoPlayer> players = new ConcurrentDictionary<string, LudoPlayer>();


        //belépésnél ( Hupproxy.Invoke("Login",email,password) ) üzenet érkezik a szervernek, paraméterként a belogolandó emailje és jelszava
        //ha sikerül minden akkor a kliens SetGuid metódusán keresztül visszaküld egy stringet, ez lesz az ehhez a kapcsolathoz tartozó azonosító
        //ha nem sikerül akkor akkor a kliens LoginError metódusán keresztül jelez
        public void Login(string email,string password,string selectedgametype)
        { 
            using (UsersRepository repo = new UsersRepository())
            {
                User user = repo.GetByEmailID("erika@email.com");
                if (user != null && user?.Password == "asd123")
                {
                    //if (!connections.TryAdd(Context.ConnectionId, user.Guid)) {
                      //  Clients.Caller.LoginError(); return;
                       // }
                    //LudoPlayer newplayer = new LudoPlayer() { Name = user.Username };
                    //players.AddOrUpdate("epicguid", newplayer,(key,oldvalue)=>newplayer);

                    //TODO: SetGuid method Hubproxy.ON<string>
                    //mentse el a hozzá tartozó Guidot, a szerver ezzel azonosítja ha esetleg (disconnect, recconect, valami történik)
                    Clients.Caller.SetGuid("epicguid");
                }
            }

            //TODO: LoginError method Hubproxy.ON<string>
            //Kezelje a Login hibát
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
