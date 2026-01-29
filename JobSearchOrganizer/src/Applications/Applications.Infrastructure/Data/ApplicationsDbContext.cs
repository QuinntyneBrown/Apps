using Applications.Core;
using Applications.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Applications.Infrastructure.Data;

public class ApplicationsDbContext : DbContext, IApplicationsDbContext
{
    public ApplicationsDbContext(DbContextOptions<ApplicationsDbContext> options) : base(options)
    {
    }

    public DbSet<Application> Applications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Applications");
            entity.HasKey(e => e.ApplicationId);
            entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(200);
            entity.Property(e => e.SalaryMin).HasPrecision(18, 2);
            entity.Property(e => e.SalaryMax).HasPrecision(18, 2);
            entity.Property(e => e.Status).HasConversion<string>();
        });
    }
}
