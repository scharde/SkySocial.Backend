namespace DAL.Entity;

public class FollowerEntity : BaseEntity
{
    public Guid FollowerId { get; set; }
    public ApplicationUser Follower { get; set; }

    public Guid FolloweeId { get; set; }
    public ApplicationUser Followee { get; set; }

    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}