using Plans.Core;
using Plans.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Plans.Infrastructure.Data;
public class PlansDbContext : DbContext, IPlansDbContext
{
    public PlansDbContext(DbContextOptions<PlansDbContext> options) : base(options) { }
    public DbSet<Plan> Plans => Set<Plan>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Plan>(entity => { entity.ToTable("Plans", "plans"); entity.HasKey(e => e.Id); entity.Property(e => e.Name).HasMaxLength(200).IsRequired(); entity.Property(e => e.TargetAmount).HasPrecision(18, 2); entity.Property(e => e.CurrentBalance).HasPrecision(18, 2); entity.HasIndex(e => e.UserId); });
    }
}
