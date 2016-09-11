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
    public class GameRepository : EFRepository<Game>
    {

        public override Game GetById(int id)
        {
            return Get(akt =>akt.GameID==id ).SingleOrDefault();
        }

        public override void Insert(Game newentity)
        {
            var sql = @"insert into [Game](Start,Turns)values(@start,@turns)";
            SqlParameter start = new SqlParameter("@start", newentity.Start);
            SqlParameter turns = new SqlParameter("@turns", newentity.Turns);

            context.Database.ExecuteSqlCommand(sql, start, turns);
            context.SaveChanges();
        }

        public void PlusTurn(int gameid)
        {
            var sql = @"update [Game] set Turns=Turns+1 where GameID=@gameid";
            SqlParameter id = new SqlParameter("@gameid", gameid);
            context.Database.ExecuteSqlCommand(sql, id);
            context.SaveChanges();
        }

        public void End(int gameid,DateTime time)
        {
            var sql = @"update [Game] set End=@end where GameID=@gameid";
            SqlParameter id = new SqlParameter("@gameid", gameid);
            SqlParameter t = new SqlParameter("@end", time);
            context.Database.ExecuteSqlCommand(sql, id, t);
            context.SaveChanges();
        }
    }
}
