using Microsoft.EntityFrameworkCore;

namespace API.Follow;

public partial class FollowResource
{
    public  async Task<List<Guid>> Get(Guid userId)
    {
        var result =await _dbContext.Followers.Where(x => x.FollowerId == userId).Select(x => x.FollowingToId).ToListAsync();
        return result;
    }
}