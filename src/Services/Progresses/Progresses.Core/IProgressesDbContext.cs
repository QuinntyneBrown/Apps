using Microsoft.EntityFrameworkCore;
using Progresses.Core.Models;

namespace Progresses.Core;

public interface IProgressesDbContext
{
    DbSet<Progress> Progresses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
