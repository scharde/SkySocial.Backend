using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class SocialContext: DbContext
{
    public SocialContext(DbContextOptions<SocialContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> User { get; set; }
}
