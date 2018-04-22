using Chatter.Domain.Core.Models;
using Chatter.Domain.Interfaces;
using Chatter.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Chatter.Infra.Data.Repository.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>, new()
    {
        protected ChatterContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(ChatterContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual TEntity Get(int id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public virtual IEnumerable<TEntity> GetAll(bool activeOnly = false)
        {
            return DbSet.ToList();
        }

        public virtual void Remove(int id)
        {
            var entity = new TEntity {Id = id};
            DbSet.Remove(entity);
        }

        public virtual int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}