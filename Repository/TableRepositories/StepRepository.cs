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
    public class StepRepository : EFRepository<Step>
    {

        public override Step GetById(int id)
        {
            return Get(akt => akt.StepID==id).SingleOrDefault();
        }

        public override void Insert(Step newentity)
        {
            var sql = @"insert into [Step](ManID,FromPoz,ToPoz,Time)values(@manid,@frompoz,@topoz,@time)";
            SqlParameter manid = new SqlParameter("@manid", newentity.ManID);
            SqlParameter frompoz = new SqlParameter("@frompoz", newentity.FromPoz);
            SqlParameter topoz = new SqlParameter("@topoz", newentity.ToPoz);
            SqlParameter time = new SqlParameter("@time", newentity.Time);

            context.Database.ExecuteSqlCommand(sql, manid, frompoz, topoz, time);
            context.SaveChanges();
        }
    }
}
