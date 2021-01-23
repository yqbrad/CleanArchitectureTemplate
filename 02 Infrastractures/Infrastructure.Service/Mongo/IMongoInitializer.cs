using System.Threading.Tasks;

namespace DDD.Infrastructure.Service.Mongo
{
    public interface IMongoInitializer
    {
        Task InitializerAsync();
    }
}