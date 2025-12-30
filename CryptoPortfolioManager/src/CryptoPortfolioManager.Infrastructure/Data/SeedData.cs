// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoPortfolioManager.Infrastructure;

/// <summary>
/// Provides seed data for the CryptoPortfolioManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(CryptoPortfolioManagerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Wallets.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedWalletsAsync(context);
                logger.LogInformation("Initial data seeded successfully.");
            }
            else
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private static async Task SeedWalletsAsync(CryptoPortfolioManagerContext context)
    {
        var wallets = new List<Wallet>
        {
            new Wallet
            {
                WalletId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Main Hardware Wallet",
                Address = "0x742d35Cc6634C0532925a3b844Bc9e7595f0bEb7",
                WalletType = "Hardware",
                IsActive = true,
                Notes = "Ledger Nano X - Cold storage for long-term holdings",
            },
            new Wallet
            {
                WalletId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Exchange Account",
                Address = null,
                WalletType = "Exchange",
                IsActive = true,
                Notes = "Binance account for active trading",
            },
            new Wallet
            {
                WalletId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "MetaMask Wallet",
                Address = "0x5aAeb6053F3E94C9b9A09f33669435E7Ef1BeAed",
                WalletType = "Software",
                IsActive = true,
                Notes = "Browser extension wallet for DeFi",
            },
        };

        context.Wallets.AddRange(wallets);

        // Add sample holdings for the first wallet
        var holdings = new List<CryptoHolding>
        {
            new CryptoHolding
            {
                CryptoHoldingId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                WalletId = wallets[0].WalletId,
                Symbol = "BTC",
                Name = "Bitcoin",
                Quantity = 0.5m,
                AverageCost = 42000m,
                CurrentPrice = 65000m,
                LastPriceUpdate = DateTime.UtcNow,
            },
            new CryptoHolding
            {
                CryptoHoldingId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                WalletId = wallets[0].WalletId,
                Symbol = "ETH",
                Name = "Ethereum",
                Quantity = 5m,
                AverageCost = 2500m,
                CurrentPrice = 3500m,
                LastPriceUpdate = DateTime.UtcNow,
            },
            new CryptoHolding
            {
                CryptoHoldingId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                WalletId = wallets[1].WalletId,
                Symbol = "ADA",
                Name = "Cardano",
                Quantity = 1000m,
                AverageCost = 0.75m,
                CurrentPrice = 1.25m,
                LastPriceUpdate = DateTime.UtcNow,
            },
        };

        context.CryptoHoldings.AddRange(holdings);

        // Add sample transactions
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                TransactionId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                WalletId = wallets[0].WalletId,
                TransactionDate = DateTime.UtcNow.AddMonths(-6),
                TransactionType = TransactionType.Buy,
                Symbol = "BTC",
                Quantity = 0.5m,
                PricePerUnit = 42000m,
                TotalAmount = 21000m,
                Fees = 50m,
                Notes = "Initial Bitcoin purchase",
            },
            new Transaction
            {
                TransactionId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                WalletId = wallets[0].WalletId,
                TransactionDate = DateTime.UtcNow.AddMonths(-3),
                TransactionType = TransactionType.Buy,
                Symbol = "ETH",
                Quantity = 5m,
                PricePerUnit = 2500m,
                TotalAmount = 12500m,
                Fees = 25m,
                Notes = "Ethereum accumulation",
            },
            new Transaction
            {
                TransactionId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                WalletId = wallets[1].WalletId,
                TransactionDate = DateTime.UtcNow.AddMonths(-1),
                TransactionType = TransactionType.Buy,
                Symbol = "ADA",
                Quantity = 1000m,
                PricePerUnit = 0.75m,
                TotalAmount = 750m,
                Fees = 5m,
                Notes = "Cardano position",
            },
        };

        context.Transactions.AddRange(transactions);

        // Add sample tax lots
        var taxLots = new List<TaxLot>
        {
            new TaxLot
            {
                TaxLotId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CryptoHoldingId = holdings[0].CryptoHoldingId,
                AcquisitionDate = DateTime.UtcNow.AddMonths(-6),
                Quantity = 0.5m,
                CostBasis = 21050m,
                IsDisposed = false,
            },
            new TaxLot
            {
                TaxLotId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CryptoHoldingId = holdings[1].CryptoHoldingId,
                AcquisitionDate = DateTime.UtcNow.AddMonths(-3),
                Quantity = 5m,
                CostBasis = 12525m,
                IsDisposed = false,
            },
        };

        context.TaxLots.AddRange(taxLots);

        await context.SaveChangesAsync();
    }
}
