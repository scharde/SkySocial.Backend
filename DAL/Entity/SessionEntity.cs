namespace DAL.Entity;

public class SessionEntity: BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime LoginTime { get; set; }
    public DateTime? LogoutTime { get; set; }
    public int? Duration => LogoutTime.HasValue ? (int?)(LogoutTime.Value - LoginTime).TotalSeconds : null;
}