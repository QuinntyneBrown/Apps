using Beneficiaries.Core;
using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Beneficiaries.Infrastructure.Data;

public class BeneficiariesDbContext : DbContext, IBeneficiariesDbContext
{
    public BeneficiariesDbContext(DbContextOptions<BeneficiariesDbContext> options) : base(options) { }
    public DbSet<Beneficiary> Beneficiaries => Set<Beneficiary>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.ToTable("Beneficiaries", "beneficiaries");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.HasIndex(e => e.UserId);
        });
    }
}
