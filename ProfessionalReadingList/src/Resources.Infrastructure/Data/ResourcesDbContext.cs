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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Resource>(entity =>
        {
            entity.ToTable("Resources");
            entity.HasKey(e => e.ResourceId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
