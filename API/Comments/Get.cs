using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Comments;

public partial class CommentsResource
{
    public async Task<PageResponse<CommentResponse>> Get(Guid postId, int page = 1, int pageSize = 20)
    {
        if (page < 1 || pageSize < 1)
            throw new BadRequestException("Invalid pagination values");


        var query = dbContext.Comments
            .Include(p => p.Author)
            .Where(x => x.PostId == postId && x.ParentCommentId == null)
            .OrderByDescending(c => c.CreatedDateUtc);

        var totalCount = await query.CountAsync();

        var comments = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new CommentResponse()
            {
                Id = x.Id,
                Author = new AuthorDetail()
                {
                    Id = x.AuthorId,
                    Name = x.Author.ToString()
                },
                Content = x.Content,
                CreatedDateUtc = x.CreatedDateUtc,
                Score = x.Score,
                ParentCommentId = x.ParentCommentId
            })
            .ToListAsync();

        return new PageResponse<CommentResponse>()
        {
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            Items = comments
        };
    }
}