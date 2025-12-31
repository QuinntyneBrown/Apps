// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using SportsTeamFollowingTracker.Core;

using SportsTeamFollowingTracker.Core.Model.UserAggregate;
using SportsTeamFollowingTracker.Core.Model.UserAggregate.Entities;
namespace SportsTeamFollowingTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SportsTeamFollowingTracker system.
/// </summary>
public class SportsTeamFollowingTrackerContext : DbContext, ISportsTeamFollowingTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SportsTeamFollowingTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SportsTeamFollowingTrackerContext(DbContextOptions<SportsTeamFollowingTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Team> Teams { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Game> Games { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Season> Seasons { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Statistic> Statistics { get; set; } = null!;


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
            modelBuilder.Entity<Team>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Game>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Season>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Statistic>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SportsTeamFollowingTrackerContext).Assembly);
    }
}
