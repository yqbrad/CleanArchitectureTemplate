using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Domain.BaseModels;

namespace YQB.Contracts._Common
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseAggregateRoot<TId>
        where TId : IEquatable<TId>
    {
        TId CreateId();
        Task<TId> CreateIdAsync();

        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        Task InsertAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        void Delete(TId id);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);

        TEntity Get(TId id);
        TEntity Get(TId id, params Expression<Func<TEntity, object>>[] includeProperties);
       
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetAsync(TId id);
        Task<TEntity> GetAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        bool Exists(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}