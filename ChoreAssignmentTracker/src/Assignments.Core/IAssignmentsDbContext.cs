using Microsoft.EntityFrameworkCore;
using Assignments.Core.Models;

namespace Assignments.Core;

public interface IAssignmentsDbContext
{
    DbSet<Assignment> Assignments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
