using DDD.Contracts._Base;
using Framework.Domain.ApplicationServices;
using Framework.Domain.EventBus;
using Framework.Domain.Requests;

namespace $safeprojectname$
{
    public abstract class RequestHandler : BaseApplicationService, IRequestHandler
    {
        protected RequestHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
            : base(unitOfWork, eventBus) { }

        public abstract void Handle();
    }

    public abstract class RequestHandlerByIn<TRequest> : BaseApplicationService, IRequestHandler
        where TRequest : IRequest
    {
        protected RequestHandlerByIn(IUnitOfWork unitOfWork, IEventBus eventBus)
            : base(unitOfWork, eventBus) { }

        public abstract void Handle(TRequest req);
    }

    public abstract class RequestHandlerByOut<TResult> : BaseApplicationService, IRequestHandler
        where TResult : class
    {
        protected RequestHandlerByOut(IUnitOfWork unitOfWork, IEventBus eventBus)
            : base(unitOfWork, eventBus) { }

        public abstract TResult Handle();
    }

    public abstract class RequestHandler<TIn, TResult> : BaseApplicationService, IRequestHandler
        where TIn : IRequest
        where TResult : class
    {
        protected RequestHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
            : base(unitOfWork, eventBus) { }

        public abstract TResult Handle(TIn req);
    }
}