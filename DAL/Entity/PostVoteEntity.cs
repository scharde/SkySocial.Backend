namespace DAL.Entity;

public class PostVoteEntity : BaseEntity
{
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public VoteType Type { get; set; }
}

public enum VoteType
{
    Upvote = 1,
    Downvote = 2,
}