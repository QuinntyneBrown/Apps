using Microsoft.EntityFrameworkCore;
using Pages.Core.Models;

namespace Pages.Core;

public interface IPagesDbContext
{
    DbSet<WikiPage> WikiPages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
