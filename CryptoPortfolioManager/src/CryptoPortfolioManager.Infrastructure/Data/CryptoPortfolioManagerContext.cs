// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CryptoPortfolioManager system.
/// </summary>
public class CryptoPortfolioManagerContext : DbContext, ICryptoPortfolioManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CryptoPortfolioManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CryptoPortfolioManagerContext(DbContextOptions<CryptoPortfolioManagerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Wallet> Wallets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<CryptoHolding> CryptoHoldings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Transaction> Transactions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TaxLot> TaxLots { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CryptoPortfolioManagerContext).Assembly);
    }
}
