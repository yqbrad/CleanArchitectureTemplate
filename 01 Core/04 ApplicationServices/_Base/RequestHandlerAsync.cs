using DDD.Contracts._Base;
using Framework.Domain.ApplicationServices;
using Framework.Domain.EventBus;
using Framework.Domain.Requests;
using System.Threading.Tasks;

namespace DDD.ApplicationServices
{
    public abstract class RequestHandlerAsync : BaseApplicationService, IRequestHandler
    {
        protected RequestHandlerAsync(IUnitOfWork unitOfWork, IEventBus eventBus)
            : base(unitOfWork, eventBus) { }

        public abstract Task HandleAsync();
    }

    public abstract class RequestHandlerByInAsync<TRequest> : BaseApplicationService, IRequestHandler
        where TRequest : IRequest
    {
        protected RequestHandlerByInAsync(IUnitOfWork unitOfWork, IEventBus eventBus)
            : base(unitOfWork, eventBus) { }

        public abstract Task HandleAsync(TRequest req);
    }

    public abstract class RequestHandlerByOutAsync<TResult> : BaseApplicationService, IRequestHandler
        where TResult : class
    {
        protected RequestHandlerByOutAsync(IUnitOfWork unitOfWork, IEventBus eventBus)
            : base(unitOfWork, eventBus) { }

        public abstract Task<TResult> HandleAsync();
    }

    public abstract class RequestHandlerAsync<TIn, TResult> : BaseApplicationService, IRequestHandler
        where TIn : IRequest
        where TResult : class
    {
        protected RequestHandlerAsync(IUnitOfWork unitOfWork, IEventBus eventBus)
            : base(unitOfWork, eventBus) { }

        public abstract Task<TResult> HandleAsync(TIn req);
    }
}