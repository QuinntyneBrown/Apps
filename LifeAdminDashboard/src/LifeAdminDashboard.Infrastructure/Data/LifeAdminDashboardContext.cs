// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LifeAdminDashboard.Core;
using Microsoft.EntityFrameworkCore;

using LifeAdminDashboard.Core.Model.UserAggregate;
using LifeAdminDashboard.Core.Model.UserAggregate.Entities;
namespace LifeAdminDashboard.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the LifeAdminDashboard system.
/// </summary>
public class LifeAdminDashboardContext : DbContext, ILifeAdminDashboardContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="LifeAdminDashboardContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public LifeAdminDashboardContext(DbContextOptions<LifeAdminDashboardContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<AdminTask> Tasks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Renewal> Renewals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Deadline> Deadlines { get; set; } = null!;


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
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<AdminTask>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Renewal>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Deadline>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LifeAdminDashboardContext).Assembly);
    }
}
