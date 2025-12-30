// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FinancialGoalTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FinancialGoalTracker system.
/// </summary>
public class FinancialGoalTrackerContext : DbContext, IFinancialGoalTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FinancialGoalTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FinancialGoalTrackerContext(DbContextOptions<FinancialGoalTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Contribution> Contributions { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialGoalTrackerContext).Assembly);
    }
}
