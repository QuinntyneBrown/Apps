// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using Microsoft.EntityFrameworkCore;

using KidsActivitySportsTracker.Core.Models.UserAggregate;
using KidsActivitySportsTracker.Core.Models.UserAggregate.Entities;
namespace KidsActivitySportsTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the KidsActivitySportsTracker system.
/// </summary>
public class KidsActivitySportsTrackerContext : DbContext, IKidsActivitySportsTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="KidsActivitySportsTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public KidsActivitySportsTrackerContext(DbContextOptions<KidsActivitySportsTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Activity> Activities { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Schedule> Schedules { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Carpool> Carpools { get; set; } = null!;


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
            modelBuilder.Entity<Activity>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Schedule>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Carpool>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KidsActivitySportsTrackerContext).Assembly);
    }
}
