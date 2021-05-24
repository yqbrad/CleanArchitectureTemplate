using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Domain.BaseModels;

namespace $safeprojectname$._Base
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseAggregateRoot<TId>
        where TId : IEquatable<TId>
    {
        TId CreateId();
        Task<TId> CreateIdAsync();

        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Insert(List<TEntity> entities);
        Task InsertAsync(List<TEntity> entities);

        void Update(TEntity entity);
        void Update(List<TEntity> entities);

        void Delete(TId id);
        void Delete(TEntity entity);
        void Delete(List<TEntity> entities);

        TEntity Get(TId id);
        Task<TEntity> GetAsync(TId id);

        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();

        bool Exists(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}