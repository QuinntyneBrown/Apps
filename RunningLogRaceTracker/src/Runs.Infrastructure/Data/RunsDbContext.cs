using Microsoft.EntityFrameworkCore;
using Runs.Core;
using Runs.Core.Models;

namespace Runs.Infrastructure.Data;

public class RunsDbContext : DbContext, IRunsDbContext
{
    public RunsDbContext(DbContextOptions<RunsDbContext> options) : base(options) { }

    public DbSet<Run> Runs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Run>(entity =>
        {
            entity.ToTable("Runs");
            entity.HasKey(e => e.RunId);
            entity.Property(e => e.Distance).HasPrecision(10, 2);
            entity.Property(e => e.AveragePace).HasPrecision(10, 2);
            entity.Property(e => e.Route).HasMaxLength(500);
            entity.Property(e => e.Weather).HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
