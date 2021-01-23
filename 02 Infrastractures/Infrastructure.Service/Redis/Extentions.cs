using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Linq;

namespace DDD.Infrastructure.Service.Redis
{
    public static class Extensions
    {
        public static void AddRedisDB(this IServiceCollection service, IConfiguration configuration)
        {
            var options = new RedisOptions();
            var section = configuration.GetSection("redis");
            section.Bind(options);

            var redis = ConnectionMultiplexer.Connect(options.ConnectionString);

            service.AddSingleton<IDatabase>(redis.GetDatabase());
            service.AddSingleton<IServer>(redis.GetServer(redis.GetEndPoints().First()));
        }
    }
}