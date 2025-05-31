using DAL.Entity;

namespace DAL.Model;

public class CommentCreateRequest
{
    public Guid PostId { get; set; }
    public Guid? ParentCommentId { get; set; }

    public string Content { get; set; }
}

public class CommentResponse : BaseEntity
{
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public int Score { get; set; }
}