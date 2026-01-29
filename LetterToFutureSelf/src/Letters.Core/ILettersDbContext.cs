using Microsoft.EntityFrameworkCore;
using Letters.Core.Models;

namespace Letters.Core;

public interface ILettersDbContext
{
    DbSet<Letter> Letters { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
