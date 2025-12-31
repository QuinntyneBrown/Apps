// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;
using Microsoft.EntityFrameworkCore;

using PasswordAccountAuditor.Core.Model.UserAggregate;
using PasswordAccountAuditor.Core.Model.UserAggregate.Entities;
namespace PasswordAccountAuditor.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PasswordAccountAuditor system.
/// </summary>
public class PasswordAccountAuditorContext : DbContext, IPasswordAccountAuditorContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordAccountAuditorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PasswordAccountAuditorContext(DbContextOptions<PasswordAccountAuditorContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Account> Accounts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SecurityAudit> SecurityAudits { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<BreachAlert> BreachAlerts { get; set; } = null!;


    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // Apply tenant filter to User
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);

        // Apply tenant filter to Role
        modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);

        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Account>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<SecurityAudit>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<BreachAlert>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PasswordAccountAuditorContext).Assembly);
    }
}
