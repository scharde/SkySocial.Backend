using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Comments;

public partial class CommentsResource
{
    public async Task<CommentResponse> GetById(Guid id)
    {
        var query = dbContext.Comments
            .Include(p => p.Author)
            .Where(x => x.Id == id)
            .OrderByDescending(c => c.CreatedDateUtc);

        var comment = await query
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
            .SingleOrDefaultAsync();
        
        return comment;
    }
}