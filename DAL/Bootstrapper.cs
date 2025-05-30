using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class Bootstrapper
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContext<SocialContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Social"))
        );

        return services;
    }
}
