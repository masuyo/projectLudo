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

        public override User GetById(int id)
        {
            return Get(akt => akt.UserID == id).SingleOrDefault();
        }

        public User GetByName(string name)
        {
            return Get(akt => akt.Username == name).SingleOrDefault();
        }

        public User GetByEmailID(string emailid)
        {
            return Get(akt => akt.EmailID == emailid).SingleOrDefault();
        }

        public User GetByGuid(string guid)
        {
            return Get(akt => akt.Guid == guid).SingleOrDefault();
        }

        public void UpdateName(int userid,string newname)
        {
            var sql = @"update [User] set Username=@newname where UserID=@userid";
            SqlParameter id = new SqlParameter("@userid", userid);
            SqlParameter name = new SqlParameter("@newname", newname);
            context.Database.ExecuteSqlCommand(sql, id,name);
            context.SaveChanges();
        }

        public void UpdateEmailID(int userid, string newemailid)
        {
            var sql = @"update [User] set EmailID=@newemailid where UserID=@userid";
            SqlParameter id = new SqlParameter("@userid", userid);
            SqlParameter emailid = new SqlParameter("@newemailid", newemailid);
            context.Database.ExecuteSqlCommand(sql, id, emailid);
            context.SaveChanges();
        }

        public void UpdateRole(int userID, string role)
        {
            var sql = @"update [User] set Role=@role where UserID=@userID";
            SqlParameter id = new SqlParameter("@userid", userID);
            SqlParameter newrole = new SqlParameter("@role", role);
            context.Database.ExecuteSqlCommand(sql, id, newrole);
            context.SaveChanges();
        }

        public void UpdatePassword(int userid, string newpassword)
        {
            var sql = @"update [User] set Password=@newpassword where UserID=@userid";
            SqlParameter id = new SqlParameter("@userid", userid);
            SqlParameter password = new SqlParameter("@newpassword", newpassword);
            context.Database.ExecuteSqlCommand(sql, id, password);
            context.SaveChanges();
        }


        public override void Insert(User newentity)
        {
            var sql = @"insert into [User](Username,Password,EmailID,Status,Token,Guid,Role) values(@username,@password,@email,@status,@token,@guid,@role)";
            SqlParameter username = new SqlParameter("@username", newentity.Username);
            SqlParameter password = new SqlParameter("@password", newentity.Password);
            SqlParameter emailid = new SqlParameter("@email", newentity.EmailID);
            SqlParameter status = new SqlParameter("@status", newentity.Status);
            SqlParameter token = new SqlParameter("@token", newentity.Token);
            SqlParameter guid = new SqlParameter("@guid", newentity.Guid);
            SqlParameter role = new SqlParameter("@role", newentity.Role);
            context.Database.ExecuteSqlCommand(sql,username,password,emailid,status,token,guid,role);
            context.SaveChanges();
        }
    }
}
