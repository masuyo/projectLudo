﻿using System;
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

        public List<User> GetByName(string name)
        {
            return Get(akt => akt.Username == name).ToList();
        }

        public User GetByEmailID(string emailid)
        {
            return Get(akt => akt.EmailID == emailid).SingleOrDefault();
        }

        public override void Insert(User newentity)
        {
            var sql = @"insert into [User](Username,Password,EmailID,Status,Token,Guid) values(@username,@password,@email,'off','token','guid')";
            context.Database.ExecuteSqlCommand(sql, new SqlParameter("@username", newentity.Username), new SqlParameter("@password", newentity.Password), new SqlParameter("@email", newentity.EmailID));
            context.SaveChanges();
        }
    }
}
