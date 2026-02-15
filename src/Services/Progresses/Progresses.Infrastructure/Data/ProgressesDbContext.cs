using Progresses.Core;
using Progresses.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Progresses.Infrastructure.Data;

public class ProgressesDbContext : DbContext, IProgressesDbContext
{
    public ProgressesDbContext(DbContextOptions<ProgressesDbContext> options) : base(options)
    {
    }

    public DbSet<Progress> Progresses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Progress>(entity =>
        {
            entity.ToTable("Progresses");
            entity.HasKey(e => e.ProgressId);
            entity.Property(e => e.PercentageComplete).HasPrecision(5, 2);
            entity.HasIndex(e => e.GoalId);
        });
    }
}
