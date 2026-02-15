using Manuals.Core;
using Manuals.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Manuals.Infrastructure.Data;

public class ManualsDbContext : DbContext, IManualsDbContext
{
    public ManualsDbContext(DbContextOptions<ManualsDbContext> options) : base(options)
    {
    }

    public DbSet<Manual> Manuals { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Manual>(entity =>
        {
            entity.ToTable("Manuals");
            entity.HasKey(e => e.ManualId);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.FileUrl).HasMaxLength(500);
            entity.Property(e => e.FileType).HasMaxLength(50);
            entity.HasIndex(e => new { e.TenantId, e.ApplianceId });
        });
    }
}
