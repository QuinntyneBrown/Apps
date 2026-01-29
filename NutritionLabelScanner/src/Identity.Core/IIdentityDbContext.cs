using Microsoft.EntityFrameworkCore;

namespace Identity.Core;

public interface IIdentityDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
