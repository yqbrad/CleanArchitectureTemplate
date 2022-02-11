using System.Threading.Tasks;
using DDD.Contracts._Common;
using Framework.Domain.ApplicationServices;
using Framework.Domain.EventBus;
using Framework.Domain.Requests;
using Framework.Domain.Results;

namespace DDD.ApplicationServices._Common
{
    public abstract class RequestHandler : BaseApplicationService, IRequestHandler
    {
        protected RequestHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public abstract Task HandleAsync();
    }

    public abstract class RequestHandler<TRequest> : BaseApplicationService, IRequestHandler
        where TRequest : IRequest
    {
        protected RequestHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public abstract Task HandleAsync(TRequest req);
    }

    public abstract class RequestHandler<TRequest, TResult> : BaseApplicationService, IRequestHandler
        where TRequest : IRequest<TResult>
        where TResult : IResult
    {
        protected RequestHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public abstract Task<TResult> HandleAsync(TRequest req);
    }
}