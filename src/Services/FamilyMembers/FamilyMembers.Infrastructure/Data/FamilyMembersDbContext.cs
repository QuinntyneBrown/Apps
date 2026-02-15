using FamilyMembers.Core;
using FamilyMembers.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyMembers.Infrastructure.Data;

public class FamilyMembersDbContext : DbContext, IFamilyMembersDbContext
{
    public FamilyMembersDbContext(DbContextOptions<FamilyMembersDbContext> options) : base(options)
    {
    }

    public DbSet<FamilyMember> FamilyMembers => Set<FamilyMember>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FamilyMember>(entity =>
        {
            entity.ToTable("FamilyMembers", "familymembers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.UserId);
        });
    }
}
