// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using InvestmentPortfolioTracker.Core.Models.UserAggregate;
using InvestmentPortfolioTracker.Core.Models.UserAggregate.Entities;
using InvestmentPortfolioTracker.Core.Services;
namespace InvestmentPortfolioTracker.Infrastructure;

/// <summary>
/// Provides seed data for the InvestmentPortfolioTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(InvestmentPortfolioTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Accounts.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedInvestmentDataAsync(context);
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

    private static async Task SeedInvestmentDataAsync(InvestmentPortfolioTrackerContext context)
    {
        // Seed Accounts
        var accounts = new List<Account>
        {
            new Account
            {
                AccountId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Traditional IRA",
                AccountType = AccountType.IRA,
                Institution = "Vanguard",
                AccountNumber = "IRA123456",
                CurrentBalance = 125000.00m,
                IsActive = true,
                OpenedDate = new DateTime(2020, 1, 15),
                Notes = "Traditional IRA for retirement savings",
            },
            new Account
            {
                AccountId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Brokerage Account",
                AccountType = AccountType.Brokerage,
                Institution = "Fidelity",
                AccountNumber = "BRK987654",
                CurrentBalance = 75000.00m,
                IsActive = true,
                OpenedDate = new DateTime(2021, 3, 10),
                Notes = "General brokerage for trading",
            },
        };

        context.Accounts.AddRange(accounts);

        // Seed Holdings
        var holdings = new List<Holding>
        {
            new Holding
            {
                HoldingId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                AccountId = accounts[0].AccountId,
                Symbol = "VTSAX",
                Name = "Vanguard Total Stock Market Index Fund",
                Shares = 500m,
                AverageCost = 105.50m,
                CurrentPrice = 112.75m,
                LastPriceUpdate = DateTime.UtcNow,
            },
            new Holding
            {
                HoldingId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                AccountId = accounts[0].AccountId,
                Symbol = "VBTLX",
                Name = "Vanguard Total Bond Market Index Fund",
                Shares = 300m,
                AverageCost = 10.25m,
                CurrentPrice = 10.15m,
                LastPriceUpdate = DateTime.UtcNow,
            },
            new Holding
            {
                HoldingId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                AccountId = accounts[1].AccountId,
                Symbol = "AAPL",
                Name = "Apple Inc.",
                Shares = 100m,
                AverageCost = 150.00m,
                CurrentPrice = 175.25m,
                LastPriceUpdate = DateTime.UtcNow,
            },
            new Holding
            {
                HoldingId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                AccountId = accounts[1].AccountId,
                Symbol = "MSFT",
                Name = "Microsoft Corporation",
                Shares = 75m,
                AverageCost = 280.00m,
                CurrentPrice = 310.50m,
                LastPriceUpdate = DateTime.UtcNow,
            },
        };

        context.Holdings.AddRange(holdings);

        // Seed Transactions
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                TransactionId = Guid.Parse("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                AccountId = accounts[0].AccountId,
                HoldingId = holdings[0].HoldingId,
                TransactionDate = DateTime.UtcNow.AddDays(-30),
                TransactionType = TransactionType.Buy,
                Symbol = "VTSAX",
                Shares = 100m,
                PricePerShare = 108.50m,
                Amount = 10850.00m,
                Fees = 0m,
                Notes = "Monthly investment contribution",
            },
            new Transaction
            {
                TransactionId = Guid.Parse("bbbb1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                AccountId = accounts[1].AccountId,
                HoldingId = holdings[2].HoldingId,
                TransactionDate = DateTime.UtcNow.AddDays(-60),
                TransactionType = TransactionType.Buy,
                Symbol = "AAPL",
                Shares = 50m,
                PricePerShare = 155.00m,
                Amount = 7750.00m,
                Fees = 6.95m,
                Notes = "Initial Apple purchase",
            },
            new Transaction
            {
                TransactionId = Guid.Parse("cccc1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                AccountId = accounts[1].AccountId,
                TransactionDate = DateTime.UtcNow.AddDays(-10),
                TransactionType = TransactionType.Deposit,
                Amount = 5000.00m,
                Fees = 0m,
                Notes = "Cash deposit to account",
            },
        };

        context.Transactions.AddRange(transactions);

        // Seed Dividends
        var dividends = new List<Dividend>
        {
            new Dividend
            {
                DividendId = Guid.Parse("dddd1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                HoldingId = holdings[0].HoldingId,
                PaymentDate = DateTime.UtcNow.AddDays(-15),
                ExDividendDate = DateTime.UtcNow.AddDays(-25),
                AmountPerShare = 0.75m,
                TotalAmount = 375.00m,
                IsReinvested = true,
                Notes = "Quarterly dividend - reinvested",
            },
            new Dividend
            {
                DividendId = Guid.Parse("eeee1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                HoldingId = holdings[2].HoldingId,
                PaymentDate = DateTime.UtcNow.AddDays(-20),
                ExDividendDate = DateTime.UtcNow.AddDays(-30),
                AmountPerShare = 0.24m,
                TotalAmount = 24.00m,
                IsReinvested = false,
                Notes = "Apple dividend - cash payment",
            },
            new Dividend
            {
                DividendId = Guid.Parse("ffff1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                HoldingId = holdings[3].HoldingId,
                PaymentDate = DateTime.UtcNow.AddDays(-18),
                ExDividendDate = DateTime.UtcNow.AddDays(-28),
                AmountPerShare = 0.68m,
                TotalAmount = 51.00m,
                IsReinvested = false,
                Notes = "Microsoft dividend - cash payment",
            },
        };

        context.Dividends.AddRange(dividends);

        await context.SaveChangesAsync();
    }
}
