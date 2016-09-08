using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Data.Entity;

namespace Repository.TableRepositories
{
    public class InvationDesktopRepository : EFRepository<InvationDesktop>
    {

        public override InvationDesktop GetById(int id)
        {
            return Get(akt =>akt.InvID==id ).SingleOrDefault();
        }
    }
}
