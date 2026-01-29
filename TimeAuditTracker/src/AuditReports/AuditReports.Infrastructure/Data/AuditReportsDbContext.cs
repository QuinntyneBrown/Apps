using AuditReports.Core;
using AuditReports.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditReports.Infrastructure.Data;

public class AuditReportsDbContext : DbContext, IAuditReportsDbContext
{
    public AuditReportsDbContext(DbContextOptions<AuditReportsDbContext> options) : base(options)
    {
    }

    public DbSet<AuditReport> AuditReports { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuditReport>(entity =>
        {
            entity.ToTable("AuditReports");
            entity.HasKey(e => e.ReportId);
            entity.HasIndex(e => new { e.TenantId, e.UserId, e.StartDate, e.EndDate });
        });
    }
}
