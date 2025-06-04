using DAL.Entity;
using DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class SocialDbContext(DbContextOptions<SocialDbContext> options): IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<PostVoteEntity> PostVotes { get; set; }
    public DbSet<CommentVoteEntity> CommentVotes { get; set; }

    public DbSet<FollowerEntity> Followers { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PostEntity>().ToTable("Post");
        builder.Entity<CommentEntity>().ToTable("Comment");
        builder.Entity<PostVoteEntity>().ToTable("PostVote");
        builder.Entity<CommentVoteEntity>().ToTable("CommentVote");

        builder.Entity<FollowerEntity>().ToTable("Follower");
        builder.Entity<ApplicationUser>().ToTable("User");

        builder.Entity<FollowerEntity>()
            .HasKey(f => new { f.FollowerId, f.FolloweeId });

        builder.Entity<PostEntity>()
            .HasMany(f => f.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PostEntity>()
            .HasMany(f => f.PostVotes)
            .WithOne(c => c.Post)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<FollowerEntity>()
            .HasOne(f => f.Follower)
            .WithMany()
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<FollowerEntity>()
            .HasOne(f => f.Followee)
            .WithMany()
            .HasForeignKey(f => f.FolloweeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CommentEntity>()
            .HasOne(c => c.Author)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PostVoteEntity>()
            .HasIndex(v => new { v.UserId, v.PostId })
            .IsUnique();
        
        builder.Entity<PostVoteEntity>()
            .HasOne(v => v.User)
            .WithMany(u => u.PostVotes)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict); // 👈 Avoid cascade

        builder.Entity<CommentVoteEntity>()
            .HasIndex(v => new { v.UserId, v.CommentId })
            .IsUnique();
        
        builder.Entity<CommentVoteEntity>()
            .HasOne(v => v.Comment)
            .WithMany(p => p.CommentVotes)
            .HasForeignKey(v => v.CommentId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
