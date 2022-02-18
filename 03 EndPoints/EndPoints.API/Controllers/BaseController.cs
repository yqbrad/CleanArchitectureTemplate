using DDD.EndPoints.API.Extension;
using DDD.Infrastructure.Service.Configuration;
using FluentValidation;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Exceptions;
using Framework.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DDD.EndPoints.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ServiceConfig Config => HttpContext.ServiceContext();
        protected IServiceProvider ServiceProvider => HttpContext.ServiceProvider();

        protected async Task<IActionResult> CreateAsync<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
        {
            if (request == null)
                return new BadRequestResult();

            await ValidateRequest<TRequest, TResult>(request);

            var handler = ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResult>>();
            var result = await handler.HandleAsync(request);
            if (result is null)
                return new NoContentResult();

            return new OkObjectResult(result)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        protected Task<IActionResult> CreateAsync<TRequest>(TRequest request)
            where TRequest : IRequest
            => HandleAsync(request);

        protected Task<IActionResult> UpdateAsync<TRequest>(TRequest request)
            where TRequest : IRequest
            => HandleAsync(request);

        protected Task<IActionResult> DeleteAsync<TRequest>(TRequest request)
            where TRequest : IRequest
            => HandleAsync(request);

        protected Task<IActionResult> GetAsync<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
            => HandleAsync<TRequest, TResult>(request);

        protected Task<IActionResult> GetAllAsync<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
            => HandleAsync<TRequest, TResult>(request);

        protected async Task<IActionResult> HandleAsync<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            if (request == null)
                return new BadRequestResult();

            await ValidateRequest(request);

            var handler = ServiceProvider.GetRequiredService<IRequestHandler<TRequest>>();
            await handler.HandleAsync(request);

            return new OkResult();
        }

        protected async Task<IActionResult> HandleAsync<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
        {
            if (request == null)
                return new BadRequestResult();

            await ValidateRequest<TRequest, TResult>(request);

            var handler = ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResult>>();
            var result = await handler.HandleAsync(request);
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
                    throw new FluentValidationException(validationResult.Errors.Select(item => item.ErrorMessage));
            }
        }

        private async Task ValidateRequest<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
        {
            var validator = ServiceProvider.GetService<IValidator<TRequest>>();
            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    throw new FluentValidationException(validationResult.Errors.Select(item => item.ErrorMessage));
            }
        }
    }
}