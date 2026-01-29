using Tags.Core;
using Tags.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Tags.Infrastructure.Data;

public class TagsDbContext : DbContext, ITagsDbContext
{
    public TagsDbContext(DbContextOptions<TagsDbContext> options) : base(options)
    {
    }

    public DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tags");
            entity.HasKey(e => e.TagId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => new { e.UserId, e.Name }).IsUnique();
        });
    }
}
