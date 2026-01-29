using Contributions.Core;
using Contributions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Contributions.Infrastructure.Data;

public class ContributionsDbContext : DbContext, IContributionsDbContext
{
    public ContributionsDbContext(DbContextOptions<ContributionsDbContext> options) : base(options)
    {
    }

    public DbSet<Contribution> Contributions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contribution>(entity =>
        {
            entity.ToTable("Contributions");
            entity.HasKey(e => e.ContributionId);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.TenantId, e.RegistryItemId });
        });
    }
}
