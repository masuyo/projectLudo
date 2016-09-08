using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Data.Entity;

namespace Repository.TableRepositories
{
    public class FriendConnectionsRepository : EFRepository<FriendConnections>
    {

        public override FriendConnections GetById(int id)
        {
           return Get(akt =>akt.FriendConnID==id).SingleOrDefault();
        }
    }
}
