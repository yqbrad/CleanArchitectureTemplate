using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

namespace DDD.EndPoints.API
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args)
            .Build()
            .Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureAppConfiguration((_, config) =>
                {
                    var buildConfiguration = typeof(Program).Assembly
                        .GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;

                    var path = "appsettings.json";
                    if (buildConfiguration != "Debug")
                        path = $"appsettings.{buildConfiguration}.json";

                    config.Sources.Clear();
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile(path, false, true);
                });
    }
}