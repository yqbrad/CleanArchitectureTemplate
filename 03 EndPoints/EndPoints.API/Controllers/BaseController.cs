using Framework.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using DDD.EndPoints.API.Extension;
using DDD.Infrastructure.Service.Configuration;

namespace DDD.EndPoints.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ServiceConfig Config => HttpContext.ServiceContext();
        
        protected IActionResult Handle(Action handler)
        {
            handler();
            return new OkResult();
        }

        protected IActionResult Handle<TRequest>(Action<TRequest> handler, TRequest request)
        {
            if (request == null)
                return new BadRequestResult();

            handler(request);
            return new OkResult();
        }

        protected IActionResult Handle<TResult>(Func<TResult> handler)
        {
            var result = handler();
            if (result is null)
                return new NoContentResult();

            return new OkObjectResult(result);
        }

        protected IActionResult Handle<TRequest, TResult>
            (Func<TRequest, TResult> handler, TRequest request)
        {
            if (request == null)
                return new BadRequestResult();

            var result = handler(request);
            if (result is null)
                return new NoContentResult();

            return new OkObjectResult(result);
        }

        //=====================Async=================================

        protected async Task<IActionResult> HandleAsync(Func<Task> handler)
        {
            await handler();
            return new OkResult();
        }

        protected async Task<IActionResult> HandleAsync<TRequest>
            (Func<TRequest, Task> handler, TRequest request)
            where TRequest : IRequest
        {
            if (request == null)
                return new BadRequestResult();

            await handler(request);
            return new OkResult();
        }

        protected async Task<IActionResult> HandleAsync<TResult>(Func<Task<TResult>> handler)
        {
            var result = await handler();
            if (result is null)
                return new NoContentResult();

            return new OkObjectResult(result);
        }

        protected async Task<IActionResult> HandleAsync<TRequest, TResult>
            (Func<TRequest, Task<TResult>> handleAsync, TRequest request)
        where TRequest : IRequest
        {
            if (request == null)
                return new BadRequestResult();

            var result = await handleAsync(request);
            if (result is null)
                return new NoContentResult();

            return new OkObjectResult(result);
        }
    }
}