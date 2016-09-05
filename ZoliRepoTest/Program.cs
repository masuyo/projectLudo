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

namespace ZoliRepoTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //Repotest();
            //GameTest();
            GameTestWithTables();
            //MVCServiceTest();

        }

        private static void MVCServiceTest()
        {
            MVCService service = new MVCService();
            service.Register("Bela", "asdasd", "email@email.com");

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
                    LudoGameManager manager = (LudoGameManager)table.Gamemanager;
                    Console.WriteLine("throw/move ?:");
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "throw":
                            table.Gamemanager.DoAction(new ThrowLudoAction(manager.getGame().nextplayer));
                            Console.WriteLine("dices:\t" + manager.Dice1 + "\t" + manager.Dice2);
                            WriteOutGame(manager.getGame());
                            break;
                        case "move":
                            Console.WriteLine("puppet?:");
                            int puppet = int.Parse(Console.ReadLine());
                            Console.WriteLine("amount?: {0} or {1}", manager.Dice1, manager.Dice2);
                            int amount = int.Parse(Console.ReadLine());
                            table.Gamemanager.DoAction(new MoveLudoAction(table.Gamemanager.getGame().nextplayer, puppet, amount));
                            WriteOutGame(table.Gamemanager.getGame());
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

            foreach (var players in ludoGame.Players)
            {
                Console.WriteLine(players.Name + "    " + players.color);
                Console.WriteLine("\t" + players.Puppets[0] + players.Puppets[1] + players.Puppets[2] + players.Puppets[3]);
            }
            Console.WriteLine("nextplayer: " + ludoGame.nextplayer.Name);
            Console.WriteLine("rounds: " + ludoGame.Rounds);
        }

        private static void Repotest()
        {
            DatabaseEntities DE = new DatabaseEntities();
            Repository.TableRepositories.UsersRepository repo = new Repository.TableRepositories.UsersRepository(DE);

            //try
            //{
            //    repo.Register("asd", "asd123", "asd@email.com");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //repo.Delete(6);

            foreach (var item in repo.GetAll())
            {
                Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t {5}\t", item.UserID, item.Username, item.Password, item.EmailID, item.Status, item.Token);
            }

            DE.SaveChanges();

            Console.Read();
        }
    }
}
