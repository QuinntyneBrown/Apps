// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NeighborhoodSocialNetwork.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using NeighborhoodSocialNetwork.Core.Model.UserAggregate;
using NeighborhoodSocialNetwork.Core.Model.UserAggregate.Entities;
using NeighborhoodSocialNetwork.Core.Services;
namespace NeighborhoodSocialNetwork.Infrastructure;

/// <summary>
/// Provides seed data for the NeighborhoodSocialNetwork database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(NeighborhoodSocialNetworkContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Neighbors.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedNeighborhoodDataAsync(context);
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

    private static async Task SeedNeighborhoodDataAsync(NeighborhoodSocialNetworkContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var neighbor1 = new Neighbor
        {
            NeighborId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            Name = "John Smith",
            Address = "123 Main Street",
            ContactInfo = "john@example.com",
            Bio = "Longtime resident, loves gardening",
            Interests = "Gardening, Community Events, Book Club",
            IsVerified = true,
            CreatedAt = DateTime.UtcNow,
        };

        var neighbor2 = new Neighbor
        {
            NeighborId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Jane Doe",
            Address = "456 Oak Avenue",
            ContactInfo = "jane@example.com",
            Bio = "New to the neighborhood, excited to meet everyone!",
            Interests = "Running, Cooking, Photography",
            IsVerified = false,
            CreatedAt = DateTime.UtcNow,
        };

        context.Neighbors.AddRange(neighbor1, neighbor2);

        var event1 = new Event
        {
            EventId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            CreatedByNeighborId = neighbor1.NeighborId,
            Title = "Community Cleanup Day",
            Description = "Let's clean up our local park together!",
            EventDateTime = DateTime.UtcNow.AddDays(14),
            Location = "Neighborhood Park",
            IsPublic = true,
            CreatedAt = DateTime.UtcNow,
        };

        var event2 = new Event
        {
            EventId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            CreatedByNeighborId = neighbor1.NeighborId,
            Title = "Block Party",
            Description = "Annual summer block party - bring your favorite dish!",
            EventDateTime = DateTime.UtcNow.AddDays(30),
            Location = "Main Street",
            IsPublic = true,
            CreatedAt = DateTime.UtcNow,
        };

        context.Events.AddRange(event1, event2);

        var recommendation1 = new Recommendation
        {
            RecommendationId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            NeighborId = neighbor1.NeighborId,
            Title = "Best Pizza in Town",
            Description = "Amazing authentic Italian pizza, family-owned",
            RecommendationType = RecommendationType.Restaurant,
            BusinessName = "Mario's Pizzeria",
            Location = "789 High Street",
            Rating = 5,
            CreatedAt = DateTime.UtcNow,
        };

        context.Recommendations.Add(recommendation1);

        var message1 = new Message
        {
            MessageId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            SenderNeighborId = neighbor1.NeighborId,
            RecipientNeighborId = neighbor2.NeighborId,
            Subject = "Welcome to the neighborhood!",
            Content = "Hi Jane, welcome! Let me know if you need any recommendations.",
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
        };

        context.Messages.Add(message1);

        await context.SaveChangesAsync();
    }
}
