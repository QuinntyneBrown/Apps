using Donations.Core;
using Donations.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Donations.Infrastructure.Data;

public class DonationsDbContext : DbContext, IDonationsDbContext
{
    public DonationsDbContext(DbContextOptions<DonationsDbContext> options) : base(options)
    {
    }

    public DbSet<Donation> Donations => Set<Donation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.ToTable("Donations", "donations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.OrganizationId);
        });
    }
}
