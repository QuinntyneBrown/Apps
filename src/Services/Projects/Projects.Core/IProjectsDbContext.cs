using Microsoft.EntityFrameworkCore;
using Projects.Core.Models;

namespace Projects.Core;

public interface IProjectsDbContext
{
    DbSet<Project> Projects { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
