using System;
using System.Threading.Tasks;
using Framework.Domain.BaseModels;

namespace Framework.Domain.EventBus
{
    public interface IServiceBus
    {
        public IUserInfo UserInfo { get; }

        //#region Event
        //Task PublishInternalAsync<TClass, TId>(TClass aggregateRoot)
        //    where TId : IEquatable<TId>
        //    where TClass : BaseAggregateRoot<TId>;

        //Task PublishExternalAsync<TClass, TId>(TClass aggregateRoot)
        //    where TId : IEquatable<TId>
        //    where TClass : BaseAggregateRoot<TId>;
        //#endregion
    }
}