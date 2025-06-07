using DAL;
using DAL.Model;

namespace API.Comments;

public interface ICommentsResource
{
    Task<PageResponse<CommentResponse>> Get(Guid postId, int page = 1, int pageSize = 20);
    Task<CommentResponse> GetById(Guid id);
    Task<PageResponse<CommentResponse>> GetReplies(Guid parentCommentId, int page = 1, int pageSize = 10);
    Task<CommentResponse> CreateComment(CommentCreateRequest request, Guid userId);
}

public partial class CommentsResource(SocialDbContext dbContext) : ICommentsResource
{
    private readonly SocialDbContext _dbContext = dbContext;
}