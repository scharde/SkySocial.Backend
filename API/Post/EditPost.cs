using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Post;

public partial class PostResource
{
    public async Task<PostResponse> EditPost(Guid postId, PostUpdateRequest request, Guid userId)
    {
        var existingPost = await _dbContext.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        if (existingPost is null)
            throw new NotFoundException("Post not found");

        if (existingPost.AuthorId != userId)
            throw new ForbiddenException("Forbidden");
            
        existingPost.Content = request.Content;
        existingPost.UpdatedDateUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        
        return await GetById(userId, existingPost.Id);
    }
}