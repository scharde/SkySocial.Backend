using Microsoft.AspNetCore.Identity;

namespace DAL.Entity;

public class ApplicationUser: IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }
    
    public ICollection<PostEntity> Posts { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }
    public ICollection<PostVoteEntity> PostVotes { get; set; }
    public ICollection<CommentVoteEntity> CommentVotes { get; set; }
    
    public static ApplicationUser Create(string email, string firstName, string lastName)
    {
        return new ApplicationUser
        {
            Email = email,
            UserName = email,
            FirstName = firstName,
            LastName = lastName
        };
    }
    
    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
