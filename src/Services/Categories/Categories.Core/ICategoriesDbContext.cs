using Microsoft.EntityFrameworkCore;
using Categories.Core.Models;

namespace Categories.Core;

public interface ICategoriesDbContext
{
    DbSet<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
