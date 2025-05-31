namespace DAL.Entity;

public class CommentVoteEntity: BaseEntity
{
    public Guid CommentId { get; set; }
    public CommentEntity Comment { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    public VoteType Type { get; set; }
}