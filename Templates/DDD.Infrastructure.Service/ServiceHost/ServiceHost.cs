using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace $safeprojectname$.ServiceHost
{
    public partial class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;
        public ServiceHost(IWebHost webHost) => _webHost = webHost;

        public void Run() => _webHost.Run();

        public static HostBuilder Create<TSturtup>(string[] args, string useUrl = "http://*:5050")
            where TSturtup : class
        {
            Console.Title = typeof(TSturtup).Namespace;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseUrls(useUrl)
                .UseConfiguration(config)
                .UseStartup<TSturtup>()
                .UseDefaultServiceProvider(options => { options.ValidateScopes = false; });

            return new HostBuilder(webHostBuilder.Build());
        }
    }
}