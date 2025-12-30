// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FocusSessionTracker system.
/// </summary>
public class FocusSessionTrackerContext : DbContext, IFocusSessionTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FocusSessionTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FocusSessionTrackerContext(DbContextOptions<FocusSessionTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<FocusSession> Sessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Distraction> Distractions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SessionAnalytics> Analytics { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FocusSessionTrackerContext).Assembly);
    }
}
