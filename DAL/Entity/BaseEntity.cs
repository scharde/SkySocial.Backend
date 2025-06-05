namespace DAL.Entity;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public DateTime UpdatedDateUtc { get; set; }
    
    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedDateUtc = DateTime.UtcNow;
        UpdatedDateUtc = DateTime.UtcNow;
    }
}