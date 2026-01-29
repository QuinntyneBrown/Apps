using Microsoft.EntityFrameworkCore;
using Plans.Core.Models;
namespace Plans.Core;
public interface IPlansDbContext
{
    DbSet<Plan> Plans { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
