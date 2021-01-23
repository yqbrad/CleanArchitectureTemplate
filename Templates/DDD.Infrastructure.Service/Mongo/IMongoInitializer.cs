using System.Threading.Tasks;

namespace $safeprojectname$.Mongo
{
    public interface IMongoInitializer
    {
        Task InitializerAsync();
    }
}