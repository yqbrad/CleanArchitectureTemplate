
namespace DDD.Contracts._Base
{
    public interface IUnitOfWorkConfiguration
    {
        string SqlServerConnectionString { get; set; }
        InitialData Seed { get; set; }
    }
}