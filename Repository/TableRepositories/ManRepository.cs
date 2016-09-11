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
    public class ManRepository : EFRepository<Man>
    { 
        public override Man GetById(int id)
        {
            return Get(akt =>akt.ManID==id ).SingleOrDefault();
        }

        public override void Insert(Man newentity)
        {
            var sql = @"insert into [Man](PlayerID,Pozition)values(@playerid,@pozition)";
            SqlParameter playerid = new SqlParameter("@playerid", newentity.PlayerID);
            SqlParameter pozition = new SqlParameter("@pozition", newentity.Pozition);

            context.Database.ExecuteSqlCommand(sql,playerid,pozition);
            context.SaveChanges();
        }
    }
}
