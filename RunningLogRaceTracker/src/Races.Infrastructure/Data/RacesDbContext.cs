using Microsoft.EntityFrameworkCore;
using Races.Core;
using Races.Core.Models;

namespace Races.Infrastructure.Data;

public class RacesDbContext : DbContext, IRacesDbContext
{
    public RacesDbContext(DbContextOptions<RacesDbContext> options) : base(options)
    {
    }

    public DbSet<Race> Races { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Race>(entity =>
        {
            entity.ToTable("Races");
            entity.HasKey(e => e.RaceId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Location).HasMaxLength(300);
            entity.Property(e => e.Distance).HasPrecision(10, 2);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
