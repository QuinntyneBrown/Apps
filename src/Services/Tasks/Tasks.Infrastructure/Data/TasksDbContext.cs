using Tasks.Core;
using Tasks.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Infrastructure.Data;

public class TasksDbContext : DbContext, ITasksDbContext
{
    public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
    {
    }

    public DbSet<PriorityTask> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PriorityTask>(entity =>
        {
            entity.ToTable("PriorityTasks");
            entity.HasKey(e => e.PriorityTaskId);
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Urgency).HasConversion<string>();
            entity.Property(e => e.Importance).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
