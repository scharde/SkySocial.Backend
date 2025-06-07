using DAL.Entity;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Comments;

public partial class CommentsResource
{
    public async Task<CommentResponse> CreateComment(CommentCreateRequest request, Guid userId)
    {
        var existingPost = await _dbContext.Posts.SingleOrDefaultAsync(x => x.Id == request.PostId);
        if (existingPost is null)
            throw new NotFoundException("Post not found");

        if (request.ParentCommentId.HasValue)
        {
            var parentComment = await _dbContext.Comments.FindAsync(request.ParentCommentId.Value);
            if (parentComment is null || parentComment.PostId != request.PostId)
                throw new NotFoundException("Parent comment not found");
        }

        var comment = new CommentEntity()
        {
            AuthorId = userId,
            PostId = request.PostId,
            ParentCommentId = request.ParentCommentId,
            Content = request.Content,
            Score = 0
        };

        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();

        return await GetById(comment.Id);
    }
}