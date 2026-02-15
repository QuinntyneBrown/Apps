using DateManagement.Core;
using DateManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DateManagement.Infrastructure.Data;

public class DateManagementDbContext : DbContext, IDateManagementDbContext
{
    public DateManagementDbContext(DbContextOptions<DateManagementDbContext> options) : base(options)
    {
    }

    public DbSet<ImportantDate> ImportantDates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ImportantDate>(entity =>
        {
            entity.ToTable("ImportantDates");
            entity.HasKey(e => e.ImportantDateId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        });
    }
}
