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
using System.Security.Cryptography;

namespace SignalRServer
{
    public class WPFHub : Hub
    {
        //private static ConcurrentDictionary<string, string> connections = new ConcurrentDictionary<string, string>();
        private static ConcurrentDictionary<string, LudoPlayer> guid_player = new ConcurrentDictionary<string, LudoPlayer>();
        private static ConcurrentDictionary<string, string> connectionid_guid = new ConcurrentDictionary<string, string>();

        //belépésnél ( Hupproxy.Invoke("Login",email,password) ) üzenet érkezik a szervernek, paraméterként a belogolandó emailje és jelszava
        //ha sikerül minden akkor a kliens SetGuid metódusán keresztül visszaküld egy stringet, ez lesz az ehhez a kapcsolathoz tartozó azonosító
        //ha nem sikerül akkor akkor a kliens LoginError metódusán keresztül jelez
        public void Login(string username,string password,string selectedgametype)
        { 
            using (UsersRepository repo = new UsersRepository())
            {
                User user = repo.GetByName(username);

                //TODO: HASH THE PW
                if (user != null && user?.Password == user?.Password)
                {
                    LudoPlayer newplayer = new LudoPlayer() { Name = user.Username };
                    if (guid_player.TryAdd(user.Guid, newplayer))
                    {
                        //TODO: SetGuid method Hubproxy.ON<string>
                        //mentse el a hozzá tartozó Guidot, a szerver ezzel azonosítja ha esetleg (disconnect, recconect, valami történik)
                        Clients.Caller.SetGuid(user.Guid);
                    }
                    else Clients.Caller.LoginError();              
                }

                //TODO: LoginError method Hubproxy.ON<string>
                //Kezelje a Login hibát
                else Clients.Caller.LoginError();
            }
        }


        public override Task OnConnected()
        {
            Console.WriteLine("Client connected:" + Context.ConnectionId);
            connectionid_guid.TryAdd(Context.ConnectionId, " + ");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string leaverguid;
            if (!connectionid_guid.TryRemove(Context.ConnectionId, out leaverguid)) leaverguid = " - ";
            Console.WriteLine("Client disconnected\nConnectionId:\t" + Context.ConnectionId+"\nGuID:\t" + leaverguid);
            //TODO do someting with the leaver!!
            return base.OnDisconnected(stopCalled);
        }
    }
}
