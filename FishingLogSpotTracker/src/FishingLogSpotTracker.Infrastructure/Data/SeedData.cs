// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FishingLogSpotTracker.Core.Model.UserAggregate;
using FishingLogSpotTracker.Core.Model.UserAggregate.Entities;
using FishingLogSpotTracker.Core.Services;
namespace FishingLogSpotTracker.Infrastructure;

/// <summary>
/// Provides seed data for the FishingLogSpotTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FishingLogSpotTrackerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Spots.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedSpotsAndTripsAsync(context);
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

    private static async Task SeedSpotsAndTripsAsync(FishingLogSpotTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var spots = new List<Spot>
        {
            new Spot
            {
                SpotId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Miller's Lake - North Shore",
                LocationType = LocationType.Lake,
                Latitude = 45.5234m,
                Longitude = -122.6762m,
                Description = "Excellent bass fishing spot with good access",
                WaterBodyName = "Miller's Lake",
                Directions = "Take Highway 26 to exit 45, then north 2 miles",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Spot
            {
                SpotId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Salmon River - Bend Pool",
                LocationType = LocationType.River,
                Latitude = 45.1234m,
                Longitude = -121.9876m,
                Description = "Great trout fishing in spring and summer",
                WaterBodyName = "Salmon River",
                Directions = "Access from River Road parking area",
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Spot
            {
                SpotId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Pacific Coast - Jetty 12",
                LocationType = LocationType.Ocean,
                Latitude = 44.6234m,
                Longitude = -124.0543m,
                Description = "Saltwater fishing from the jetty, good for rockfish",
                WaterBodyName = "Pacific Ocean",
                Directions = "From town, follow coast highway south 5 miles",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Spots.AddRange(spots);

        var trips = new List<Trip>
        {
            new Trip
            {
                TripId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                SpotId = spots[0].SpotId,
                TripDate = DateTime.UtcNow.AddDays(-10),
                StartTime = DateTime.UtcNow.AddDays(-10).AddHours(6),
                EndTime = DateTime.UtcNow.AddDays(-10).AddHours(12),
                WeatherConditions = "Partly cloudy, light breeze",
                WaterTemperature = 68.5m,
                AirTemperature = 75.0m,
                Notes = "Great day on the water, very productive",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new Trip
            {
                TripId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                SpotId = spots[1].SpotId,
                TripDate = DateTime.UtcNow.AddDays(-5),
                StartTime = DateTime.UtcNow.AddDays(-5).AddHours(7),
                EndTime = DateTime.UtcNow.AddDays(-5).AddHours(11),
                WeatherConditions = "Sunny, calm",
                WaterTemperature = 62.0m,
                AirTemperature = 78.0m,
                Notes = "Water was a bit warm but still had some bites",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
        };

        context.Trips.AddRange(trips);

        var catches = new List<Catch>
        {
            new Catch
            {
                CatchId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                TripId = trips[0].TripId,
                Species = FishSpecies.LargemouthBass,
                Length = 18.5m,
                Weight = 4.2m,
                CatchTime = trips[0].StartTime.AddHours(2),
                BaitUsed = "Green plastic worm",
                WasReleased = true,
                Notes = "Beautiful fish, caught near the lily pads",
                CreatedAt = trips[0].StartTime.AddHours(2),
            },
            new Catch
            {
                CatchId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                TripId = trips[0].TripId,
                Species = FishSpecies.LargemouthBass,
                Length = 14.0m,
                Weight = 2.1m,
                CatchTime = trips[0].StartTime.AddHours(4),
                BaitUsed = "Crankbait",
                WasReleased = true,
                Notes = "Smaller bass, released immediately",
                CreatedAt = trips[0].StartTime.AddHours(4),
            },
            new Catch
            {
                CatchId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                TripId = trips[1].TripId,
                Species = FishSpecies.RainbowTrout,
                Length = 12.5m,
                Weight = 1.3m,
                CatchTime = trips[1].StartTime.AddHours(1),
                BaitUsed = "Salmon eggs",
                WasReleased = false,
                Notes = "Kept for dinner",
                CreatedAt = trips[1].StartTime.AddHours(1),
            },
        };

        context.Catches.AddRange(catches);

        await context.SaveChangesAsync();
    }
}
