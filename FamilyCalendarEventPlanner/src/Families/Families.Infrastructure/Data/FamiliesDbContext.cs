using Families.Core;
using Families.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Families.Infrastructure.Data;

public class FamiliesDbContext : DbContext, IFamiliesDbContext
{
    public FamiliesDbContext(DbContextOptions<FamiliesDbContext> options) : base(options)
    {
    }

    public DbSet<FamilyMember> FamilyMembers { get; set; } = null!;
    public DbSet<Household> Households { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FamilyMember>(entity =>
        {
            entity.ToTable("FamilyMembers");
            entity.HasKey(e => e.FamilyMemberId);
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Relationship).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.HasIndex(e => new { e.TenantId, e.HouseholdId });
        });

        modelBuilder.Entity<Household>(entity =>
        {
            entity.ToTable("Households");
            entity.HasKey(e => e.HouseholdId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.ZipCode).HasMaxLength(20);
            entity.HasIndex(e => e.TenantId);
        });
    }
}
