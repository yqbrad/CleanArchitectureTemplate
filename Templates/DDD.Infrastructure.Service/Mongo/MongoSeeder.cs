using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace $safeprojectname$.Mongo
{
    public class MongoSeeder : IMongoSeeder
    {
        protected readonly IMongoDatabase Database;

        public MongoSeeder(IMongoDatabase database) => Database = database;

        public async Task SeedAsync()
        {
            var collectionsCursor = await Database.ListCollectionsAsync();
            var collections = await collectionsCursor.ToListAsync();

            if (collections.Any())
                return;

            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync() => await Task.CompletedTask;
    }
}