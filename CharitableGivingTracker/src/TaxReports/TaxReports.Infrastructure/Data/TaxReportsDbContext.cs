using TaxReports.Core;
using TaxReports.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TaxReports.Infrastructure.Data;

public class TaxReportsDbContext : DbContext, ITaxReportsDbContext
{
    public TaxReportsDbContext(DbContextOptions<TaxReportsDbContext> options) : base(options)
    {
    }

    public DbSet<TaxReport> TaxReports => Set<TaxReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaxReport>(entity =>
        {
            entity.ToTable("TaxReports", "taxreports");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalDonations).HasPrecision(18, 2);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.TaxYear);
        });
    }
}
