using Categories.Core;
using Categories.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Categories.Infrastructure.Data;

public class CategoriesDbContext : DbContext, ICategoriesDbContext
{
    public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Color).HasMaxLength(20);
            entity.HasIndex(e => new { e.TenantId, e.Name }).IsUnique();
        });
    }
}
