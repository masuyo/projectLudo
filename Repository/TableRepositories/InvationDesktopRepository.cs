using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Repository.TableRepositories
{
    public class InvationDesktopRepository : EFRepository<InvationDesktop>
    {

        public override InvationDesktop GetById(int id)
        {
            return Get(akt =>akt.InvID==id ).SingleOrDefault();
        }



        public override void Insert(InvationDesktop newentity)
        {
            var sql = @"insert into[InvationDesktop](AdminUserID,Dname,Dpass)values(@adminuserid,@dname,@dpass)";
            SqlParameter adminuserid = new SqlParameter("@adminuserid",newentity.AdminUserID);
            SqlParameter dname = new SqlParameter("dname", newentity.DName);
            SqlParameter dpass = new SqlParameter("dpass", newentity.DPass);

            context.Database.ExecuteSqlCommand(sql, adminuserid, dname, dpass);
            context.SaveChanges();
        }
    }
}
