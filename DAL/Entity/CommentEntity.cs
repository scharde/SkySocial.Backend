namespace DAL.Entity;

public class CommentEntity : BaseEntity
{
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; }

    public Guid AuthorId { get; set; }
    public ApplicationUser Author { get; set; }

    public Guid? ParentCommentId { get; set; }
    public CommentEntity ParentComment { get; set; }

    public ICollection<CommentEntity> Replies { get; set; }

    public string Content { get; set; }
    public int Score { get; set; } = 0;
    
    public ICollection<CommentVoteEntity> CommentVotes { get; set; } = new List<CommentVoteEntity>();
}