using DDD.Contracts._Common;
using Framework.Domain.EventBus;

namespace DDD.ApplicationServices._Common
{
    public abstract class BaseApplicationService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IEventBus EventBus;

        protected BaseApplicationService(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            UnitOfWork = unitOfWork;
            EventBus = eventBus;
        }
    }
}