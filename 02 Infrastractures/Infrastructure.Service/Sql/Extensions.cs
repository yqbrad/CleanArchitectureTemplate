using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace DDD.Infrastructure.Service.Sql
{
    public static class Extensions
    {
        public static void AddSqlDB(this IServiceCollection service, IConfiguration configuration)
        {
            var options = new SqlOptions();
            var section = configuration.GetSection("sql");
            section.Bind(options);

            service.AddTransient<IDbConnection>(c => new SqlConnection(options.ConnectionString));
        }
    }
}