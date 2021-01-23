namespace $safeprojectname$._Base
{
    public interface IUnitOfWorkConfiguration
    {
        string MongoConnectionString { get; set; }
        string SqlServerConnectionString { get; set; }
        string RedisConnectionString { get; set; }
        InitialData Seed { get; set; }
    }
}