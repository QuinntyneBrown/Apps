using Microsoft.EntityFrameworkCore;

namespace Products.Core;

public interface IProductsDbContext
{
    DbSet<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
