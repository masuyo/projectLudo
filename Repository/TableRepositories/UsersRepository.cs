using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace Repository.TableRepositories
{
    public class UsersRepository : EFRepository<users>
    {
        public UsersRepository(DbContext newctx) : base(newctx)
        {
        }

        public override users GetById(int id)
        {
            return Get(akt => akt.id == id).SingleOrDefault();
        }
    }
}
