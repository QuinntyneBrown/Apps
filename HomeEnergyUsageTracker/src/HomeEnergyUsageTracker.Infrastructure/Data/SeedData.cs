// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeEnergyUsageTracker.Infrastructure;

/// <summary>
/// Provides seed data for the HomeEnergyUsageTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HomeEnergyUsageTrackerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.UtilityBills.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedUtilityBillsAsync(context);
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

    private static async Task SeedUtilityBillsAsync(HomeEnergyUsageTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var utilityBills = new List<UtilityBill>
        {
            new UtilityBill
            {
                UtilityBillId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                UtilityType = UtilityType.Electricity,
                BillingDate = new DateTime(2024, 12, 1),
                Amount = 125.50m,
                UsageAmount = 850.0m,
                Unit = "kWh",
                CreatedAt = DateTime.UtcNow,
            },
            new UtilityBill
            {
                UtilityBillId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                UtilityType = UtilityType.Gas,
                BillingDate = new DateTime(2024, 12, 1),
                Amount = 78.25m,
                UsageAmount = 45.0m,
                Unit = "Therms",
                CreatedAt = DateTime.UtcNow,
            },
            new UtilityBill
            {
                UtilityBillId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                UtilityType = UtilityType.Water,
                BillingDate = new DateTime(2024, 12, 1),
                Amount = 42.00m,
                UsageAmount = 5500.0m,
                Unit = "Gallons",
                CreatedAt = DateTime.UtcNow,
            },
            new UtilityBill
            {
                UtilityBillId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                UtilityType = UtilityType.Electricity,
                BillingDate = new DateTime(2024, 11, 1),
                Amount = 118.75m,
                UsageAmount = 810.0m,
                Unit = "kWh",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.UtilityBills.AddRange(utilityBills);

        // Add sample usages for the first utility bill
        var usages = new List<Usage>
        {
            new Usage
            {
                UsageId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UtilityBillId = utilityBills[0].UtilityBillId,
                Date = new DateTime(2024, 12, 1),
                Amount = 28.5m,
                CreatedAt = DateTime.UtcNow,
            },
            new Usage
            {
                UsageId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UtilityBillId = utilityBills[0].UtilityBillId,
                Date = new DateTime(2024, 12, 15),
                Amount = 32.1m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Usages.AddRange(usages);

        // Add sample savings tips
        var savingsTips = new List<SavingsTip>
        {
            new SavingsTip
            {
                SavingsTipId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Title = "Lower Your Thermostat",
                Description = "Reduce your thermostat by 1-2 degrees to save up to 10% on heating costs.",
                CreatedAt = DateTime.UtcNow,
            },
            new SavingsTip
            {
                SavingsTipId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Title = "Switch to LED Bulbs",
                Description = "LED bulbs use 75% less energy and last 25 times longer than incandescent bulbs.",
                CreatedAt = DateTime.UtcNow,
            },
            new SavingsTip
            {
                SavingsTipId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Title = "Fix Leaky Faucets",
                Description = "A dripping faucet can waste over 3,000 gallons of water per year.",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.SavingsTips.AddRange(savingsTips);

        await context.SaveChangesAsync();
    }
}
