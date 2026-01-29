using Recipes.Core;
using Recipes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Recipes.Infrastructure.Data;

public class RecipesDbContext : DbContext, IRecipesDbContext
{
    public RecipesDbContext(DbContextOptions<RecipesDbContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.ToTable("Recipes");
            entity.HasKey(e => e.RecipeId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Ingredients).IsRequired();
            entity.Property(e => e.Instructions).IsRequired();
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
