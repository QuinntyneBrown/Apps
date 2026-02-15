using Schedules.Core;
using Schedules.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Schedules.Infrastructure.Data;

public class SchedulesDbContext : DbContext, ISchedulesDbContext
{
    public SchedulesDbContext(DbContextOptions<SchedulesDbContext> options) : base(options)
    {
    }

    public DbSet<Schedule> Schedules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedules");
            entity.HasKey(e => e.ScheduleId);
        });
    }
}
