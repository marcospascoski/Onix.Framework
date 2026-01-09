using Microsoft.EntityFrameworkCore;
using Onix.Framework.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Onix.Framework.Infra.Data.EFCore
{
    public class EFCoreRepository<TAggregateRoot>(DbContext dbContext) : EFCoreReadRepository<TAggregateRoot>(dbContext)
        where TAggregateRoot : IAggregateRoot
    {
        protected virtual IQueryable<TEntity> Queryable<TEntity>()
            where TEntity : class
        {
            return DbSet<TEntity>().AsQueryable();
        }
        protected virtual void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            var dbSet = DbSet<TEntity>();
            dbSet.Add(entity);
        }

        protected  virtual async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = DbSet<TEntity>();
            await dbSet.AddAsync(entity);
        }

        protected  virtual void AddRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            var dbSet = DbSet<TEntity>();
            dbSet.AddRange(entities);
        }

        protected  virtual async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            var dbSet = DbSet<TEntity>();
            await dbSet.AddRangeAsync(entities);
        }

        protected  virtual void Remove<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity != null)
            {
                var dbSet = DbSet<TEntity>();
                dbSet.Remove(entity);
            }
        }

        protected  virtual void Remove<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            if (predicate != null)
            {
                var dbSet = DbSet<TEntity>();
                var entities = Get(predicate, true);
                if ((entities?.Any()).GetValueOrDefault())
                {
                    dbSet.RemoveRange(entities);
                }
            }
        }

        protected  virtual async Task RemoveAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            if (predicate != null)
            {
                var dbSet = DbSet<TEntity>();
                var entities = await GetAsync(predicate, true);
                if ((entities?.Any()).GetValueOrDefault())
                {
                    dbSet.RemoveRange(entities);
                }
            }
        }

        protected  virtual void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity != null)
            {
                var dbSet = DbSet<TEntity>();
                dbSet.Update(entity);
            }
        }
    }
}