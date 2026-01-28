using Reminders.Core;
using Reminders.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Reminders.Infrastructure.Data;

public class RemindersDbContext : DbContext, IRemindersDbContext
{
    public RemindersDbContext(DbContextOptions<RemindersDbContext> options) : base(options)
    {
    }

    public DbSet<Reminder> Reminders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.ToTable("Reminders");
            entity.HasKey(e => e.ReminderId);
            entity.Property(e => e.Message).HasMaxLength(500);
        });
    }
}
