// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TimeAuditTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace TimeAuditTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the TimeAuditTracker system.
/// </summary>
public class TimeAuditTrackerContext : DbContext, ITimeAuditTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TimeAuditTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public TimeAuditTrackerContext(DbContextOptions<TimeAuditTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<TimeBlock> TimeBlocks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<AuditReport> AuditReports { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimeAuditTrackerContext).Assembly);
    }
}
