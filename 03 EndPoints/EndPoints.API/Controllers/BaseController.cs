using Framework.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using DDD.EndPoints.API.Extension;
using DDD.Infrastructure.Service.Configuration;
using FluentValidation;
using FluentValidation.TestHelper;
using Framework.Domain.Exceptions;

namespace DDD.EndPoints.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ServiceConfig Config => HttpContext.ServiceContext();
        protected IServiceProvider ServiceProvider => HttpContext.ServiceProvider();

        protected async Task<IActionResult> CreateAsync<TRequest, TResult>
            (Func<TRequest, Task<TResult>> handleAsync, TRequest request)
            where TRequest : IRequest<TResult>
            where TResult : Framework.Domain.Results.IResult
        {
            if (request == null)
                return new BadRequestResult();

            await ValidateRequest<TRequest, TResult>(request);

            var result = await handleAsync(request);
            if (result is null)
                return new NoContentResult();

            return new OkObjectResult(result)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        protected Task<IActionResult> CreateAsync<TRequest>
            (Func<TRequest, Task> handler, TRequest request)
            where TRequest : IRequest
            => HandleAsync(handler, request);

        protected Task<IActionResult> DeleteAsync<TRequest>
            (Func<TRequest, Task> handler, TRequest request)
            where TRequest : IRequest
            => HandleAsync(handler, request);

        protected Task<IActionResult> GetAsync<TRequest, TResult>
            (Func<TRequest, Task<TResult>> handleAsync, TRequest request)
            where TRequest : IRequest<TResult>
            where TResult : Framework.Domain.Results.IResult
            => HandleAsync(handleAsync, request);

        protected Task<IActionResult> GetAllAsync<TRequest, TResult>
            (Func<TRequest, Task<TResult>> handleAsync, TRequest request)
            where TRequest : IRequest<TResult>
            where TResult : Framework.Domain.Results.IResult
            => HandleAsync(handleAsync, request);

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

            await ValidateRequest(request);

            await handler(request);
            return new OkResult();
        }

        protected async Task<IActionResult> HandleAsync<TRequest, TResult>
            (Func<TRequest, Task<TResult>> handleAsync, TRequest request)
            where TRequest : IRequest<TResult>
            where TResult : Framework.Domain.Results.IResult
        {
            if (request == null)
                return new BadRequestResult();

            await ValidateRequest<TRequest, TResult>(request);

            var result = await handleAsync(request);
            if (result is null)
                return new NoContentResult();

            return new OkObjectResult(result);
        }

        private async Task ValidateRequest<TRequest>(TRequest request)
        where TRequest : IRequest
        {
            var validator = ServiceProvider.GetService<IValidator<TRequest>>();
            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    throw new DomainValidationException(validationResult.Errors.Select(item => item.ErrorMessage));
            }
        }

        private async Task ValidateRequest<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
            where TResult : Framework.Domain.Results.IResult
        {
            var validator = ServiceProvider.GetService<IValidator<TRequest>>();
            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    throw new DomainValidationException(validationResult.Errors.Select(item => item.ErrorMessage));
            }
        }
    }
}