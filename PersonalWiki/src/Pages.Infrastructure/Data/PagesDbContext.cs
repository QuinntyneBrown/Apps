using Pages.Core;
using Pages.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Pages.Infrastructure.Data;

public class PagesDbContext : DbContext, IPagesDbContext
{
    public PagesDbContext(DbContextOptions<PagesDbContext> options) : base(options)
    {
    }

    public DbSet<WikiPage> WikiPages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WikiPage>(entity =>
        {
            entity.ToTable("WikiPages");
            entity.HasKey(e => e.WikiPageId);
            entity.Property(e => e.Title).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Slug).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Status).HasMaxLength(50).IsRequired();
            entity.HasIndex(e => new { e.TenantId, e.Slug }).IsUnique();
        });
    }
}
