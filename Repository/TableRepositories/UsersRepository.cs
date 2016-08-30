using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace Repository.TableRepositories
{
    public class UsersRepository : EFRepository<User>
    {
        public UsersRepository(DbContext newctx) : base(newctx)
        {
        }

        public override User GetById(int id)
        {
            return Get(akt => akt.UserID == id).SingleOrDefault();
        }

        public User GetByName(string name)
        {
            //checkolom
            //ha van akkor beállítom aktívra
            //return sikerült/nem sikerült
            return Get(akt => akt.Username == name).SingleOrDefault();
        }

        public bool Register(string Username,string Password,string EmailID)
        {
            //leellenőrzöm
            //if (Get(akt => akt.Username == Username && akt.EmailID == EmailID) != null) throw new Exception();
            //insertálom
            Insert(new User() {UserID=1, Username = Username, Password = Password, EmailID = EmailID });

            return true; //sikerült? 
        }
    }
}
