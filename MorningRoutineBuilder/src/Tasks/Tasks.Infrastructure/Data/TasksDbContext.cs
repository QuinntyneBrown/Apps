using Tasks.Core;
using Tasks.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Infrastructure.Data;

public class TasksDbContext : DbContext, ITasksDbContext
{
    public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }
    public DbSet<Task> Tasks { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>(e => {
            e.ToTable("Tasks");
            e.HasKey(x => x.TaskId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
