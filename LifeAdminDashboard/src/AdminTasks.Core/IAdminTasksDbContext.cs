using Microsoft.EntityFrameworkCore;
using AdminTasks.Core.Models;

namespace AdminTasks.Core;

public interface IAdminTasksDbContext
{
    DbSet<AdminTask> AdminTasks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
