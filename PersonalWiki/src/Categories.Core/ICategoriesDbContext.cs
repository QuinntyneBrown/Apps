using Microsoft.EntityFrameworkCore;
using Categories.Core.Models;

namespace Categories.Core;

public interface ICategoriesDbContext
{
    DbSet<WikiCategory> WikiCategories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
