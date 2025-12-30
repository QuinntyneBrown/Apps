// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Infrastructure.Data;

/// <summary>
/// Represents the Entity Framework database context for the ChoreAssignmentTracker system.
/// </summary>
public class ChoreAssignmentTrackerContext : DbContext, IChoreAssignmentTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChoreAssignmentTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public ChoreAssignmentTrackerContext(DbContextOptions<ChoreAssignmentTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <summary>
    /// Gets or sets the DbSet of chores.
    /// </summary>
    public DbSet<Chore> Chores { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of assignments.
    /// </summary>
    public DbSet<Assignment> Assignments { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of rewards.
    /// </summary>
    public DbSet<Reward> Rewards { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of family members.
    /// </summary>
    public DbSet<FamilyMember> FamilyMembers { get; set; } = null!;

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Chore>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Assignment>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Reward>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<FamilyMember>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChoreAssignmentTrackerContext).Assembly);
    }
}
