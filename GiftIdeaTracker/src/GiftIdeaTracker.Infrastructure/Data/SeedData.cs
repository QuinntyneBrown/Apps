// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GiftIdeaTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using GiftIdeaTracker.Core.Model.UserAggregate;
using GiftIdeaTracker.Core.Model.UserAggregate.Entities;
using GiftIdeaTracker.Core.Services;
namespace GiftIdeaTracker.Infrastructure;

/// <summary>
/// Provides seed data for the GiftIdeaTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(GiftIdeaTrackerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Recipients.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedRecipientsAndGiftIdeasAsync(context);
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

    private static async Task SeedRecipientsAndGiftIdeasAsync(GiftIdeaTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var recipients = new List<Recipient>
        {
            new Recipient
            {
                RecipientId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Sarah Johnson",
                Relationship = "Wife",
                CreatedAt = DateTime.UtcNow,
            },
            new Recipient
            {
                RecipientId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Michael Chen",
                Relationship = "Brother",
                CreatedAt = DateTime.UtcNow,
            },
            new Recipient
            {
                RecipientId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Emily Davis",
                Relationship = "Friend",
                CreatedAt = DateTime.UtcNow,
            },
            new Recipient
            {
                RecipientId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "Mom",
                Relationship = "Mother",
                CreatedAt = DateTime.UtcNow,
            },
            new Recipient
            {
                RecipientId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Name = "David Wilson",
                Relationship = "Colleague",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Recipients.AddRange(recipients);

        var giftIdeas = new List<GiftIdea>
        {
            new GiftIdea
            {
                GiftIdeaId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipientId = recipients[0].RecipientId,
                Name = "Diamond Necklace",
                Occasion = Occasion.Anniversary,
                EstimatedPrice = 1200.00m,
                IsPurchased = false,
                CreatedAt = DateTime.UtcNow,
            },
            new GiftIdea
            {
                GiftIdeaId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipientId = recipients[1].RecipientId,
                Name = "Gaming Laptop",
                Occasion = Occasion.Birthday,
                EstimatedPrice = 1500.00m,
                IsPurchased = false,
                CreatedAt = DateTime.UtcNow,
            },
            new GiftIdea
            {
                GiftIdeaId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipientId = recipients[2].RecipientId,
                Name = "Yoga Mat Set",
                Occasion = Occasion.Birthday,
                EstimatedPrice = 45.00m,
                IsPurchased = true,
                CreatedAt = DateTime.UtcNow,
            },
            new GiftIdea
            {
                GiftIdeaId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipientId = recipients[3].RecipientId,
                Name = "Gardening Tools Kit",
                Occasion = Occasion.Birthday,
                EstimatedPrice = 85.00m,
                IsPurchased = false,
                CreatedAt = DateTime.UtcNow,
            },
            new GiftIdea
            {
                GiftIdeaId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                RecipientId = recipients[4].RecipientId,
                Name = "Coffee Maker",
                Occasion = Occasion.Christmas,
                EstimatedPrice = 150.00m,
                IsPurchased = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.GiftIdeas.AddRange(giftIdeas);

        // Add a sample purchase for the yoga mat
        var purchase = new Purchase
        {
            PurchaseId = Guid.Parse("11111111-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            GiftIdeaId = giftIdeas[2].GiftIdeaId,
            PurchaseDate = DateTime.UtcNow.AddDays(-5),
            ActualPrice = 42.99m,
            Store = "Amazon",
            CreatedAt = DateTime.UtcNow.AddDays(-5),
        };

        context.Purchases.Add(purchase);

        await context.SaveChangesAsync();
    }
}
