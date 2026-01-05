// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PersonalNetWorthDashboard.Core.Models.UserAggregate;
using PersonalNetWorthDashboard.Core.Models.UserAggregate.Entities;
using PersonalNetWorthDashboard.Core.Services;
namespace PersonalNetWorthDashboard.Infrastructure;

/// <summary>
/// Provides seed data for the PersonalNetWorthDashboard database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PersonalNetWorthDashboardContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Assets.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedNetWorthDataAsync(context);
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

    private static async Task SeedNetWorthDataAsync(PersonalNetWorthDashboardContext context)
    {
        // Add sample assets
        var assets = new List<Asset>
        {
            new Asset
            {
                AssetId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Primary Residence",
                AssetType = AssetType.RealEstate,
                CurrentValue = 450000m,
                PurchasePrice = 350000m,
                PurchaseDate = new DateTime(2015, 6, 1),
                Notes = "3-bedroom home in suburban area",
                LastUpdated = DateTime.UtcNow,
                IsActive = true,
            },
            new Asset
            {
                AssetId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "401(k) Retirement Account",
                AssetType = AssetType.RetirementAccount,
                CurrentValue = 125000m,
                Institution = "Vanguard",
                AccountNumber = "****1234",
                Notes = "Target date fund 2050",
                LastUpdated = DateTime.UtcNow,
                IsActive = true,
            },
            new Asset
            {
                AssetId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Checking Account",
                AssetType = AssetType.Cash,
                CurrentValue = 15000m,
                Institution = "Chase Bank",
                AccountNumber = "****5678",
                LastUpdated = DateTime.UtcNow,
                IsActive = true,
            },
            new Asset
            {
                AssetId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Name = "Investment Portfolio",
                AssetType = AssetType.Investment,
                CurrentValue = 75000m,
                PurchasePrice = 60000m,
                Institution = "Fidelity",
                AccountNumber = "****9012",
                Notes = "Mix of index funds and individual stocks",
                LastUpdated = DateTime.UtcNow,
                IsActive = true,
            },
        };

        context.Assets.AddRange(assets);

        // Add sample liabilities
        var liabilities = new List<Liability>
        {
            new Liability
            {
                LiabilityId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                Name = "Mortgage",
                LiabilityType = LiabilityType.Mortgage,
                CurrentBalance = 280000m,
                OriginalAmount = 350000m,
                InterestRate = 3.75m,
                MonthlyPayment = 1800m,
                Creditor = "Bank of America",
                AccountNumber = "****3456",
                Notes = "30-year fixed mortgage",
                LastUpdated = DateTime.UtcNow,
                IsActive = true,
            },
            new Liability
            {
                LiabilityId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                Name = "Auto Loan",
                LiabilityType = LiabilityType.AutoLoan,
                CurrentBalance = 18000m,
                OriginalAmount = 25000m,
                InterestRate = 4.5m,
                MonthlyPayment = 450m,
                Creditor = "Honda Finance",
                AccountNumber = "****7890",
                Notes = "2021 Honda Accord",
                LastUpdated = DateTime.UtcNow,
                IsActive = true,
            },
            new Liability
            {
                LiabilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Credit Card",
                LiabilityType = LiabilityType.CreditCard,
                CurrentBalance = 3500m,
                InterestRate = 18.99m,
                MonthlyPayment = 150m,
                Creditor = "Chase",
                AccountNumber = "****2345",
                LastUpdated = DateTime.UtcNow,
                IsActive = true,
            },
        };

        context.Liabilities.AddRange(liabilities);

        // Add sample net worth snapshot
        var totalAssets = assets.Sum(a => a.CurrentValue);
        var totalLiabilities = liabilities.Sum(l => l.CurrentBalance);

        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = totalAssets,
            TotalLiabilities = totalLiabilities,
            NetWorth = totalAssets - totalLiabilities,
            Notes = "Initial net worth calculation",
            CreatedAt = DateTime.UtcNow,
        };

        context.NetWorthSnapshots.Add(snapshot);

        await context.SaveChangesAsync();
    }
}
