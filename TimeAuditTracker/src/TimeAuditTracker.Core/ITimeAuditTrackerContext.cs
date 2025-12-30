// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace TimeAuditTracker.Core;

/// <summary>
/// Represents the persistence surface for the TimeAuditTracker system.
/// </summary>
public interface ITimeAuditTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of time blocks.
    /// </summary>
    DbSet<TimeBlock> TimeBlocks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of audit reports.
    /// </summary>
    DbSet<AuditReport> AuditReports { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of goals.
    /// </summary>
    DbSet<Goal> Goals { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
