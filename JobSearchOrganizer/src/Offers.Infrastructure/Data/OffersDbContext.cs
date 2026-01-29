using Offers.Core;
using Offers.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Offers.Infrastructure.Data;

public class OffersDbContext : DbContext, IOffersDbContext
{
    public OffersDbContext(DbContextOptions<OffersDbContext> options) : base(options)
    {
    }

    public DbSet<Offer> Offers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.ToTable("Offers");
            entity.HasKey(e => e.OfferId);
            entity.Property(e => e.SalaryAmount).HasPrecision(18, 2);
            entity.Property(e => e.SigningBonus).HasPrecision(18, 2);
            entity.Property(e => e.AnnualBonus).HasPrecision(18, 2);
            entity.Property(e => e.Status).HasConversion<string>();
        });
    }
}
