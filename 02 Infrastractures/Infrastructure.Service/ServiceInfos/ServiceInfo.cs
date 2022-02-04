using Framework.Domain.Dtoes;

namespace DDD.Infrastructure.Service.ServiceInfos
{
    public class ServiceInfo : IDto
    {
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Mac { get; set; }
        public string Version { get; set; }
        public string BuildType { get; set; }
    }
}