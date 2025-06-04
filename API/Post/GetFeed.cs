using DAL.Entity;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Post;

public partial class PostResource
{
    public async Task<PageResponse<PostResponse>> GetFeed(Guid userId, int page = 1, int pageSize = 10)
    {
        var followedUserIds = await _dbContext.Followers
            .Where(x => x.FollowerId == userId)
            .Select(c => c.FolloweeId)
            .ToListAsync();

        var query = dbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Comments)
            .Include(p => p.PostVotes)
            .Select(p => new
            {
                Post = p,
                IsFollowedAuthor = followedUserIds.Contains(p.AuthorId),
                Score = p.PostVotes.Sum(x => x.Type == VoteType.Upvote ? 1 : -1),
                CommentCount = p.Comments.Count,
                UpVotesCount = p.PostVotes.Count(x => x.Type == VoteType.Upvote),
                DownVotesCount = p.PostVotes.Count(x => x.Type == VoteType.Downvote),
                UserVote = _dbContext.PostVotes.Where(x => x.UserId == userId && x.PostId == p.Id).Select(x => x.Type).SingleOrDefault(),
                CreatedDateUtc = p.CreatedDateUtc
            });

        var ordered = query
            .OrderByDescending(x => x.IsFollowedAuthor) // 1. Followed author post
            .ThenByDescending(x => x.Score) // 2. Higher scores
            .ThenByDescending(x => x.CommentCount) // 3. more comments
            .ThenByDescending(x => x.CreatedDateUtc);

        var totalCount = await ordered.CountAsync();
        var posts = await ordered
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new PostResponse()
            {
                Id = x.Post.Id,
                Content = x.Post.Content,
                Author = new AuthorDetail
                {
                    Id = x.Post.AuthorId, 
                    Name = $"{x.Post.Author.FirstName} {x.Post.Author.LastName}",
                    Title = x.Post.Author.Title,
                    IsFollowing = x.IsFollowedAuthor,
                },
                Score = x.Score,
                CommentCount = x.CommentCount,
                UpVotes = x.UpVotesCount,
                DownVotes = x.DownVotesCount,
                CreatedDateUtc = x.CreatedDateUtc,
                UserVote = x.UserVote
            })
            .ToListAsync();

        return new PageResponse<PostResponse>()
        {
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            Items = posts
        };
    }
}