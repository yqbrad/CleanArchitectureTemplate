using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Services.WebApiCaller.Configuration;

namespace $safeprojectname$.ApiConfig
{
    public class ApiConfiguration : AbstractApiConfiguration
    {
        private readonly IConfiguration _configuration;
        public ApiConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            Load();
        }

        public sealed override void Load()
        {
            var section = _configuration.GetSection("ApiSites");
            section.Bind(ApiSites);
        }

        protected override string GetConfigFilePath()
            => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        public override void Save()
            => throw new Exception("You cannot save data by 'Save' method. Edit 'appsettings.json' from application root.");
    }
}