using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Infrastructure.Service.MediatR
{
    public static class Extensions
    {
        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddSingleton(Console.Out);
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton(services.BuildServiceProvider().GetRequiredService<IMediator>());
        }
    }
}