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
                    var buildConfiguration = typeof(Program)
                        .Assembly
                        .GetCustomAttribute<AssemblyConfigurationAttribute>()?
                        .Configuration;

                    config.Sources.Clear();
                    config.SetBasePath($"{Directory.GetCurrentDirectory()}\\AppSettings");
                    config.AddJsonFile($"appsettings.{buildConfiguration}.json", optional: false, reloadOnChange: true);
                });
    }
}