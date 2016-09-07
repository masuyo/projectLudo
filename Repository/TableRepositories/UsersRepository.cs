using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Data.SqlClient;

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

        public List<User> GetByName(string name)
        {
            return Get(akt => akt.Username == name).ToList();
        }

        public User GetByEmailID(string emailid)
        {
            return Get(akt => akt.EmailID == emailid).SingleOrDefault();
        }

        public bool Register(string Username,string Password,string EmailID)
        {
            if (GetByName(Username) != null) return false;
            if (GetByEmailID(EmailID) != null) return false;
            Insert(new User() { Username = Username, Password = Password, EmailID = EmailID });

            return true; 
        }

        public override void  Insert(User newentity)
        {
            var sql = @"insert into [User](Username,Password,EmailID,Status,Token) values(@username,@password,@email,'off','token')";
            context.Database.ExecuteSqlCommand(sql,new SqlParameter("@username",newentity.Username), new SqlParameter("@password", newentity.Password), new SqlParameter("@email", newentity.EmailID));
        }
    }
}
