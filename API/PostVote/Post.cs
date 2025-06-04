using DAL.Entity;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.PostVote;

public partial class PostVoteResource
{
    public async Task<UserVoteResponse> Post(Guid postId, Guid userId, int vote)
    {
        var userVote = await _dbContext.PostVotes.SingleOrDefaultAsync(x => x.UserId == userId && x.PostId == postId);
        if (userVote is null && vote > 0)
        {
            // When Up or down vote, and already not exist, create record
            userVote = new PostVoteEntity()
            {
                UserId = userId,
                PostId = postId,
                Type = (VoteType)vote
            };

            _dbContext.PostVotes.Add(userVote);
            await _dbContext.SaveChangesAsync();
        }
        else if (userVote is not null && userVote.Type == (VoteType)vote)
        {
            // Vote is removed
            _dbContext.PostVotes.Remove(userVote);
            await _dbContext.SaveChangesAsync();
            return null;
        }
        else if (userVote is not null && vote > 0)
        {
            // When user vote exist and vote is also up or down, then update

            userVote.Type = (VoteType)vote;
            await _dbContext.SaveChangesAsync();
        }

        return new UserVoteResponse()
        {
            PostId = postId,
            UserId = userId,
            Type = userVote.Type
        };
    }
}