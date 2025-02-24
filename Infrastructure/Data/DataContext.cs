using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = System.IO.File;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{

    public DbSet<User> Users { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UploadedFile> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();

        modelBuilder.Entity<News>()
            .HasMany(n => n.Tags)
            .WithMany();

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UploadedFile>()
            .Property(f => f.FileSize)
            .HasColumnType("bigint");
    }
}
