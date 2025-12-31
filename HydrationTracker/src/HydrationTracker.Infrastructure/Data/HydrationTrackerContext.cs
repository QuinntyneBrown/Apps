// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using Microsoft.EntityFrameworkCore;

using HydrationTracker.Core.Model.UserAggregate;
using HydrationTracker.Core.Model.UserAggregate.Entities;
namespace HydrationTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HydrationTracker system.
/// </summary>
public class HydrationTrackerContext : DbContext, IHydrationTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HydrationTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HydrationTrackerContext(DbContextOptions<HydrationTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Intake> Intakes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Reminder> Reminders { get; set; } = null!;


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
            modelBuilder.Entity<Intake>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Goal>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Reminder>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HydrationTrackerContext).Assembly);
    }
}
