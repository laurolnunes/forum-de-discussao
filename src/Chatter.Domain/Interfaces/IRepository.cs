using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chatter.Domain.Core.Models;

namespace Chatter.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        TEntity Add(TEntity entity);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll(bool activeOnly = false);
        void Update(TEntity obj);
        void Remove(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}