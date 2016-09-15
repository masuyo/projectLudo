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
            Console.WriteLine("Client with {0} connection try to login",Context.ConnectionId);
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

                    LudoPlayer player;
                    using (UsersRepository userrepo = new UsersRepository())
                    {
                        Entities.User userke = userrepo.GetByGuid(user.Guid);
                        player = new LudoPlayer(user.Username);
                    }

                    LudoTable newtable = new LudoTable(player, user.UserID.ToString(), user.UserID.ToString());
                    name_table.TryAdd(user.UserID.ToString(), newtable);
                    guid_player.TryAdd(user.Guid, player);

                    Groups.Add(Context.ConnectionId, newtable.Name);

                    using (InvationDesktopRepository tablerepo = new InvationDesktopRepository())
                    {
                        //adatbázishoz adás
                    }




                    connectionid_guid.AddOrUpdate(Context.ConnectionId, user.Guid, (key, oldvalue) => user.Guid);

                    Console.WriteLine("Clien logged in with {0} guid",user.Guid);
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
            Console.WriteLine("Client with {0} guid asked allroomlist.",guid);
            List<Room> rooms = new List<Room>();
            Console.WriteLine("Rooms:");
            foreach (var item in name_table)
            {
                LudoTable room = item.Value;
                //TODO 0 helyett mi?
                Console.WriteLine(room.Name);
                Room newroom = new Room() {Name=room.Name,ID=0,AvailablePlaces=4-room.Players.Count,Password=room.Password};
                rooms.Add(newroom);
            }
            Clients.Caller.SendAllRoomList(rooms);
        }

        public void GetUsersInRoom(string guid,IRoom room)
        {
            Console.WriteLine("Client with {0} guid asked usersinroom in {1} room",guid,room.Name);
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
            Console.WriteLine("Client with {0} guid try to creatreroom with {1} name",guid,newRoom.Name);
            LudoPlayer player;
            using (UsersRepository userrepo = new UsersRepository())
            {
                Entities.User user = userrepo.GetByGuid(guid);
                player = new LudoPlayer(user.Username);
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
            Console.WriteLine("Client with {0} guid connectusertoroom to {1} room",guid,room.Name);
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
                Console.WriteLine(e.Message);
            }

            Clients.Caller.SendConnectUserToRoom(connectedToRoom);
        }

        public void GetStart(string guid,int playerID)
        {
            Console.WriteLine("Client with {0} guid try to start a game.",guid);
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

            StartGameInfo startgameinfo = MakeStartGameInfo(table);
            Clients.Group(table.Name).SendStart(startgameinfo);
        }

        private StartGameInfo MakeStartGameInfo(LudoTable table)
        {
            StartGameInfo startgameinfo = new StartGameInfo();
            GameInfo gameinfo = MakeGameInfo(table);

            startgameinfo.MsgFromServer = gameinfo;

            LudoPlayer nextplayer = table.getGame().Nextplayer;
            PlayerColor color = PlayerColor.RED;
            switch (nextplayer.color)
            {
                case puppetColor.Red:
                    color = PlayerColor.RED;
                    break;
                case puppetColor.Yellow:
                    color = PlayerColor.YELLOW;
                    break;
                case puppetColor.Blue:
                    color = PlayerColor.BLUE;
                    break;
                case puppetColor.Green:
                    color = PlayerColor.GREEN;
                    break;
                case puppetColor.Default:
                    break;
                default:
                    break;
            }
            startgameinfo.WPFPlayer = new SharedLudoLibrary.ClientClasses.Player(nextplayer.sequence, color);

            SharedLudoLibrary.ClientClasses.Player[] otherwpfplayers = new SharedLudoLibrary.ClientClasses.Player[3];
            LudoPlayer[] otherplayers = new LudoPlayer[3];
            List<LudoPlayer> except = new List<LudoPlayer>();
            except.Add(nextplayer);
            otherplayers = table.getGame().Players.Except(except).ToArray();

            for (int i = 0; i < 3; i++)
            {
                PlayerColor othercolor = PlayerColor.RED;
                switch (otherplayers[i].color)
                {
                    case puppetColor.Red:
                        color = PlayerColor.RED;
                        break;
                    case puppetColor.Yellow:
                        color = PlayerColor.YELLOW;
                        break;
                    case puppetColor.Blue:
                        color = PlayerColor.BLUE;
                        break;
                    case puppetColor.Green:
                        color = PlayerColor.GREEN;
                        break;
                    case puppetColor.Default:
                        break;
                    default:
                        break;
                }
                SharedLudoLibrary.ClientClasses.Player otherwpfplayer = new SharedLudoLibrary.ClientClasses.Player(otherplayers[i].sequence, othercolor);
                otherwpfplayers[i] = otherwpfplayer;
            }
            startgameinfo.OtherWPFPlayers = otherwpfplayers;

            return startgameinfo;
        }

        private GameInfo MakeGameInfo(LudoTable table)
        {
            GameInfo gameinfo = new GameInfo();

            LudoPlayer nextplayer = table.getGame().Nextplayer;
            gameinfo.ActivePlayerID = nextplayer.sequence;
            gameinfo.Msg = "";
            gameinfo.OnManHit = false;
            gameinfo.Reroll = false;
            gameinfo.PuppetList = CreatePuppetList(table.Gamemanager);
            if((table.Gamemanager as LudoGameManager).Dice1==0 && (table.Gamemanager as LudoGameManager).Dice2 == 0)
            {
                table.Gamemanager.DoAction(new ThrowLudoAction(table.getGame().Nextplayer));   
            }
            gameinfo.Dice1 = (table.Gamemanager as LudoGameManager).Dice1;
            gameinfo.Dice2 = (table.Gamemanager as LudoGameManager).Dice2;
                        
            return gameinfo;
        }

        private List<IPuppet> CreatePuppetList(IGameManager<LudoGame, LudoAction> gamemanager)
        {
            List<IPuppet> puppetlist = new List<IPuppet>();

            LudoGameManager gm = (LudoGameManager)gamemanager;

            foreach (LudoPlayer player in gm.getGame().Players)
            {
                PlayerColor color = PlayerColor.RED;
                switch (player.color)
                {
                    case puppetColor.Red:
                        color = PlayerColor.RED;
                        break;
                    case puppetColor.Yellow:
                        color = PlayerColor.YELLOW;
                        break;
                    case puppetColor.Blue:
                        color = PlayerColor.BLUE;
                        break;
                    case puppetColor.Green:
                        color = PlayerColor.GREEN;
                        break;
                    case puppetColor.Default:
                        break;
                    default:
                        break;
                }

                SharedLudoLibrary.ClientClasses.Player newplayer = new SharedLudoLibrary.ClientClasses.Player(player.sequence, color);

                for (int i=0;i<player.Puppets.Length;i++)
                {
                    int puppet = player.Puppets[i];
                    int pozition = 0;
                    switch (player.color)
                    {
                        case puppetColor.Red:
                            if (puppet == 0) pozition = 10 + i + 1;
                            else if (puppet < 41) pozition = puppet - 1 + 110;
                            else pozition = puppet + 60;
                            break;
                        case puppetColor.Yellow:
                            if (puppet == 0) pozition = 30 + i + 1;
                            else if (puppet < 41) pozition = puppet - 1 + 130;
                            else pozition = puppet + 260;
                            break;
                        case puppetColor.Blue:
                            if (puppet == 0) pozition = 20 + i + 1;
                            else if (puppet < 41) pozition = puppet - 1 + 120;
                            else pozition = puppet + 160;
                            break;
                        case puppetColor.Green:
                            if (puppet == 0) pozition = 40 + i + 1;
                            else if (puppet < 41) pozition = puppet - 1 + 140;
                            else pozition = puppet + 360;
                            break;
                        case puppetColor.Default:
                            break;
                        default:
                            break;
                    }
                    puppetlist.Add(new Puppet(i+1, pozition, newplayer));
                }
            }

            return puppetlist;
        }

        public void GetMove(string guid,int puppetID, int actPoz, int destPoz)
        {
            Console.WriteLine("Client with {0} guid try to move.", guid);
            LudoPlayer caller = guid_player[guid];
            LudoTable table = name_table.Where(akt => akt.Value.Players.Contains(caller)).SingleOrDefault().Value;
            int amount = destPoz - actPoz;
            table.Gamemanager.DoAction(new MoveLudoAction(table.getGame().Nextplayer, puppetID, amount));
            GameInfo gameinfo = MakeGameInfo(table);

            Clients.Group(table.Name).SendMove(gameinfo);
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

        public void GetMessage(string guid, string username, string text)
        {
            throw new NotImplementedException();
        }

    }
}
