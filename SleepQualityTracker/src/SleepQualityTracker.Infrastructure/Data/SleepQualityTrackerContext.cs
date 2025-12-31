// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SleepQualityTracker.Core;
using Microsoft.EntityFrameworkCore;

using SleepQualityTracker.Core.Model.UserAggregate;
using SleepQualityTracker.Core.Model.UserAggregate.Entities;
namespace SleepQualityTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SleepQualityTracker system.
/// </summary>
public class SleepQualityTrackerContext : DbContext, ISleepQualityTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SleepQualityTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SleepQualityTrackerContext(DbContextOptions<SleepQualityTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<SleepSession> SleepSessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Habit> Habits { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Pattern> Patterns { get; set; } = null!;


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
            modelBuilder.Entity<SleepSession>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Habit>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Pattern>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SleepQualityTrackerContext).Assembly);
    }
}
