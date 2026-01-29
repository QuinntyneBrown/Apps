using Categories.Core;
using Categories.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Categories.Infrastructure.Data;

public class CategoriesDbContext : DbContext, ICategoriesDbContext
{
    public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options) : base(options)
    {
    }

    public DbSet<WikiCategory> WikiCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WikiCategory>(entity =>
        {
            entity.ToTable("WikiCategories");
            entity.HasKey(e => e.WikiCategoryId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.HasIndex(e => new { e.TenantId, e.Name }).IsUnique();
        });
    }
}
