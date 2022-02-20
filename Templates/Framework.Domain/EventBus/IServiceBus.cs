using System;
using System.Threading.Tasks;
using $safeprojectname$.BaseModels;

namespace $safeprojectname$.EventBus
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