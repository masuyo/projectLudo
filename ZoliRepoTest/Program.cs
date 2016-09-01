using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

using Entities;
using Repository;

namespace ZoliRepoTest
{
    class Program
    {
        static Repository.TableRepositories.UsersRepository repo;

        static void Main(string[] args)
        {

            //Repotest();
            GameTest();
            
        }

        private static void GameTest()
        {
            LudoPlayer player1 = new LudoPlayer("Elso", puppetColor.Blue,0,0,0,0,1);
            LudoPlayer player2 = new LudoPlayer("Masodik", puppetColor.Yellow,0,0,0,0,2);
            LudoPlayer player3 = new LudoPlayer("Harmadik", puppetColor.Green,0,0,0,0,3);
            LudoPlayer player4 = new LudoPlayer("Negyedik", puppetColor.Red,0,0,0,0,4);

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
                        Console.WriteLine("dices:\t"+manager.dice1+"\t"+manager.dice2);
                        WriteOutGame(manager.getGame());
                        break;
                    case "move":
                        Console.WriteLine("puppet?:");
                        int puppet = int.Parse(Console.ReadLine());
                        Console.WriteLine("amount?: {0} or {1}",manager.dice1,manager.dice2);
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
                Console.WriteLine(players.Name+"    "+players.color);
                Console.WriteLine("\t"+players.puppet1position+ players.puppet2position + players.puppet3position + players.puppet4position);
            }
            Console.WriteLine("nextplayer: "+ludoGame.nextplayer.Name);
            Console.WriteLine("rounds: "+ludoGame.Rounds);
        }

        private static void Repotest()
        {
            DatabaseEntities DE = new DatabaseEntities();
            repo = new Repository.TableRepositories.UsersRepository(DE);

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
