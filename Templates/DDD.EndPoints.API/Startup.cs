using $safeprojectname$.Configuration;
using $safeprojectname$.Extension;
using $safeprojectname$.Filters;
using DDD.Infrastructure.Service.DB;
using HealthChecks.UI.Client;
using Logger.EndPoints.Service.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceConfig = services.AddServiceConfig(Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.ApiName = serviceConfig.Idp.ApiName;
                    options.Authority = serviceConfig.Idp.ServerUrl;
                });

            services.Inject(Configuration);

            services.AddControllers();

            services.AddMvc(option =>
            {
                option.Filters.Add<ExceptionFilter>();
                option.EnableEndpointRouting = false;
                option.CacheProfiles.Add("Default", new CacheProfile
                {
                    Duration = serviceConfig.CacheDuration
                });
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddResponseCaching();
            services.AddSwagger(serviceConfig);
            services.AddHealthCheck(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ServiceConfig config)
        {
            // Look at this to middleware order:
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0#middleware-order
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseCookiePolicy();

            app.UseRouting();
            // app.UseRequestLocalization();
            //app.UseCors();

            //app.UseAuthentication();
            app.UseAuthorization();
            // app.UseSession();
            // app.UseResponseCompression();
            app.UseResponseCaching();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            /*
             * Other middleware
             */

            app.ConfigSwagger(config);

            app.UseHealthChecks(config.HealthCheckRoute, new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            Initialize(app, config);
        }

        private static void Initialize(IApplicationBuilder app, ServiceConfig config)
        {
            using var scope = app.ApplicationServices.CreateScope();
            scope.ServiceProvider.GetRequiredService<DatabaseInitializer>().Initialize();
            app.InitializeLogger(config.LoggerToken);
        }
    }
}