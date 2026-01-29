using Referrals.Core;
using Referrals.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Referrals.Infrastructure.Data;

public class ReferralsDbContext : DbContext, IReferralsDbContext
{
    public ReferralsDbContext(DbContextOptions<ReferralsDbContext> options) : base(options) { }

    public DbSet<Referral> Referrals { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Referral>(entity =>
        {
            entity.ToTable("Referrals");
            entity.HasKey(e => e.ReferralId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
