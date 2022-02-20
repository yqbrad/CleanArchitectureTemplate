using YQB.Contracts._Common;
using Framework.Domain.ApplicationServices;
using Framework.Domain.EventBus;
using Framework.Domain.Requests;
using System.Threading.Tasks;

namespace YQB.ApplicationServices._Common
{
    public abstract class RequestHandler<TRequest> : BaseApplicationService,
        IRequestHandler<TRequest>
        where TRequest : IRequest
    {
        protected RequestHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public abstract Task HandleAsync(TRequest req);
    }

    public abstract class RequestHandler<TRequest, TResult> : BaseApplicationService,
        IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        protected RequestHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public abstract Task<TResult> HandleAsync(TRequest req);
    }
}