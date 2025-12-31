// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FinancialGoalTracker.Core;
using Microsoft.EntityFrameworkCore;

using FinancialGoalTracker.Core.Model.UserAggregate;
using FinancialGoalTracker.Core.Model.UserAggregate.Entities;
namespace FinancialGoalTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FinancialGoalTracker system.
/// </summary>
public class FinancialGoalTrackerContext : DbContext, IFinancialGoalTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FinancialGoalTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FinancialGoalTrackerContext(DbContextOptions<FinancialGoalTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Contribution> Contributions { get; set; } = null!;


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
            modelBuilder.Entity<Goal>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Milestone>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Contribution>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialGoalTrackerContext).Assembly);
    }
}
