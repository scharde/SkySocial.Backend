using API.Auth;
using API.Post;
using API.TokenProcessor;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API;

public static class Bootstrapper
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDAL(configuration)
            .AddTransient<IAuthResource, AuthResource>()
            .AddTransient<IAuthTokenProcessor, AuthTokenProcessor>()
            .AddTransient<IPostResource, PostResource>();

        return services;
    }
}
