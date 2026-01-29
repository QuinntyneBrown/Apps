using Microsoft.EntityFrameworkCore;
using AuditReports.Core.Models;

namespace AuditReports.Core;

public interface IAuditReportsDbContext
{
    DbSet<AuditReport> AuditReports { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
