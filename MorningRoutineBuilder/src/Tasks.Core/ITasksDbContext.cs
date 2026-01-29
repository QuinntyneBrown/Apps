using Microsoft.EntityFrameworkCore;
using Tasks.Core.Models;

namespace Tasks.Core;

public interface ITasksDbContext
{
    DbSet<Task> Tasks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
