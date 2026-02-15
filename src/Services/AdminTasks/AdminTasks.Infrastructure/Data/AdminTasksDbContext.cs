using AdminTasks.Core;
using AdminTasks.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminTasks.Infrastructure.Data;

public class AdminTasksDbContext : DbContext, IAdminTasksDbContext
{
    public AdminTasksDbContext(DbContextOptions<AdminTasksDbContext> options) : base(options)
    {
    }

    public DbSet<AdminTask> AdminTasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AdminTask>(entity =>
        {
            entity.ToTable("AdminTasks");
            entity.HasKey(e => e.AdminTaskId);
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Category).HasConversion<string>().HasMaxLength(50);
            entity.Property(e => e.Priority).HasConversion<string>().HasMaxLength(50);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
