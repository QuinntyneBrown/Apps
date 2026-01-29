using Celebrations.Core;
using Celebrations.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Celebrations.Infrastructure.Data;

public class CelebrationsDbContext : DbContext, ICelebrationsDbContext
{
    public CelebrationsDbContext(DbContextOptions<CelebrationsDbContext> options) : base(options)
    {
    }

    public DbSet<Celebration> Celebrations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Celebration>(entity =>
        {
            entity.ToTable("Celebrations");
            entity.HasKey(e => e.CelebrationId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CelebrationType).IsRequired().HasMaxLength(50);
        });
    }
}
