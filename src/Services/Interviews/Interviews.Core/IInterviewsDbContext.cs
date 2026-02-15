using Microsoft.EntityFrameworkCore;
using Interviews.Core.Models;

namespace Interviews.Core;

public interface IInterviewsDbContext
{
    DbSet<Interview> Interviews { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
