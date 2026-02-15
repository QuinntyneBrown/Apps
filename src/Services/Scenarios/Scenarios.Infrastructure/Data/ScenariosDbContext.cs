using Scenarios.Core;
using Scenarios.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Scenarios.Infrastructure.Data;

public class ScenariosDbContext : DbContext, IScenariosDbContext
{
    public ScenariosDbContext(DbContextOptions<ScenariosDbContext> options) : base(options)
    {
    }

    public DbSet<RetirementScenario> RetirementScenarios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RetirementScenario>(entity =>
        {
            entity.ToTable("RetirementScenarios");
            entity.HasKey(e => e.RetirementScenarioId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CurrentSavings).HasPrecision(18, 2);
            entity.Property(e => e.AnnualContribution).HasPrecision(18, 2);
            entity.Property(e => e.ExpectedReturnRate).HasPrecision(5, 2);
            entity.Property(e => e.InflationRate).HasPrecision(5, 2);
        });
    }
}
