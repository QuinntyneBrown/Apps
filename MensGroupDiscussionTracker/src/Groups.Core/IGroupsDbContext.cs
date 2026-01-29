using Microsoft.EntityFrameworkCore;
using Groups.Core.Models;

namespace Groups.Core;

public interface IGroupsDbContext
{
    DbSet<Group> Groups { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
