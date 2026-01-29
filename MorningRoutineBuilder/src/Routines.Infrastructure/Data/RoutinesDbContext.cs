using Routines.Core;
using Routines.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Routines.Infrastructure.Data;

public class RoutinesDbContext : DbContext, IRoutinesDbContext
{
    public RoutinesDbContext(DbContextOptions<RoutinesDbContext> options) : base(options) { }
    public DbSet<Routine> Routines { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Routine>(e => {
            e.ToTable("Routines");
            e.HasKey(x => x.RoutineId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
