using Links.Core;
using Links.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Links.Infrastructure.Data;

public class LinksDbContext : DbContext, ILinksDbContext
{
    public LinksDbContext(DbContextOptions<LinksDbContext> options) : base(options)
    {
    }

    public DbSet<PageLink> PageLinks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PageLink>(entity =>
        {
            entity.ToTable("PageLinks");
            entity.HasKey(e => e.PageLinkId);
            entity.Property(e => e.LinkText).HasMaxLength(500);
            entity.HasIndex(e => new { e.TenantId, e.SourcePageId, e.TargetPageId }).IsUnique();
        });
    }
}
