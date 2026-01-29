using Tagging.Core;
using Tagging.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Tagging.Infrastructure.Data;

public class TaggingDbContext : DbContext, ITaggingDbContext
{
    public TaggingDbContext(DbContextOptions<TaggingDbContext> options) : base(options) { }

    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<PersonTag> PersonTags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>(e => { e.ToTable("Tags"); e.HasKey(x => x.TagId); e.Property(x => x.Name).HasMaxLength(100); });
        modelBuilder.Entity<PersonTag>(e => { e.ToTable("PersonTags"); e.HasKey(x => x.PersonTagId); e.Property(x => x.PersonName).HasMaxLength(200); });
    }
}
