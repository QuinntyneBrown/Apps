using Microsoft.EntityFrameworkCore;
using Businesses.Core.Models;

namespace Businesses.Core;

public interface IBusinessesDbContext
{
    DbSet<Business> Businesses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
