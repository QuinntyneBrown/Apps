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
            entity.Property(e => e.InterestRate).HasPrecision(5, 2);
            entity.Property(e => e.MonthlyPayment).HasPrecision(18, 2);
            entity.Property(e => e.TotalCost).HasPrecision(18, 2);
            entity.Property(e => e.OriginationFee).HasPrecision(18, 2);
        });
    }
}
