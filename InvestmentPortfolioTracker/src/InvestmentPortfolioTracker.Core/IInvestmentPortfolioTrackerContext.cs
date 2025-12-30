// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Represents the persistence surface for the InvestmentPortfolioTracker system.
/// </summary>
public interface IInvestmentPortfolioTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of accounts.
    /// </summary>
    DbSet<Account> Accounts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of holdings.
    /// </summary>
    DbSet<Holding> Holdings { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of transactions.
    /// </summary>
    DbSet<Transaction> Transactions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of dividends.
    /// </summary>
    DbSet<Dividend> Dividends { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
