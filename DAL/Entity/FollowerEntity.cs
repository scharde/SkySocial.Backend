namespace DAL.Entity;

public class FollowerEntity : BaseEntity
{
    public Guid FollowerId { get; set; } // User who followed (User)
    public ApplicationUser Follower { get; set; }

    public Guid FollowingToId { get; set; } // To whom user has followed
    public ApplicationUser FollowingTo { get; set; }

    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}