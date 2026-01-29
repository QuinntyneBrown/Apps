using Microsoft.EntityFrameworkCore;
using ServiceRecords.Core.Models;

namespace ServiceRecords.Core;

public interface IServiceRecordsDbContext
{
    DbSet<ServiceRecord> ServiceRecords { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
