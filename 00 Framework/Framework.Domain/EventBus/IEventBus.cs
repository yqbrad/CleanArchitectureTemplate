using System;
using System.Threading.Tasks;
using Framework.Domain.BaseModels;

namespace Framework.Domain.EventBus
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