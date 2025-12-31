// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using RunningLogRaceTracker.Core;

using RunningLogRaceTracker.Core.Model.UserAggregate;
using RunningLogRaceTracker.Core.Model.UserAggregate.Entities;
namespace RunningLogRaceTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the RunningLogRaceTracker system.
/// </summary>
public class RunningLogRaceTrackerContext : DbContext, IRunningLogRaceTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RunningLogRaceTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RunningLogRaceTrackerContext(DbContextOptions<RunningLogRaceTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Run> Runs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Race> Races { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TrainingPlan> TrainingPlans { get; set; } = null!;


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
            modelBuilder.Entity<Run>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Race>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TrainingPlan>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RunningLogRaceTrackerContext).Assembly);
    }
}
