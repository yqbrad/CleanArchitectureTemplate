using Framework.Domain.Requests;
using System.Threading.Tasks;

namespace Framework.Domain.ApplicationServices
{
    public interface IRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        Task HandleAsync(TRequest req);
    }

    public interface IRequestHandler<in TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        Task<TResult> HandleAsync(TRequest req);
    }
}