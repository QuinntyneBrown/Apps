using Gratitudes.Core;
using Gratitudes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Gratitudes.Infrastructure.Data;

public class GratitudesDbContext : DbContext, IGratitudesDbContext
{
    public GratitudesDbContext(DbContextOptions<GratitudesDbContext> options) : base(options)
    {
    }

    public DbSet<Gratitude> Gratitudes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Gratitude>(entity =>
        {
            entity.ToTable("Gratitudes");
            entity.HasKey(e => e.GratitudeId);
            entity.Property(e => e.Content).IsRequired();
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
