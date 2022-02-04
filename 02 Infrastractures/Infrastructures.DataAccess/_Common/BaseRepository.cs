using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DDD.Contracts._Common;
using Framework.Domain.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.DataAccess._Common
{
    public class BaseRepository<TEntity, TId, TDbContext> : IRepository<TEntity, TId>
        where TEntity : BaseAggregateRoot<TId>
        where TId : IEquatable<TId>
        where TDbContext : BaseDbContext
    {
        internal readonly TDbContext DbContext;
        public BaseRepository(TDbContext dbContext)
            => DbContext = dbContext;

        public TId CreateId()
            => DbContext.GetNextSequenceValue<TId, TEntity>();

        public Task<TId> CreateIdAsync()
            => DbContext.GetNextSequenceValueAsync<TId, TEntity>();

        public void Insert(TEntity entity)
            => DbContext.Set<TEntity>().Add(entity);

        public async Task InsertAsync(TEntity entity)
            => await DbContext.Set<TEntity>().AddAsync(entity);

        public void Insert(IEnumerable<TEntity> entities)
            => DbContext.Set<TEntity>().AddRange(entities);

        public async Task InsertAsync(IEnumerable<TEntity> entities)
            => await DbContext.Set<TEntity>().AddRangeAsync(entities);

        public void Update(TEntity entity)
            => DbContext.Set<TEntity>().Update(entity);

        public void Update(IEnumerable<TEntity> entities)
            => DbContext.Set<TEntity>().UpdateRange(entities);

        public void Delete(TId id)
        {
            var entity = DbContext.Set<TEntity>().Find(id);
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(TEntity entity)
            => DbContext.Set<TEntity>().Remove(entity);

        public void Delete(IEnumerable<TEntity> entities)
            => DbContext.Set<TEntity>().RemoveRange(entities);

        public TEntity Get(TId id)
            => DbContext.Set<TEntity>().Find(id);

        public TEntity Get(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
            => ApplyIncludesOnQuery(DbContext.Set<TEntity>(), includeProperties).FirstOrDefault();

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
            => DbContext.Set<TEntity>().Where(predicate).SingleOrDefault();

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = DbContext.Set<TEntity>().Where(predicate);
            return ApplyIncludesOnQuery(query, includeProperties).SingleOrDefault();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => DbContext.Set<TEntity>().Where(predicate).ToList();

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = DbContext.Set<TEntity>().Where(predicate);
            return ApplyIncludesOnQuery(query, includeProperties).ToList();
        }

        public async Task<TEntity> GetAsync(TId id)
            => await DbContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
            => await ApplyIncludesOnQuery(DbContext.Set<TEntity>(), includeProperties).FirstOrDefaultAsync();

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
            => DbContext.Set<TEntity>().Where(predicate).SingleOrDefaultAsync();

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = DbContext.Set<TEntity>().Where(predicate);
            return ApplyIncludesOnQuery(query, includeProperties).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await DbContext.Set<TEntity>().Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = DbContext.Set<TEntity>().Where(predicate);
            return await ApplyIncludesOnQuery(query, includeProperties).ToListAsync();
        }

        public IEnumerable<TEntity> GetAll()
            => DbContext.Set<TEntity>().ToList();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await DbContext.Set<TEntity>().ToListAsync();

        public bool Exists(Expression<Func<TEntity, bool>> expression)
            => DbContext.Set<TEntity>().Any(expression);

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
            => DbContext.Set<TEntity>().AnyAsync(expression);

        private IQueryable<TEntity> ApplyIncludesOnQuery(IQueryable<TEntity> query,
            params Expression<Func<TEntity, object>>[] includeProperties)
            => includeProperties.Aggregate(query, (current, include)
                => current.Include(include));
    }
}