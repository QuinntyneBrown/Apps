using Activities.Core;
using Activities.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Activities.Infrastructure.Data;

public class ActivitiesDbContext : DbContext, IActivitiesDbContext
{
    public ActivitiesDbContext(DbContextOptions<ActivitiesDbContext> options) : base(options)
    {
    }

    public DbSet<Activity> Activities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activities");
            entity.HasKey(e => e.ActivityId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Cost).HasPrecision(18, 2);
            entity.Property(e => e.Type).HasConversion<string>();
        });
    }
}
