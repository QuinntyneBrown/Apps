using Microsoft.EntityFrameworkCore;
using Products.Core;

namespace Products.Infrastructure.Data;

public class ProductsDbContext : DbContext, IProductsDbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);
    }
}
