using ServiceRecords.Core;
using ServiceRecords.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ServiceRecords.Infrastructure.Data;

public class ServiceRecordsDbContext : DbContext, IServiceRecordsDbContext
{
    public ServiceRecordsDbContext(DbContextOptions<ServiceRecordsDbContext> options) : base(options)
    {
    }

    public DbSet<ServiceRecord> ServiceRecords { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ServiceRecord>(entity =>
        {
            entity.ToTable("ServiceRecords");
            entity.HasKey(e => e.ServiceRecordId);
            entity.Property(e => e.ServiceProvider).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Cost).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.TenantId, e.ApplianceId });
        });
    }
}
