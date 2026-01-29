using ReturnWindows.Core;
using ReturnWindows.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ReturnWindows.Infrastructure.Data;

public class ReturnWindowsDbContext : DbContext, IReturnWindowsDbContext
{
    public ReturnWindowsDbContext(DbContextOptions<ReturnWindowsDbContext> options) : base(options) { }
    public DbSet<ReturnWindow> ReturnWindows { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ReturnWindow>(entity => { entity.ToTable("ReturnWindows"); entity.HasKey(e => e.ReturnWindowId); });
    }
}
