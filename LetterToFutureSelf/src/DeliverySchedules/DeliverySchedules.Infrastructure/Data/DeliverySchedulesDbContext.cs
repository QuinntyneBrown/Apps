using DeliverySchedules.Core;
using DeliverySchedules.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliverySchedules.Infrastructure.Data;

public class DeliverySchedulesDbContext : DbContext, IDeliverySchedulesDbContext
{
    public DeliverySchedulesDbContext(DbContextOptions<DeliverySchedulesDbContext> options) : base(options)
    {
    }

    public DbSet<DeliverySchedule> DeliverySchedules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeliverySchedule>(entity =>
        {
            entity.ToTable("DeliverySchedules");
            entity.HasKey(e => e.DeliveryScheduleId);
            entity.Property(e => e.DeliveryMethod).HasMaxLength(50).IsRequired();
            entity.Property(e => e.RecipientContact).HasMaxLength(256);
            entity.HasIndex(e => new { e.TenantId, e.LetterId });
            entity.HasIndex(e => e.ScheduledDateTime);
        });
    }
}
