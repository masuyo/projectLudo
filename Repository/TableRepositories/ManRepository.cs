using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Data.Entity;

namespace Repository.TableRepositories
{
    public class ManRepository : EFRepository<Man>
    { 
        public override Man GetById(int id)
        {
            return Get(akt =>akt.ManID==id ).SingleOrDefault();
        }
    }
}
