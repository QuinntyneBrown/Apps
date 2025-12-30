// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CouplesGoalTracker system.
/// </summary>
public class CouplesGoalTrackerContext : DbContext, ICouplesGoalTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CouplesGoalTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CouplesGoalTrackerContext(DbContextOptions<CouplesGoalTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Progress> Progresses { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CouplesGoalTrackerContext).Assembly);
    }
}
