using ContactManagementApp.Core.Model.UserAggregate;
using ContactManagementApp.Core.Model.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementApp.Core;

public interface IContactManagementAppContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
