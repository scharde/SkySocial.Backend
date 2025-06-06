using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace DAL;

public static class Bootstrapper
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ?? configuration.GetConnectionString("DefaultConnection");

        var dbUri = new Uri(connectionString);
        var userInfo = dbUri.UserInfo.Split(':');

        var npgsqlConnStr = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = dbUri.Host,
            Port = dbUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = dbUri.LocalPath.Trim('/'),
            SslMode = SslMode.Require,
            TrustServerCertificate = true
        }.ToString();


        services.AddDbContext<SocialDbContext>(options =>
            options.UseNpgsql(npgsqlConnStr)
        );

        return services;
    }
}
