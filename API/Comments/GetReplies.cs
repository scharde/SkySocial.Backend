using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Comments;

public partial class CommentsResource
{
    public async Task<PageResponse<CommentResponse>> GetReplies(Guid parentCommentId, int page = 1, int perPage = 10)
    {
        if (page < 1 || perPage < 1)
            throw new BadRequestException("Invalid pagination values");
        
        if (parentCommentId == Guid.Empty)
            throw new BadRequestException("Invalid parent comment id");

        var query = _dbContext.Comments
            .Where(x => x.ParentCommentId == parentCommentId);

        var totalCount = await query.CountAsync();

        var replies = await query
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .Select(x => new CommentResponse()
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
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
            PageSize = perPage,
            Items = replies
        };
    }
}