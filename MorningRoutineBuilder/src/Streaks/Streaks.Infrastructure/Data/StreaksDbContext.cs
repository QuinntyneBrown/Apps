using Streaks.Core;
using Streaks.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Streaks.Infrastructure.Data;

public class StreaksDbContext : DbContext, IStreaksDbContext
{
    public StreaksDbContext(DbContextOptions<StreaksDbContext> options) : base(options) { }
    public DbSet<Streak> Streaks { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Streak>(e => {
            e.ToTable("Streaks");
            e.HasKey(x => x.StreakId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
