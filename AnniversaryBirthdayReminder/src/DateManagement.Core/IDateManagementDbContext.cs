using Microsoft.EntityFrameworkCore;
using DateManagement.Core.Models;

namespace DateManagement.Core;

public interface IDateManagementDbContext
{
    DbSet<ImportantDate> ImportantDates { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
