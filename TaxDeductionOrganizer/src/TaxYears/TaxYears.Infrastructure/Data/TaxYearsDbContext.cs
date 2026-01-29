using TaxYears.Core;
using TaxYears.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TaxYears.Infrastructure.Data;

public class TaxYearsDbContext : DbContext, ITaxYearsDbContext
{
    public TaxYearsDbContext(DbContextOptions<TaxYearsDbContext> options) : base(options)
    {
    }

    public DbSet<TaxYear> TaxYears { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaxYear>(entity =>
        {
            entity.ToTable("TaxYears");
            entity.HasKey(e => e.TaxYearId);
            entity.HasIndex(e => new { e.TenantId, e.UserId, e.Year }).IsUnique();
        });
    }
}
