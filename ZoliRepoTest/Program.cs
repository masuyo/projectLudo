using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SignalRServer;

//using Game;
using Entities;
using Repository;
using Game;
using Game.LudoActions;
using SignalRServer.MVCData.MethodClasses;
using Repository.TableRepositories;
using SignalRServer.MVCData.DataClasses;

namespace ZoliRepoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Repotest();
            FriendOfMeTest();

        }

        private static void FriendOfMeTest()
        {
            do
            {
                Console.WriteLine("seacrher and searched:");
                int searcher = int.Parse(Console.ReadLine());
                string searched = Console.ReadLine();

                UsersRepository userepo = new UsersRepository(new DatabaseEntities());

                UserActions useractioner = new UserActions();
                //UserData data= useractioner.EmaildIDSearch(userepo.GetById(searched).EmailID, userepo.GetById(searcher).EmailID);
                //Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t", data.Username,data.EmailID,data.AreWeFriends,data.FriendedMe,data.FriendedYou);

                foreach (var data in useractioner.UsernameSearch(searched, userepo.GetById(searcher).EmailID))
                {
                    Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t", data.Username,data.EmailID,data.AreWeFriends,data.FriendedMe,data.FriendedYou);
                }

            } while (true);
        }

        private static void MVCServiceTest()
        {
            Console.ReadLine();
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

            try
            {
                table.Start();
                do
                {
                    WriteOutGame(table.Gamemanager.getGame());
                    LudoGameManager manager = (LudoGameManager)table.Gamemanager;
                    Console.WriteLine("throw/move/check ?:");
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "check":
                            table.Gamemanager.DoAction(new CheckLudoAction(manager.getGame().nextplayer));
                            break;
                        case "throw":
                            table.Gamemanager.DoAction(new ThrowLudoAction(manager.getGame().nextplayer));
                            Console.WriteLine("dices:\t" + manager.Dice1 + "\t" + manager.Dice2);
                            break;
                        case "move":
                            Console.WriteLine("puppet?:");
                            int puppet = int.Parse(Console.ReadLine());
                            Console.WriteLine("amount?: {0} or {1}", manager.Dice1, manager.Dice2);
                            int amount = int.Parse(Console.ReadLine());
                            table.Gamemanager.DoAction(new MoveLudoAction(table.Gamemanager.getGame().nextplayer, puppet, amount));
                            break;
                        default:
                            break;
                    }
                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.Read();
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
                        manager.DoAction(new ThrowLudoAction(manager.getGame().nextplayer));
                        Console.WriteLine("dices:\t" + manager.Dice1 + "\t" + manager.Dice2);
                        WriteOutGame(manager.getGame());
                        break;
                    case "move":
                        Console.WriteLine("puppet?:");
                        int puppet = int.Parse(Console.ReadLine());
                        Console.WriteLine("amount?: {0} or {1}", manager.Dice1, manager.Dice2);
                        int amount = int.Parse(Console.ReadLine());
                        manager.DoAction(new MoveLudoAction(manager.getGame().nextplayer, puppet, amount));
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
                Console.WriteLine("\t" + players.Puppets[0] + players.Puppets[1] + players.Puppets[2] + players.Puppets[3]);
            }
            Console.WriteLine("nextplayer: " + ludoGame.nextplayer.Name);
            Console.WriteLine("rounds: " + ludoGame.Rounds);
            Console.WriteLine("---------------------------------------");
        }

        private static void Repotest()
        {
            DatabaseEntities DE = new DatabaseEntities();
            Repository.TableRepositories.UsersRepository userrepo = new Repository.TableRepositories.UsersRepository(DE);
            Console.WriteLine("User:");
            foreach (var item in userrepo.GetAll())
            {
                Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t {5}\t", item.UserID, item.Username, item.Password, item.EmailID, item.Status, item.Token);
            }
            Console.WriteLine("------------------------------------------");

            Repository.TableRepositories.FriendConnectionsRepository friendrepo = new Repository.TableRepositories.FriendConnectionsRepository(DE);
            Console.WriteLine("FriendConnections:");
            foreach (var item in friendrepo.GetAll())
            {
                Console.WriteLine("{0}\t {1}\t {2}\t",item.FriendConnID,item.UserID, item.FriendUserID);
            }
            Console.WriteLine("------------------------------------------");

            DE.SaveChanges();

            Console.ReadLine();
        }
    }
}
