using DAL.Entity;
using DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class SocialContext: IdentityDbContext<ApplicationUser>
{
    public SocialContext(DbContextOptions<SocialContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> User { get; set; }
}
