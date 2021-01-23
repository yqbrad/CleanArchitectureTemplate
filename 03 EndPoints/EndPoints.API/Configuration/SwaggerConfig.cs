namespace DDD.EndPoints.API.Configuration
{
    public class SwaggerConfig
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsEnable { get; set; }
        public string RoutePrefix { get; set; }
    }
}