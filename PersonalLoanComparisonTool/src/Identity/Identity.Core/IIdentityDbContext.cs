using Microsoft.EntityFrameworkCore;
using Identity.Core.Models.UserAggregate;
using Identity.Core.Models.UserAggregate.Entities;

namespace Identity.Core;

public interface IIdentityDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
