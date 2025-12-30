// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Represents the persistence surface for the PerformanceReviewPrepTool system.
/// </summary>
public interface IPerformanceReviewPrepToolContext
{
    /// <summary>
    /// Gets or sets the DbSet of review periods.
    /// </summary>
    DbSet<ReviewPeriod> ReviewPeriods { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of achievements.
    /// </summary>
    DbSet<Achievement> Achievements { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of goals.
    /// </summary>
    DbSet<Goal> Goals { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of feedback.
    /// </summary>
    DbSet<Feedback> Feedbacks { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
