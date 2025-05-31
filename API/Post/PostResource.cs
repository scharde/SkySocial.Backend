using DAL;
using DAL.Entity;
using DAL.Model;

namespace API.Post;

public interface IPostResource
{
    Task<PostEntity> CreatePost(PostCreateRequest request, Guid userId);
    Task<PostEntity> EditPost(Guid postId, PostUpdateRequest request, Guid userId);
}

public partial class PostResource(SocialDbContext dbContext) : IPostResource
{
    private readonly SocialDbContext _dbContext = dbContext;
}