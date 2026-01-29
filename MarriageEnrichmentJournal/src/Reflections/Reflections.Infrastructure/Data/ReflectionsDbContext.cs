using Reflections.Core;
using Reflections.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Reflections.Infrastructure.Data;

public class ReflectionsDbContext : DbContext, IReflectionsDbContext
{
    public ReflectionsDbContext(DbContextOptions<ReflectionsDbContext> options) : base(options)
    {
    }

    public DbSet<Reflection> Reflections { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reflection>(entity =>
        {
            entity.ToTable("Reflections");
            entity.HasKey(e => e.ReflectionId);
            entity.Property(e => e.Topic).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
