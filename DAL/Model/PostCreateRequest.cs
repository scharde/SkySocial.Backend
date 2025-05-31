namespace DAL.Model;

public class PostCreateRequest
{
    public string Content { get; set; }
}

public class PostUpdateRequest
{
    public string Content { get; set; }
}

public class PostResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }

    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }

    public int Score { get; set; }
    public int CommentCount { get; set; }

    public DateTime CreatedDateUtc { get; set; }
}