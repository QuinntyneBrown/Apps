// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TravelDestinationWishlist.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TravelDestinationWishlist.Core.Models.UserAggregate;
using TravelDestinationWishlist.Core.Models.UserAggregate.Entities;
using TravelDestinationWishlist.Core.Services;
namespace TravelDestinationWishlist.Infrastructure;

/// <summary>
/// Provides seed data for the TravelDestinationWishlist database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(TravelDestinationWishlistContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Destinations.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedDestinationsAndTripsAsync(context);
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

    private static async Task SeedDestinationsAndTripsAsync(TravelDestinationWishlistContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var destinations = new List<Destination>
        {
            new Destination
            {
                DestinationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Tokyo",
                Country = "Japan",
                DestinationType = DestinationType.City,
                Description = "Experience the blend of traditional and modern culture",
                Priority = 1,
                IsVisited = true,
                VisitedDate = new DateTime(2023, 5, 15),
                Notes = "Amazing food and friendly people",
                CreatedAt = DateTime.UtcNow.AddMonths(-12),
            },
            new Destination
            {
                DestinationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Santorini",
                Country = "Greece",
                DestinationType = DestinationType.Beach,
                Description = "Beautiful Greek island with stunning sunsets",
                Priority = 2,
                IsVisited = false,
                Notes = "Top priority for next summer",
                CreatedAt = DateTime.UtcNow.AddMonths(-8),
            },
            new Destination
            {
                DestinationId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Swiss Alps",
                Country = "Switzerland",
                DestinationType = DestinationType.Mountain,
                Description = "Scenic mountain ranges and hiking trails",
                Priority = 3,
                IsVisited = false,
                Notes = "Plan for winter skiing trip",
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
            },
            new Destination
            {
                DestinationId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "Paris",
                Country = "France",
                DestinationType = DestinationType.City,
                Description = "The city of lights and romance",
                Priority = 2,
                IsVisited = true,
                VisitedDate = new DateTime(2022, 9, 20),
                Notes = "Visited museums and enjoyed French cuisine",
                CreatedAt = DateTime.UtcNow.AddMonths(-18),
            },
            new Destination
            {
                DestinationId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Name = "Machu Picchu",
                Country = "Peru",
                DestinationType = DestinationType.Historical,
                Description = "Ancient Incan citadel in the Andes Mountains",
                Priority = 1,
                IsVisited = false,
                Notes = "Dream destination - need to plan extensively",
                CreatedAt = DateTime.UtcNow.AddMonths(-10),
            },
        };

        context.Destinations.AddRange(destinations);

        var trips = new List<Trip>
        {
            new Trip
            {
                TripId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                DestinationId = destinations[0].DestinationId,
                StartDate = new DateTime(2023, 5, 10),
                EndDate = new DateTime(2023, 5, 20),
                TotalCost = 3500.00m,
                Accommodation = "Hotel in Shibuya",
                Transportation = "Flights + Tokyo Metro",
                Notes = "10-day trip exploring Tokyo and surrounding areas",
                CreatedAt = new DateTime(2023, 5, 20),
            },
            new Trip
            {
                TripId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                DestinationId = destinations[3].DestinationId,
                StartDate = new DateTime(2022, 9, 15),
                EndDate = new DateTime(2022, 9, 25),
                TotalCost = 2800.00m,
                Accommodation = "Boutique hotel near Louvre",
                Transportation = "Flights + Paris Metro",
                Notes = "Romantic getaway to Paris",
                CreatedAt = new DateTime(2022, 9, 25),
            },
        };

        context.Trips.AddRange(trips);

        var memories = new List<Memory>
        {
            new Memory
            {
                MemoryId = Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111"),
                UserId = sampleUserId,
                TripId = trips[0].TripId,
                Title = "Cherry Blossoms at Ueno Park",
                Description = "Beautiful cherry blossoms in full bloom, stunning scenery",
                MemoryDate = new DateTime(2023, 5, 12),
                PhotoUrl = "/photos/tokyo-cherry-blossoms.jpg",
                CreatedAt = new DateTime(2023, 5, 12),
            },
            new Memory
            {
                MemoryId = Guid.Parse("bbbbbbbb-1111-1111-1111-111111111111"),
                UserId = sampleUserId,
                TripId = trips[0].TripId,
                Title = "Sushi at Tsukiji Market",
                Description = "Fresh sushi breakfast at the famous fish market",
                MemoryDate = new DateTime(2023, 5, 14),
                PhotoUrl = "/photos/tsukiji-sushi.jpg",
                CreatedAt = new DateTime(2023, 5, 14),
            },
            new Memory
            {
                MemoryId = Guid.Parse("cccccccc-1111-1111-1111-111111111111"),
                UserId = sampleUserId,
                TripId = trips[1].TripId,
                Title = "Sunset from Eiffel Tower",
                Description = "Breathtaking sunset view from the top of Eiffel Tower",
                MemoryDate = new DateTime(2022, 9, 18),
                PhotoUrl = "/photos/eiffel-sunset.jpg",
                CreatedAt = new DateTime(2022, 9, 18),
            },
            new Memory
            {
                MemoryId = Guid.Parse("dddddddd-1111-1111-1111-111111111111"),
                UserId = sampleUserId,
                TripId = trips[1].TripId,
                Title = "Louvre Museum Visit",
                Description = "Saw the Mona Lisa and countless other masterpieces",
                MemoryDate = new DateTime(2022, 9, 19),
                PhotoUrl = "/photos/louvre-visit.jpg",
                CreatedAt = new DateTime(2022, 9, 19),
            },
        };

        context.Memories.AddRange(memories);

        await context.SaveChangesAsync();
    }
}
