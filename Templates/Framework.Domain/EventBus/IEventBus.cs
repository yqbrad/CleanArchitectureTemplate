using System;
using System.Threading.Tasks;
using $safeprojectname$.BaseModels;

namespace $safeprojectname$.EventBus
{
    public interface IEventBus
    {
        public IApplicant Applicant { get; }

        ApiServices GetApiServices();

        #region Event
        Task SubscribeInternalAsync<TClass, TId>(TClass aggregateRoot)
            where TId : IEquatable<TId>
            where TClass : BaseAggregateRoot<TId>;

        Task SubscribeExternalAsync<TClass, TId>(TClass aggregateRoot)
            where TId : IEquatable<TId>
            where TClass : BaseAggregateRoot<TId>;
        #endregion
    }
}