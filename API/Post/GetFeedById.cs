using DAL.Entity;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Post;

public partial class PostResource
{
    public async Task<PostResponse> GetById(Guid userId, Guid id)
    {
        var followedUserIds = await _dbContext.Followers
            .Where(x => x.FollowerId == userId)
            .Select(c => c.FollowingToId)
            .ToListAsync();

        var post = await dbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Comments)
            .Include(p => p.PostVotes)
            .Where(p => p.Id == id)
            .Select(p => new
            {
                Post = p,
                IsFollowedAuthor = followedUserIds.Contains(p.AuthorId),
                Score = p.PostVotes.Sum(x => x.Type == VoteType.Upvote ? 1 : -1),
                CommentCount = p.Comments.Count,
                UpVotesCount = p.PostVotes.Count(x => x.Type == VoteType.Upvote),
                DownVotesCount = p.PostVotes.Count(x => x.Type == VoteType.Downvote),
                UserVote = _dbContext.PostVotes
                    .Where(x => x.PostId == p.Id && x.UserId == userId)
                    .Select(x => x.Type)
                    .SingleOrDefault(),
                CreatedDateUtc = p.CreatedDateUtc
            })
            .SingleOrDefaultAsync();

        if (post is null)
            return null;

        return new PostResponse
        {
            Id = post.Post.Id,
            Content = post.Post.Content,
            Author = new AuthorDetail
            {
                Id = post.Post.AuthorId,
                Name = $"{post.Post.Author.FirstName} {post.Post.Author.LastName}",
                Title = post.Post.Author.Title,
            },
            Score = post.Score,
            CommentCount = post.CommentCount,
            UpVotes = post.UpVotesCount,
            DownVotes = post.DownVotesCount,
            CreatedDateUtc = post.CreatedDateUtc,
            UserVote = post.UserVote
        };
    }
}