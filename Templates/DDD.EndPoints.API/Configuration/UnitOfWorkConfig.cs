using DDD.Contracts._Base;
using Microsoft.Extensions.Configuration;

namespace $safeprojectname$.Configuration
{
    public class UnitOfWorkConfig : IUnitOfWorkConfiguration
    {
        public UnitOfWorkConfig(IConfiguration config)
        {
            var section = config.GetSection(nameof(UnitOfWorkConfig));
            section.Bind(this);
        }

        public string MongoConnectionString { get; set; }
        public string SqlServerConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
        public InitialData Seed { get; set; }
    }
}