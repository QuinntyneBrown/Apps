using Intakes.Core;
using Intakes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Intakes.Infrastructure.Data;

public class IntakesDbContext : DbContext, IIntakesDbContext
{
    public IntakesDbContext(DbContextOptions<IntakesDbContext> options) : base(options)
    {
    }

    public DbSet<Intake> Intakes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Intake>(entity =>
        {
            entity.ToTable("Intakes");
            entity.HasKey(e => e.IntakeId);
            entity.Property(e => e.AmountMl).HasPrecision(18, 2);
            entity.Property(e => e.BeverageType).HasConversion<string>();
        });
    }
}
