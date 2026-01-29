using Microsoft.EntityFrameworkCore;
using Insurance.Core;
using Insurance.Core.Models;

namespace Insurance.Infrastructure.Data;

public class InsuranceDbContext : DbContext, IInsuranceDbContext
{
    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options) { }

    public DbSet<InsuranceInfo> InsuranceInfos => Set<InsuranceInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InsuranceInfo>(entity =>
        {
            entity.HasKey(e => e.InsuranceInfoId);
            entity.Property(e => e.InsuranceCompany).HasMaxLength(200).IsRequired();
            entity.Property(e => e.PolicyNumber).HasMaxLength(100).IsRequired();
            entity.Property(e => e.PolicyHolder).HasMaxLength(200).IsRequired();
            entity.Property(e => e.AgentName).HasMaxLength(200);
            entity.Property(e => e.AgentPhone).HasMaxLength(20);
            entity.Property(e => e.CompanyPhone).HasMaxLength(20);
            entity.Property(e => e.ClaimsPhone).HasMaxLength(20);
            entity.Property(e => e.CoverageType).HasMaxLength(100);
            entity.Property(e => e.Deductible).HasPrecision(12, 2);
            entity.Property(e => e.Premium).HasPrecision(12, 2);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.HasIndex(e => e.TenantId);
            entity.HasIndex(e => e.VehicleId);
        });
    }
}
