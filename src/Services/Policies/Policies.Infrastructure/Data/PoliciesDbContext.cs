using Microsoft.EntityFrameworkCore;
using Policies.Core;
using Policies.Core.Models;

namespace Policies.Infrastructure.Data;

public class PoliciesDbContext : DbContext, IPoliciesDbContext
{
    public PoliciesDbContext(DbContextOptions<PoliciesDbContext> options) : base(options) { }

    public DbSet<Policy> Policies => Set<Policy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId);
            entity.Property(e => e.Provider).HasMaxLength(200).IsRequired();
            entity.Property(e => e.PolicyNumber).HasMaxLength(100).IsRequired();
            entity.Property(e => e.EmergencyPhone).HasMaxLength(20).IsRequired();
            entity.Property(e => e.AnnualPremium).HasPrecision(12, 2);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.HasIndex(e => e.TenantId);
            entity.HasIndex(e => e.VehicleId);
        });
    }
}
