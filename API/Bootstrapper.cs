using API.Auth;
using API.Comments;
using API.Follow;
using API.Post;
using API.PostVote;
using API.TokenProcessor;
using API.User;
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
            .AddTransient<IPostResource, PostResource>()
            .AddTransient<IUserResource, UserResource>()
            .AddTransient<IPostVoteResource, PostVoteResource>()
            .AddTransient<IFollowResource, FollowResource>()
            .AddTransient<ICommentsResource, CommentsResource>();

        return services;
    }
}
