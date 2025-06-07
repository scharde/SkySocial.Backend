using DAL;
using DAL.Model;

namespace API.Follow;

public interface IFollowResource
{
    Task<List<Guid>> Get(Guid userId);
    Task<FollowToggleResponse> Post(Guid followeeId, Guid userId);
}

public partial class FollowResource(SocialDbContext dbContext) : IFollowResource
{
    private readonly SocialDbContext _dbContext = dbContext;
}