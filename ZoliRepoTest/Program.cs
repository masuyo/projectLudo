using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using Repository;

namespace ZoliRepoTest
{

    //TODO nem mentődik el az insertált adat az adatbázisban 
    class Program
    {
        static Repository.TableRepositories.UsersRepository repo;

        static void Main(string[] args)
        {
            DatabaseEntities DE = new DatabaseEntities();
            repo = new Repository.TableRepositories.UsersRepository(DE);

            //repo.Insert(new User() {id=2,name = "nev1", pass = "asd", email = "mail", status = 0, token = 0 });

            //foreach (var item in repo.GetAll())
            //{
            //    Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t {5}\t",item.id,item.name,item.pass,item.email,item.status,item.token);
            //}

            Console.Read();

            //ude.Users.Add(u);
            //ude.SaveChanges();
        }
    }
}
