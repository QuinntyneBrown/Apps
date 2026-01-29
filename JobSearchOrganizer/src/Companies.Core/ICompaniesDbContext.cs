using Microsoft.EntityFrameworkCore;
using Companies.Core.Models;

namespace Companies.Core;

public interface ICompaniesDbContext
{
    DbSet<Company> Companies { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
