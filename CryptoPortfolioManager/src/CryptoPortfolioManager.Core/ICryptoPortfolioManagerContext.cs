// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using CryptoPortfolioManager.Core.Model.UserAggregate;
using CryptoPortfolioManager.Core.Model.UserAggregate.Entities;
namespace CryptoPortfolioManager.Core;

/// <summary>
/// Represents the persistence surface for the CryptoPortfolioManager system.
/// </summary>
public interface ICryptoPortfolioManagerContext
{
    /// <summary>
    /// Gets or sets the DbSet of wallets.
    /// </summary>
    DbSet<Wallet> Wallets { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of crypto holdings.
    /// </summary>
    DbSet<CryptoHolding> CryptoHoldings { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of transactions.
    /// </summary>
    DbSet<Transaction> Transactions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tax lots.
    /// </summary>
    DbSet<TaxLot> TaxLots { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    
    /// <summary>
    /// Gets the users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
