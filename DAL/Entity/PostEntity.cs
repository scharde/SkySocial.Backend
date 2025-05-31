namespace DAL.Entity;

public class PostEntity : BaseEntity
{
    public Guid AuthorId { get; set; }
    public ApplicationUser Author { get; set; }

    public string Content { get; set; }
    public int Score { get; set; }
}