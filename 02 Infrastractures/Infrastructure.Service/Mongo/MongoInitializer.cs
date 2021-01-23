using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;

namespace DDD.Infrastructure.Service.Mongo
{
    public class MongoInitializer : IMongoInitializer
    {
        private bool _initialized;
        private readonly bool _seed;
        private readonly IMongoSeeder _seeder;

        public MongoInitializer(IOptions<MongoOptions> options, IMongoSeeder seeder)
        {
            _seed = options.Value.Seed;
            _seeder = seeder;
        }

        public async Task InitializerAsync()
        {
            if (_initialized)
                return;

            RegisterConventions();

            _initialized = true;

            if (!_seed)
                return;

            await _seeder.SeedAsync();
        }

        private void RegisterConventions()
            => ConventionRegistry.Register("Conventions", new MongoConventions(), x => true);
    }
}