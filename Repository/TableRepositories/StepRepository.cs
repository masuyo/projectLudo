using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Data.Entity;

namespace Repository.TableRepositories
{
    public class StepRepository : EFRepository<Step>
    {
        public StepRepository(DbContext newctx) : base(newctx)
        {
        }

        public override Step GetById(int id)
        {
            return Get(akt => akt.StepID==id).SingleOrDefault();
        }
    }
}
