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
using SharedLudoLibrary.Interfaces.Server;
using SharedLudoLibrary.ClientClasses;
using SharedLudoLibrary.Interfaces;

namespace SignalRServer
{
    public class WPFHub : Hub,ILudoServer,IChatServer
    {
        
        public override Task OnConnected()
        {
            Console.WriteLine("Client connected:" + Context.ConnectionId);
            connectionid_guid.TryAdd(Context.ConnectionId, "+");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string leaverguid;
            connectionid_guid.TryRemove(Context.ConnectionId, out leaverguid);
            Console.WriteLine("Client disconnected\nConnectionId:\t" + Context.ConnectionId+"\nGuID:\t" + leaverguid);
            
            //TODO do someting with the leaver!!

            return base.OnDisconnected(stopCalled);
        }

       
        private static ConcurrentDictionary<string, LudoPlayer> guid_player = new ConcurrentDictionary<string, LudoPlayer>();
        private static ConcurrentDictionary<string, string> connectionid_guid = new ConcurrentDictionary<string, string>();
        private static ConcurrentDictionary<string, LudoTable> name_table = new ConcurrentDictionary<string, LudoTable>();

        //belépésnél ( Hupproxy.Invoke("Login",email,password) ) üzenet érkezik a szervernek, paraméterként a belogolandó emailje és jelszava
        //ha sikerül minden akkor a kliens SetGuid metódusán keresztül visszaküld egy stringet, ez lesz az ehhez a kapcsolathoz tartozó azonosító
        //ha nem sikerül akkor akkor a kliens LoginError metódusán keresztül jelez
        public void GetLogin(string username, string password, string selectedgametype = "LUDO")
        {
            using (UsersRepository repo = new UsersRepository())
            {
                Entities.User user = repo.GetByName(username);

                //TODO: HASH THE PW
                if (user != null && user?.Password == user?.Password)
                {

                    foreach (var item in connectionid_guid)
                    {
                        //ha van ilyen guid-dal bejelentkezve valaki
                        if (item.Value == user.Guid)
                        {
                            Clients.Caller.SendLoginError();
                            return;
                        }
                    }

                    connectionid_guid.AddOrUpdate(Context.ConnectionId, user.Guid, (key, oldvalue) => user.Guid);
                    //TODO: SetGuid method Hubproxy.ON<string>
                    //mentse el a hozzá tartozó Guidot, a szerver ezzel azonosítja ha esetleg (disconnect, recconect, valami történik)
                    Clients.Caller.SendLogin(user.Guid);
                }

                //TODO: LoginError method Hubproxy.ON<string>
                //Kezelje a Login hibát
                else Clients.Caller.SendLoginError();
            }
        }

        public void GetGameTypes()
        {
            List<string> gameTypes = new List<string>();
            gameTypes.Add("LUDO");
            Clients.Caller.SendGameTypes(gameTypes);
        }

        public void GetAllRoomList(string guid)
        {
            List<Room> rooms = new List<Room>();
            foreach (var item in name_table)
            {
                LudoTable room = item.Value;
                //TODO 0 helyett mi? 
                Room newroom = new Room(room.Players.Count-4,0, room.Name, room.Password);
                rooms.Add(newroom);
            }
            Clients.Caller.SendAllRoomList(rooms);
        }

        public void GetUsersInRoom(string guid,IRoom room)
        {
            LudoTable table = name_table[room.Name];
            if (table == null) return;
            List<SharedLudoLibrary.ClientClasses.User> users = new List<SharedLudoLibrary.ClientClasses.User>();

            using (UsersRepository userrepo = new UsersRepository())
            {
                foreach (var item in table.Players)
                {
                    Entities.User user = userrepo.GetByName(item.Name);
                    //TODO 0 helyett mi? 
                    SharedLudoLibrary.ClientClasses.User newuser = new SharedLudoLibrary.ClientClasses.User(item.Name);
                    users.Add(newuser);
                }
            }

            Clients.Caller.SendPlayersInRoom(users);
        }

        public void GetCreateRoom(string guid,IRoom newRoom)
        {
            
            LudoPlayer player;
            using (UsersRepository userrepo = new UsersRepository())
            {
                Entities.User user = userrepo.GetByGuid(guid);
                player= new LudoPlayer(user.Username);
            }

            LudoTable newtable = new LudoTable(player, newRoom.Name, newRoom.Password);
            name_table.TryAdd(newRoom.Name, newtable);
            guid_player.TryAdd(guid, player);

            Groups.Add(Context.ConnectionId, newRoom.Name);
     
            using (InvationDesktopRepository tablerepo = new InvationDesktopRepository())
            {
                //adatbázishoz adás
            }

            Clients.Caller.SendCreateRoom(new Room(newtable.Players.Count - 4, 0, newtable.Name, newtable.Password));
        }

        public void GetConnectUserToRoom(string guid,IUser user, IRoom room)
        {
           
            bool connectedToRoom = false;

            LudoPlayer player;
            using (UsersRepository userrepo = new UsersRepository())
            {
                Entities.User newuser = userrepo.GetByGuid(guid);
                player = new LudoPlayer(newuser.Username);
            }
            try
            {
                name_table[room.Name].AddPlayer(player, room.Password);
                Groups.Add(Context.ConnectionId, room.Name);

                using (InvationDesktopRepository tablerepo = new InvationDesktopRepository())
                {
                    //adatbázishoz adás
                }

                connectedToRoom = true;
            }
            catch (Exception e)
            {
               
            }

            Clients.Caller.SendConnectUserToRoom(connectedToRoom);
        }

        public void GetStart(string guid,int playerID)
        {
            LudoPlayer caller = guid_player[guid];
            LudoTable table = name_table.Where(akt => akt.Value.Creator.Name == caller.Name).SingleOrDefault().Value;

            //set color, check everybody etc
            table.SetCheck(table.Players[0], true);
            table.SetColor(table.Players[0], puppetColor.Blue);
            table.SetCheck(table.Players[1], true);
            table.SetColor(table.Players[1], puppetColor.Green);
            table.SetCheck(table.Players[2], true);
            table.SetColor(table.Players[2], puppetColor.Yellow);
            table.SetCheck(table.Players[3], true);
            table.SetColor(table.Players[3], puppetColor.Red);
            //set vége
            table.Start();

            StartGameInfo startgameinfo = new StartGameInfo();
            GameInfo gameinfo = new GameInfo();
            //fill game info
           
            //set startgameinfo

            Clients.Group(table.Name).SendStart(gameinfo);
        }

        public void GetMove(string guid,int playerID, int actPoz, int destPoz)
        {
            GameInfo gameinfo = new GameInfo();
            throw new NotImplementedException();
        }

        public void GetOverall(string guid,int playerID)
        {
            throw new NotImplementedException();
        }

        public void Befriend(string guid,int playerID, int friendPlayerID)
        {
            throw new NotImplementedException();
        }

        public void ConnectToRoom(int userID, IRoom room)
        {
            throw new NotImplementedException();
        }

        public void Message(int playerID, string text)
        {
            throw new NotImplementedException();
        }
    }
}
