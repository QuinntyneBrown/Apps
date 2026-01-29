using Claims.Core;
using Claims.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Data;

public class ClaimsDbContext : DbContext, IClaimsDbContext
{
    public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options) : base(options)
    {
    }

    public DbSet<Claim> Claims { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.ToTable("Claims");
            entity.HasKey(e => e.ClaimId);
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(50);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.HasIndex(e => new { e.TenantId, e.Status });
        });
    }
}
