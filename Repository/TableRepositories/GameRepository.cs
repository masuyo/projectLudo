﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Data.Entity;

namespace Repository.TableRepositories
{
    public class GameRepository : EFRepository<Game>
    {
        public GameRepository(DbContext newctx) : base(newctx)
        {
        }

        public override Game GetById(int id)
        {
            return Get(akt =>akt.GameID==id ).SingleOrDefault();
        }
    }
}
