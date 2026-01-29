using Revisions.Core;
using Revisions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Revisions.Infrastructure.Data;

public class RevisionsDbContext : DbContext, IRevisionsDbContext
{
    public RevisionsDbContext(DbContextOptions<RevisionsDbContext> options) : base(options)
    {
    }

    public DbSet<PageRevision> PageRevisions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PageRevision>(entity =>
        {
            entity.ToTable("PageRevisions");
            entity.HasKey(e => e.PageRevisionId);
            entity.Property(e => e.ChangeDescription).HasMaxLength(500);
            entity.HasIndex(e => new { e.TenantId, e.PageId, e.VersionNumber }).IsUnique();
        });
    }
}
