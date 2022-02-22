using YQB.Contracts._Common;
using YQB.EndPoints.API.Extension;
using YQB.EndPoints.API.Filters;
using Framework.Domain.Error;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppsettings();

var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog((hbc, lc)
    => lc.ReadFrom.Configuration(hbc.Configuration));

var serviceConfig = builder.Services.AddServiceConfig(configuration);
builder.Services
    .AddDependencies(configuration, serviceConfig)
    .AddControllers(options =>
    {
        options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
        options.Filters.Add(new ProducesResponseTypeAttribute(typeof(Error), 499));
        options.Filters.Add<ExceptionFilter>();
        options.EnableEndpointRouting = false;
        options.CacheProfiles.Add("Default", new CacheProfile
        {
            Duration = serviceConfig.CacheDuration
        });
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseHeaderPropagation();
app.UseStaticFiles();
// app.UseCookiePolicy();
app.UseRouting();
//app.UseRequestLocalization();
app.ConfigCors(serviceConfig.AllowedOrigins);
//app.UseAuthentication();
app.UseAuthorization();
// app.UseSession();
// app.UseResponseCompression();
app.UseResponseCaching();
app.MapControllers();

app.ConfigSwagger(serviceConfig);
app.UseHealthChecks(serviceConfig.HealthCheckRoute, new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.AddBanner(serviceConfig);
var scope = app.Services.CreateScope();
scope.ServiceProvider.GetService<IUnitOfWork>()?.InitiateDatabase();

app.Run();