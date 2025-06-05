using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Comments;

public partial class CommentsResource
{
    public async Task<PageResponse<CommentResponse>> GetReplies(Guid parentCommentId, int page = 1, int pageSize = 10)
    {
        if (page < 1 || pageSize < 1)
            throw new BadRequestException("Invalid pagination values");
        
        if (parentCommentId == Guid.Empty)
            throw new BadRequestException("Invalid parent comment id");

        var query = _dbContext.Comments
            .Include(p => p.Author)
            .Where(x => x.ParentCommentId == parentCommentId);

        var totalCount = await query.CountAsync();

        var replies = await query
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
            Items = replies
        };
    }
}