using System;
using System.Collections.Generic;
using Game;
using Game.LudoActions;
using SignalRServer.MVCData.MethodClasses;
using Repository.TableRepositories;
using System.Net.Http;
using Microsoft.AspNet.SignalR.Client;
using SignalRServer;
using SignalRServer.MVCData.DataClasses;

namespace ZoliRepoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Repotest();

            //UserActions ua = new UserActions();
            //Console.WriteLine(ua.Register("Gabi","gabipw","gabi@mail.com"));
            //Console.WriteLine(ua.Register("Erika","erikapw","erika@mail.com"));
            //Console.WriteLine(ua.Register("Zoli","zolipw","zoli@mail.com"));

            //ua.Friend("gabi@mail.com", "zoli@mail.com");
            //ua.Friend("gabi@mail.com", "erika@mail.com");
            //ua.FriendAccept("gabi@mail.com", "erika@mail.com");

            //foreach (var item in ua.UsernameSearch("i","gabi@mail.com"))
            //{
            //    Console.WriteLine("{0}\t {1}\t {2}\t {3}",item.Username,item.FriendedMe,item.FriendedYou,item.AreWeFriends);
            //}
            //Repotest();
        }

        private static void SignalRtest()
        {
            IHubProxy HubProxy;
            HubConnection Connection = new HubConnection("http://localhost:8080/signalr");
            HubProxy = Connection.CreateHubProxy("WPFHub");
            try
            {
                Connection.Start();
            } catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        private static void FriendOfMeTest()
        {
            do
            {
                Console.WriteLine("seacrher and searched:");
                int searcher = int.Parse(Console.ReadLine());
                string searched = Console.ReadLine();

                UsersRepository userepo = new UsersRepository();

                UserActions useractioner = new UserActions();
                //UserData data= useractioner.EmaildIDSearch(userepo.GetById(searched).EmailID, userepo.GetById(searcher).EmailID);
                //Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t", data.Username,data.EmailID,data.AreWeFriends,data.FriendedMe,data.FriendedYou);

                foreach (var data in useractioner.UsernameSearch(searched, userepo.GetById(searcher).EmailID))
                {
                    Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t", data.Username,data.EmailID,data.AreWeFriends,data.FriendedMe,data.FriendedYou);
                }

            } while (true);
        }

        private static void GameTestWithTables()
        {
            LudoPlayer donatello = new LudoPlayer("Donatello");
            LudoPlayer raffaello = new LudoPlayer("Raffaello");
            LudoPlayer michalangelo = new LudoPlayer("Michalangelo");
            LudoPlayer leonadro = new LudoPlayer("Leonadro");

            LudoTable table = new LudoTable(donatello, "asztal", "pw");

            table.AddPlayer(raffaello, "pw");
            table.AddPlayer(michalangelo, "pw");
            table.AddPlayer(leonadro, "pw");

            table.SetColor(donatello, puppetColor.Blue);
            table.SetColor(michalangelo, puppetColor.Yellow);
            table.SetColor(leonadro, puppetColor.Green);
            table.SetColor(raffaello, puppetColor.Red);

            table.SetCheck(donatello, true);
            table.SetCheck(raffaello, true);
            table.SetCheck(michalangelo, true);
            table.SetCheck(leonadro, true);

            table.Start();
                do
                {
                try
                {
                    
                    LudoGameManager manager = (LudoGameManager)table.Gamemanager;
                    WriteOutGame(table.Gamemanager.getGame());
                    Console.WriteLine("dices: {0} - {1}", manager.Dice1, manager.Dice2);
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("throw/move/check ? (t/m/c):");
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "c":
                            table.Gamemanager.DoAction(new CheckLudoAction(manager.getGame().Nextplayer));
                            break;
                        case "t":
                            table.Gamemanager.DoAction(new ThrowLudoAction(manager.getGame().Nextplayer));
                            break;
                        case "m":
                            Console.WriteLine("puppet?:");
                            int puppet = int.Parse(Console.ReadLine());
                            Console.WriteLine("amount?:");
                            int amount = int.Parse(Console.ReadLine());
                            table.Gamemanager.DoAction(new MoveLudoAction(table.Gamemanager.getGame().Nextplayer, puppet, amount));
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                } while (true);
        }

        private static void GameTest()
        {
            LudoPlayer player1 = new LudoPlayer("Elso", puppetColor.Blue, 0, 0, 0, 0, 1);
            LudoPlayer player2 = new LudoPlayer("Masodik", puppetColor.Yellow, 0, 0, 0, 0, 2);
            LudoPlayer player3 = new LudoPlayer("Harmadik", puppetColor.Green, 0, 0, 0, 0, 3);
            LudoPlayer player4 = new LudoPlayer("Negyedik", puppetColor.Red, 0, 0, 0, 0, 4);

            LudoGame game = new LudoGame(player1, player2, player3, player4, new DateTime());

            LudoGameManager manager = new LudoGameManager(game);

            do
            {
                Console.WriteLine("throw/move ?:");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "throw":
                        manager.DoAction(new ThrowLudoAction(manager.getGame().Nextplayer));
                        Console.WriteLine("dices:\t" + manager.Dice1 + "\t" + manager.Dice2);
                        WriteOutGame(manager.getGame());
                        break;
                    case "move":
                        Console.WriteLine("puppet?:");
                        int puppet = int.Parse(Console.ReadLine());
                        Console.WriteLine("amount?: {0} or {1}", manager.Dice1, manager.Dice2);
                        int amount = int.Parse(Console.ReadLine());
                        manager.DoAction(new MoveLudoAction(manager.getGame().Nextplayer, puppet, amount));
                        WriteOutGame(manager.getGame());
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        private static void WriteOutGame(LudoGame ludoGame)
        {
            Console.WriteLine("---------------------------------------");
            foreach (var players in ludoGame.Players)
            {
                Console.WriteLine(players.Name + "    " + players.color);
                foreach (var item in CountPuppets(players) )
                {
                    Console.Write(item+"\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("nextplayer: " + ludoGame.Nextplayer.Name);
            Console.WriteLine("rounds: " + ludoGame.Rounds);
        }

        private static List<int> CountPuppets(LudoPlayer player)
        {
            List<int> puppets = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                switch (player.color)
                {
                    case puppetColor.Red:
                        if (player.Puppets[i] > 0 && player.Puppets[i] < 41) puppets.Add((player.Puppets[i] + 0) % 40);
                        else puppets.Add(player.Puppets[i]);
                        break;
                    case puppetColor.Yellow:
                        if (player.Puppets[i] > 0 && player.Puppets[i] < 41) puppets.Add((player.Puppets[i] + 20) % 40);
                        else puppets.Add(player.Puppets[i]);
                        break;
                    case puppetColor.Blue:
                        if (player.Puppets[i] > 0 && player.Puppets[i] < 41) puppets.Add((player.Puppets[i] + 10) % 40);
                        else puppets.Add(player.Puppets[i]);
                        break;
                    case puppetColor.Green:
                        if (player.Puppets[i] > 0 && player.Puppets[i] < 41) puppets.Add((player.Puppets[i] + 30) % 40);
                        else puppets.Add(player.Puppets[i]);
                        break;
                    case puppetColor.Default:
                        break;
                    default:
                        break;
                }
            }
            return puppets;
        }

        private static void Repotest()
        {

            UsersRepository userrepo = new UsersRepository();
            Console.WriteLine("User:");
            foreach (var item in userrepo.GetAll())
            {
                Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t {5}\t {6}\t {7}", item.UserID, item.Username, item.Password, item.EmailID, item.Status, item.Token,item.Role,item.Guid);
            }
            Console.WriteLine("------------------------------------------");

            FriendConnectionsRepository friendrepo = new FriendConnectionsRepository();
            Console.WriteLine("FriendConnections:");
            foreach (var item in friendrepo.GetAll())
            { 
                Console.WriteLine("{0}\t {1}\t {2}\t",item.FriendConnID,item.UserID, item.FriendUserID);
            }
            
            Console.WriteLine("------------------------------------------");

            

            Console.ReadLine();
        }
    }
}
