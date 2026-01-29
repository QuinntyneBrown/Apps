using Achievements.Core;
using Achievements.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Achievements.Infrastructure.Data;

public class AchievementsDbContext : DbContext, IAchievementsDbContext
{
    public AchievementsDbContext(DbContextOptions<AchievementsDbContext> options) : base(options)
    {
    }

    public DbSet<Achievement> Achievements { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.ToTable("Achievements");
            entity.HasKey(e => e.AchievementId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
        });
    }
}
