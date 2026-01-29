using Ideas.Core;
using Ideas.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Ideas.Infrastructure.Data;

public class IdeasDbContext : DbContext, IIdeasDbContext
{
    public IdeasDbContext(DbContextOptions<IdeasDbContext> options) : base(options)
    {
    }

    public DbSet<Idea> Ideas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Idea>(entity =>
        {
            entity.ToTable("Ideas");
            entity.HasKey(e => e.IdeaId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.EstimatedPrice).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.TenantId, e.RecipientId });
        });
    }
}
