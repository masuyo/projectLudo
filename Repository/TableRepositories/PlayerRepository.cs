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
    public class PlayerRepository : EFRepository<Player>
    {

        public override Player GetById(int id)
        {
           return Get(akt =>akt.PlayerID==id ).SingleOrDefault();
        }

        public override void Insert(Player newentity)
        {
            var sql = @"insert into [Player](UserID,GameID,IsWinner,Dice1,Dice2,Status,Color)values(@userid,@gameid,@iswinner,@dice1,@dice2,@status,@color)";
            SqlParameter userid = new SqlParameter("@userid", newentity.UserID);
            SqlParameter gameid = new SqlParameter("@gameid", newentity.GameID);
            SqlParameter iswinner = new SqlParameter("@iswinner", newentity.IsWinner);
            SqlParameter dice1 = new SqlParameter("@dice1", newentity.Dice1);
            SqlParameter dice2 = new SqlParameter("@dice2", newentity.Dice2);
            SqlParameter status = new SqlParameter("@status", newentity.Status);
            SqlParameter color = new SqlParameter("@color", newentity.Color);

            context.Database.ExecuteSqlCommand(sql, userid, gameid, iswinner, dice1, dice2, status, color);
            context.SaveChanges();
        }
    }
}
