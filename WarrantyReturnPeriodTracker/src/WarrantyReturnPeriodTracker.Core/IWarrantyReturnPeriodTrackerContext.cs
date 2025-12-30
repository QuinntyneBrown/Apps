// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the persistence surface for the WarrantyReturnPeriodTracker system.
/// </summary>
public interface IWarrantyReturnPeriodTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of purchases.
    /// </summary>
    DbSet<Purchase> Purchases { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of warranties.
    /// </summary>
    DbSet<Warranty> Warranties { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of return windows.
    /// </summary>
    DbSet<ReturnWindow> ReturnWindows { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of receipts.
    /// </summary>
    DbSet<Receipt> Receipts { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
