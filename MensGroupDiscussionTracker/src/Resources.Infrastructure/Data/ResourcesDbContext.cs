using Resources.Core;
using Resources.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Resources.Infrastructure.Data;

public class ResourcesDbContext : DbContext, IResourcesDbContext
{
    public ResourcesDbContext(DbContextOptions<ResourcesDbContext> options) : base(options) { }
    public DbSet<Resource> Resources { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>(e => {
            e.ToTable("Resources");
            e.HasKey(x => x.ResourceId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
