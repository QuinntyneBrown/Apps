using Letters.Core;
using Letters.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Letters.Infrastructure.Data;

public class LettersDbContext : DbContext, ILettersDbContext
{
    public LettersDbContext(DbContextOptions<LettersDbContext> options) : base(options)
    {
    }

    public DbSet<Letter> Letters { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Letter>(entity =>
        {
            entity.ToTable("Letters");
            entity.HasKey(e => e.LetterId);
            entity.Property(e => e.Subject).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.DeliveryStatus).HasConversion<string>().HasMaxLength(50);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.HasIndex(e => e.ScheduledDeliveryDate);
        });
    }
}
