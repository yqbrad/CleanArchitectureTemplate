using DDD.Contracts._Base;
using Framework.Domain.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace $safeprojectname$._Base
{
    public class BaseRepository<TEntity, TId, TDbContext>: IRepository<TEntity, TId>
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

        public void Insert(List<TEntity> entities)
            => DbContext.Set<TEntity>().AddRange(entities);

        public async Task InsertAsync(List<TEntity> entities)
            => await DbContext.Set<TEntity>().AddRangeAsync(entities);

        public void Update(TEntity entity)
            => DbContext.Set<TEntity>().Update(entity);

        public void Update(List<TEntity> entities)
            => DbContext.Set<TEntity>().UpdateRange(entities);

        public void Delete(TId id)
        {
            var entity = DbContext.Set<TEntity>().Find(id);
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(TEntity entity)
            => DbContext.Set<TEntity>().Remove(entity);

        public void Delete(List<TEntity> entities)
            => DbContext.Set<TEntity>().RemoveRange(entities);

        public TEntity Get(TId id)
            => DbContext.Set<TEntity>().Find(id);

        public async Task<TEntity> GetAsync(TId id)
            => await DbContext.Set<TEntity>().FindAsync(id);

        public List<TEntity> GetAll()
            => DbContext.Set<TEntity>().ToList();

        public async Task<List<TEntity>> GetAllAsync()
            => await DbContext.Set<TEntity>().ToListAsync();

        public bool Exists(Expression<Func<TEntity, bool>> expression)
            => DbContext.Set<TEntity>().Any(expression);

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
            => DbContext.Set<TEntity>().AnyAsync(expression);
    }
}