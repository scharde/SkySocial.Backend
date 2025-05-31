using DAL;
using DAL.Entity;
using DAL.Model;

namespace API.Post;

public interface IPostResource
{
    Task<PageResponse<PostResponse>> GetFeed(Guid userId, int page = 1, int pageSize = 10);
    Task<PostEntity> CreatePost(PostCreateRequest request, Guid userId);
    Task<PostEntity> EditPost(Guid postId, PostUpdateRequest request, Guid userId);
}

public partial class PostResource(SocialDbContext dbContext) : IPostResource
{
    private readonly SocialDbContext _dbContext = dbContext;
}