using Microsoft.EntityFrameworkCore;
using Policies.Core.Models;

namespace Policies.Core;

public interface IPoliciesDbContext
{
    DbSet<Policy> Policies { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
