using Microsoft.EntityFrameworkCore;
using Onix.Framework.Infra.Data.Implementation;
using Onix.Framework.Infra.Data.Implementation.Helpers;
using Onix.Framework.Infra.Data.Interfaces;
using Onix.Framework.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Onix.Framework.Infra.Data.EFCore
{
    public class EFCoreReadRepository<TAggregateRoot>(DbContext dbContext) : IRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        private readonly DbContext _dbContext = dbContext;

        protected virtual bool Any<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class
        {
            var query = Query(predicate, false);
            return query.Any();
        }

        protected virtual bool Any<TEntity>(IQueryable<TEntity> query)
            where TEntity : class
        {
            return query.Any();
        }

        protected virtual async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class
        {
            var query = Query(predicate, false);
            return await query.AnyAsync();
        }

        protected virtual async Task<bool> AnyAsync<TEntity>(IQueryable<TEntity> query)
            where TEntity : class
        {
            return await query.AnyAsync();
        }

        protected virtual int Count<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class
        {
            var query = Query(predicate, false);
            return query.Count();
        }

        protected virtual int Count<TEntity>(IQueryable<TEntity> query)
            where TEntity : class
        {
            return query.Count();
        }

        protected virtual async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class
        {
            var query = Query(predicate, false);
            return await query.CountAsync();
        }

        protected virtual async Task<int> CountAsync<TEntity>(IQueryable<TEntity> query)
            where TEntity : class
        {
            return await query.CountAsync();
        }

        protected virtual void Detache<TEntity>(TEntity entity) 
            where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        protected virtual TEntity FindById<TEntity, T>(T key)
            where TEntity : class
        {
            return DbSet<TEntity>().Find(key);
        }

        protected virtual async Task<TEntity> FindByIdAsync<TEntity, T>(T key)
            where TEntity : class
        {
            return await DbSet<TEntity>().FindAsync(key);
        }

        protected virtual TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            var query = Query(predicate, trackChanges, include);
            return query.FirstOrDefault();
        }

        protected virtual TEntity FirstOrDefault<TEntity>(IQueryable<TEntity> query, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            query = trackChanges ? query : query.AsNoTracking();
            query = Include(query, include);
            return query.FirstOrDefault();
        }

        protected virtual async Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            var query = Query(predicate, trackChanges, include);
            return await query.FirstOrDefaultAsync();
        }

        protected virtual async Task<TEntity> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            query = trackChanges ? query : query.AsNoTracking();
            query = Include(query, include);
            return await query.FirstOrDefaultAsync();
        }

        protected virtual IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            var query = Query(predicate, trackChanges, include);
            return query.ToList();
        }

        protected virtual IEnumerable<TEntity> Get<TEntity>(IQueryable<TEntity> query, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            query = trackChanges ? query : query.AsNoTracking();
            query = Include(query, include);
            return query.ToList();
        }

        protected virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            var query = Query(predicate, trackChanges, include);
            return await query.ToListAsync();
        }

        protected virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(IQueryable<TEntity> query, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            query = trackChanges ? query : query.AsNoTracking();
            query = Include(query, include);
            return await query.ToListAsync();
        }

        protected virtual IPagedItems<TEntity> GetPaged<TEntity>(IPaged paged, IQueryable<TEntity> query = null) where TEntity : class
        {
            query ??= Query<TEntity>();
            var totalItems = Count(query);
            var items = DataHelper.Page(query, paged).ToList();
            return new PagedItems<TEntity>(paged, items, totalItems);
        }

        protected virtual async Task<IPagedItems<TEntity>> GetPagedAsync<TEntity>(IPaged paged, IQueryable<TEntity> query = null) where TEntity : class
        {
            query ??= Query<TEntity>();
            var totalItems = await CountAsync(query);
            var items = await DataHelper.Page(query, paged).ToListAsync();
            return new PagedItems<TEntity>(paged, items, totalItems);
        }

        protected virtual IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool trackChanges = false, params string[] include)
            where TEntity : class
        {
            var dbSet = DbSet<TEntity>();
            var query = predicate == null ? dbSet : dbSet.Where(predicate);
            query = trackChanges ? query : query.AsNoTracking();
            return Include(query, include);
        }

        protected virtual IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> query, params string[] include)
            where TEntity : class
        {
            if ((include?.Any()).GetValueOrDefault())
            {
                foreach (var includeItem in include)
                {
                    query = query.Include(includeItem);
                }
            }
            return query;
        }

        protected virtual DbSet<TEntity> DbSet<TEntity>()
            where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }
    }
}