using Deductions.Core;
using Deductions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Deductions.Infrastructure.Data;

public class DeductionsDbContext : DbContext, IDeductionsDbContext
{
    public DeductionsDbContext(DbContextOptions<DeductionsDbContext> options) : base(options)
    {
    }

    public DbSet<Deduction> Deductions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Deduction>(entity =>
        {
            entity.ToTable("Deductions");
            entity.HasKey(e => e.DeductionId);
            entity.Property(e => e.Description).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.Category).HasConversion<string>();
            entity.HasIndex(e => new { e.TenantId, e.TaxYearId });
        });
    }
}
