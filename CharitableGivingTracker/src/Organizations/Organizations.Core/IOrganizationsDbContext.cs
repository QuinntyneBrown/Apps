using Microsoft.EntityFrameworkCore;
using Organizations.Core.Models;

namespace Organizations.Core;

public interface IOrganizationsDbContext
{
    DbSet<Organization> Organizations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
