using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using Repository;

namespace ZoliRepoTest
{
    class Program
    {
        static Repository.TableRepositories.UsersRepository repo;

        static void Main(string[] args)
        {
            DatabaseEntities DE = new DatabaseEntities();
            repo = new Repository.TableRepositories.UsersRepository(DE);

            //repo.Register("asd", "asd123", "asd@email.com");
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
