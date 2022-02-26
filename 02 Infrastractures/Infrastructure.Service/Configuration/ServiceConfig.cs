namespace YQB.Infra.Service.Configuration
{
    public class ServiceConfig
    {
        public string Id { get; set; } = "Service01";
        public int CacheDuration { get; set; }
        public string HealthCheckRoute { get; set; }
        public IdpConfig Idp { get; set; }
        public SwaggerConfig Swagger { get; set; }
        public string Banner { get; set; }
        public string[] AssemblyNames { get; set; }
        public string[] AllowedOrigins { get; set; }
    }
}