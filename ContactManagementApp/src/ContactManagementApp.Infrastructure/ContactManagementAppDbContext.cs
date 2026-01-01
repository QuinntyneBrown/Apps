using ContactManagementApp.Core;
using ContactManagementApp.Core.Model.UserAggregate;
using ContactManagementApp.Core.Model.UserAggregate.Entities;
using ContactManagementApp.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementApp.Infrastructure;

public class ContactManagementAppDbContext : DbContext, IContactManagementAppContext
{
    public ContactManagementAppDbContext(DbContextOptions<ContactManagementAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
    }
}
