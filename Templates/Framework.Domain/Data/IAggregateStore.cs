using System;
using System.Threading.Tasks;
using $safeprojectname$.BaseModels;

namespace $safeprojectname$.Data
{
    public interface IAggregateStore
    {
        Task<bool> Exists<T, TId>(TId aggregateId);

        Task Save<T, TId>(T aggregate)
             where TId : IEquatable<TId>
             where T : BaseAggregateRoot<TId>;

        Task<T> Load<T, TId>(TId aggregateId)
             where TId : IEquatable<TId>
             where T : BaseAggregateRoot<TId>;
    }
}