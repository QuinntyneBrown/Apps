using Microsoft.EntityFrameworkCore;
using Books.Core.Models;

namespace Books.Core;

public interface IBooksDbContext
{
    DbSet<Book> Books { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
