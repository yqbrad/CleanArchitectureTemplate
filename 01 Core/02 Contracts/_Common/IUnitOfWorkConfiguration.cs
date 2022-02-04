
namespace DDD.Contracts._Common
{
    public interface IUnitOfWorkConfiguration
    {
        string SqlServerConnectionString { get; set; }
        InitialData Seed { get; set; }
    }
}