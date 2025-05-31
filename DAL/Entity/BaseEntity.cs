namespace DAL.Entity;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDateUtc { get; set; } = DateTime.UtcNow;
}