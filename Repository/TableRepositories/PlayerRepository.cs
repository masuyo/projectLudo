using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Data.Entity;

namespace Repository.TableRepositories
{
    public class PlayerRepository : EFRepository<Player>
    {

        public override Player GetById(int id)
        {
           return Get(akt =>akt.PlayerID==id ).SingleOrDefault();
        }
    }
}
