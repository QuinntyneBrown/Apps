using Registries.Core;
using Registries.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Registries.Infrastructure.Data;

public class RegistriesDbContext : DbContext, IRegistriesDbContext
{
    public RegistriesDbContext(DbContextOptions<RegistriesDbContext> options) : base(options)
    {
    }

    public DbSet<Registry> Registries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Registry>(entity =>
        {
            entity.ToTable("Registries");
            entity.HasKey(e => e.RegistryId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.EventType).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.HasIndex(e => e.ShareCode).IsUnique();
        });
    }
}
