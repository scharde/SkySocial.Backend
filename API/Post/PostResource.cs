using DAL;
using DAL.Entity;
using DAL.Model;

namespace API.Post;

public interface IPostResource
{
    Task<PostResponse> GetById(Guid userId, Guid id);
    Task<PageResponse<PostResponse>> GetFeed(Guid userId, int page = 1, int pageSize = 10);
    Task<PostResponse> CreatePost(PostCreateRequest request, Guid userId);
    Task<PostResponse> EditPost(Guid postId, PostUpdateRequest request, Guid userId);
}

public partial class PostResource(SocialDbContext dbContext) : IPostResource
{
    private readonly SocialDbContext _dbContext = dbContext;
}