// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SubscriptionAuditTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Infrastructure;

/// <summary>
/// Provides seed data for the SubscriptionAuditTool database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(SubscriptionAuditToolContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Categories.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedCategoriesAndSubscriptionsAsync(context);
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

    private static async Task SeedCategoriesAndSubscriptionsAsync(SubscriptionAuditToolContext context)
    {
        var categories = new List<Category>
        {
            new Category
            {
                CategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Entertainment",
                Description = "Streaming and entertainment services",
                ColorCode = "#FF5733",
            },
            new Category
            {
                CategoryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Productivity",
                Description = "Software and productivity tools",
                ColorCode = "#33A1FF",
            },
            new Category
            {
                CategoryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Health & Fitness",
                Description = "Health and fitness related subscriptions",
                ColorCode = "#28A745",
            },
            new Category
            {
                CategoryId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Name = "News & Media",
                Description = "News and media subscriptions",
                ColorCode = "#FFC107",
            },
        };

        context.Categories.AddRange(categories);

        var subscriptions = new List<Subscription>
        {
            new Subscription
            {
                SubscriptionId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ServiceName = "Netflix",
                Cost = 15.99m,
                BillingCycle = BillingCycle.Monthly,
                NextBillingDate = DateTime.UtcNow.AddDays(15),
                Status = SubscriptionStatus.Active,
                StartDate = DateTime.UtcNow.AddMonths(-12),
                CategoryId = categories[0].CategoryId,
                Notes = "Premium plan for family",
            },
            new Subscription
            {
                SubscriptionId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ServiceName = "Spotify",
                Cost = 9.99m,
                BillingCycle = BillingCycle.Monthly,
                NextBillingDate = DateTime.UtcNow.AddDays(20),
                Status = SubscriptionStatus.Active,
                StartDate = DateTime.UtcNow.AddMonths(-24),
                CategoryId = categories[0].CategoryId,
                Notes = "Student discount applied",
            },
            new Subscription
            {
                SubscriptionId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ServiceName = "Microsoft 365",
                Cost = 99.99m,
                BillingCycle = BillingCycle.Annual,
                NextBillingDate = DateTime.UtcNow.AddMonths(8),
                Status = SubscriptionStatus.Active,
                StartDate = DateTime.UtcNow.AddMonths(-4),
                CategoryId = categories[1].CategoryId,
                Notes = "Business subscription",
            },
            new Subscription
            {
                SubscriptionId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ServiceName = "Adobe Creative Cloud",
                Cost = 52.99m,
                BillingCycle = BillingCycle.Monthly,
                NextBillingDate = DateTime.UtcNow.AddDays(10),
                Status = SubscriptionStatus.Active,
                StartDate = DateTime.UtcNow.AddMonths(-6),
                CategoryId = categories[1].CategoryId,
                Notes = "Individual plan",
            },
            new Subscription
            {
                SubscriptionId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ServiceName = "Gym Membership",
                Cost = 45.00m,
                BillingCycle = BillingCycle.Monthly,
                NextBillingDate = DateTime.UtcNow.AddDays(5),
                Status = SubscriptionStatus.Active,
                StartDate = DateTime.UtcNow.AddMonths(-18),
                CategoryId = categories[2].CategoryId,
                Notes = "Includes pool and classes",
            },
            new Subscription
            {
                SubscriptionId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ServiceName = "New York Times",
                Cost = 17.00m,
                BillingCycle = BillingCycle.Monthly,
                NextBillingDate = DateTime.UtcNow.AddDays(-30),
                Status = SubscriptionStatus.Cancelled,
                StartDate = DateTime.UtcNow.AddMonths(-36),
                CancellationDate = DateTime.UtcNow.AddDays(-30),
                CategoryId = categories[3].CategoryId,
                Notes = "Cancelled due to budget cuts",
            },
        };

        context.Subscriptions.AddRange(subscriptions);

        await context.SaveChangesAsync();
    }
}
