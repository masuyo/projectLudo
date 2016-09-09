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
    public class FriendConnectionsRepository : EFRepository<FriendConnections>
    {

        public override FriendConnections GetById(int id)
        {
           return Get(akt =>akt.FriendConnID==id).SingleOrDefault();
        }

        public FriendConnections GetByUserIDs(int userid, int frienduserid)
        {
            return Get(akt => akt.UserID == userid && akt.FriendUserID == frienduserid).SingleOrDefault();
        }

        public override void Insert(FriendConnections newentity)
        {
            var sql = @"insert into [FriendConnections](UserID,FriendUserID) values(@userid,@frienduserid)";
            SqlParameter userid = new SqlParameter("@userid", newentity.UserID);
            SqlParameter frienduserid = new SqlParameter("@frienduserid", newentity.FriendUserID);

            context.Database.ExecuteSqlCommand(sql,userid,frienduserid);
            context.SaveChanges();
        }
    }
}
