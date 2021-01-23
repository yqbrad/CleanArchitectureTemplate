namespace $safeprojectname$.Configuration
{
    public class ServiceConfig
    {
        public string Id { get; set; } = "Service01";
        public string LoggerToken { get; set; }
        public int CacheDuration { get; set; }
        public string HealthCheckRoute { get; set; }
        public IdpConfig Idp { get; set; }
        public SwaggerConfig Swagger { get; set; }
    }
}