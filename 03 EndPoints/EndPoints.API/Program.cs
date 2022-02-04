using System.Reflection;
using System.Text.Json.Serialization;
using DDD.Contracts._Base;
using DDD.EndPoints.API;
using DDD.EndPoints.API.Extension;
using DDD.EndPoints.API.Filters;
using Framework.Domain.Error;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var buildConfiguration = typeof(Program).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;

var path = "appsettings.json";
if (buildConfiguration != "Debug")
    path = $"appsettings.{buildConfiguration}.json";

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile(path, false, true);

var configuration = builder.Configuration;
var serviceConfig = builder.Services.AddServiceConfig(configuration);

builder.Services.AddControllers(options =>
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

builder.Services.AddIdp(serviceConfig);
builder.Services.Inject(configuration);
builder.Services.AddResponseCaching();
builder.Services.AddHeaderPropagation(serviceConfig);
builder.Services.AddSwagger(serviceConfig);
builder.Services.AddHealthCheck(configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseHttpsRedirection();
app.UseHeaderPropagation();
app.UseStaticFiles();
// app.UseCookiePolicy();
app.UseRouting();
//app.UseRequestLocalization();
//app.UseCors();
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

app.Services.GetService<IUnitOfWork>()?.InitiateDatabase();

app.Run();