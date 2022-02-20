using $safeprojectname$.Requests;
using System.Threading.Tasks;

namespace $safeprojectname$.ApplicationServices
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