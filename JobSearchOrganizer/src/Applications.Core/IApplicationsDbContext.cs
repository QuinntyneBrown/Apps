using Microsoft.EntityFrameworkCore;
using Applications.Core.Models;

namespace Applications.Core;

public interface IApplicationsDbContext
{
    DbSet<Application> Applications { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
