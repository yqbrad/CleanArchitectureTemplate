using Microsoft.Extensions.Configuration;
using YQB.Contracts._Common;

namespace YQB.Infra.Service.Configuration
{
    public class UnitOfWorkConfig : IUnitOfWorkConfiguration
    {
        public UnitOfWorkConfig(IConfiguration config)
        {
            var section = config.GetSection(nameof(UnitOfWorkConfig));
            section.Bind(this);
        }
        
        public string SqlServerConnectionString { get; set; }
        public InitialData Seed { get; set; }
    }
}