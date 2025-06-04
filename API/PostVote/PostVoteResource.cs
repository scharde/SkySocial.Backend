using DAL;
using DAL.Model;

namespace API.PostVote;

public interface IPostVoteResource
{
    Task<UserVoteResponse> Post(Guid postId, Guid userId, int vote);
}

public partial class PostVoteResource(SocialDbContext dbContext) : IPostVoteResource
{
    private readonly SocialDbContext _dbContext = dbContext;
}