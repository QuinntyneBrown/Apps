using Projections.Core;
using Projections.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Projections.Infrastructure.Data;
public class ProjectionsDbContext : DbContext, IProjectionsDbContext
{
    public ProjectionsDbContext(DbContextOptions<ProjectionsDbContext> options) : base(options) { }
    public DbSet<Projection> Projections => Set<Projection>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Projection>(entity => { entity.ToTable("Projections", "projections"); entity.HasKey(e => e.Id); entity.Property(e => e.ProjectedAmount).HasPrecision(18, 2); entity.Property(e => e.AssumedReturnRate).HasPrecision(5, 4); entity.HasIndex(e => e.PlanId); });
    }
}
