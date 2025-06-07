using DAL.Entity;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Follow;

public partial class FollowResource
{
    public async Task<FollowToggleResponse> Post(Guid followeeId, Guid userId)
    {
        var record = await _dbContext.Followers.SingleOrDefaultAsync(x =>
            x.FollowerId == userId && x.FollowingToId == followeeId);
        if (record is not null)
        {
            // Already followed, now unfollow
            _dbContext.Followers.Remove(record);
            await _dbContext.SaveChangesAsync();
            return null;
        }

        record = new FollowerEntity()
            { FollowerId = userId, FollowingToId = followeeId, FollowedAt = DateTime.UtcNow };
        await _dbContext.Followers.AddAsync(record);
        await _dbContext.SaveChangesAsync();
        
        return new FollowToggleResponse() { FolloweeId = followeeId };
    }
}