using DAL.Entity;
using DAL.Model;

namespace API.Post;

public partial class PostResource
{
    public async Task<PostEntity> CreatePost(PostCreateRequest request, Guid userId)
    {
        if (userId == Guid.Empty)
            throw new UnauthorizedAccessException();

        var post = new PostEntity()
        {
            AuthorId = userId,
            Content = request.Content,
        };

        await _dbContext.Posts.AddAsync(post);
        return post;
    }
}