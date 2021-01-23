using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace $safeprojectname$.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDB(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<MongoOptions>(configuration.GetSection("mongo"));
            service.AddSingleton(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                return new MongoClient(options.Value.ConnectionString);
            });

            service.AddScoped(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(options.Value.Database);
            });

            service.AddScoped<IMongoSeeder, MongoSeeder>();
            service.AddScoped<IMongoInitializer, MongoInitializer>();
        }
    }
}