using System.Threading.Tasks;

namespace $safeprojectname$.Mongo
{
    public interface IMongoSeeder
    {
        Task SeedAsync();
    }
}