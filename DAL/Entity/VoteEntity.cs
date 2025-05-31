namespace DAL.Entity;

public class VoteEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public Guid TargetId { get; set; } // Post or Comment Id
    public TargetType TargetType { get; set; } // Post or Comment

    public VoteType Value { get; set; } 
}

public enum VoteType
{
    Upvote= 1,
    Downvote = -1,
}

public enum TargetType
{
    Post = 1,
    Comment = 2,
}