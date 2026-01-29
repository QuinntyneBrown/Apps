using Microsoft.EntityFrameworkCore;
using ReturnWindows.Core.Models;

namespace ReturnWindows.Core;

public interface IReturnWindowsDbContext
{
    DbSet<ReturnWindow> ReturnWindows { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
