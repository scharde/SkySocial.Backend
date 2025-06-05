using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class Bootstrapper
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
        
        services.AddDbContext<SocialDbContext>(options =>
            options.UseSqlServer(connectionString)
        );
        
        return services;
    }
}
