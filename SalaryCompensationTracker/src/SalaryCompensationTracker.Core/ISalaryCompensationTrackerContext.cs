// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace SalaryCompensationTracker.Core;

/// <summary>
/// Represents the persistence surface for the SalaryCompensationTracker system.
/// </summary>
public interface ISalaryCompensationTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of compensation records.
    /// </summary>
    DbSet<Compensation> Compensations { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of benefits.
    /// </summary>
    DbSet<Benefit> Benefits { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of market comparisons.
    /// </summary>
    DbSet<MarketComparison> MarketComparisons { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
