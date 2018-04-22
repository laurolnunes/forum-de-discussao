using System;
using System.Linq;
using Chatter.Domain.Users;
using Chatter.Domain.Users.Repository;
using Chatter.Infra.Data.Context;
using Chatter.Infra.Data.Repository.Base;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infra.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChatterContext context) : base(context)
        {
        }

        public int GetIdByIdentityId(Guid id)
        {
            const string query = @"SELECT TOP 1 Id FROM Users WHERE IdentityId = @_identityId";
            return Db.Database.GetDbConnection().ExecuteScalar<int>(query, new { _identityId = id });
        }

        public override void Remove(int id)
        {
            var user = Get(id);
            user.SetRemoved();
            Update(user);
        }
    }
}