// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CampingTripPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using CampingTripPlanner.Core.Model.UserAggregate;
using CampingTripPlanner.Core.Model.UserAggregate.Entities;
using CampingTripPlanner.Core.Services;
namespace CampingTripPlanner.Infrastructure;

/// <summary>
/// Provides seed data for the CampingTripPlanner database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(CampingTripPlannerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Campsites.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedCampsitesAsync(context);
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

    private static async Task SeedCampsitesAsync(CampingTripPlannerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var campsites = new List<Campsite>
        {
            new Campsite
            {
                CampsiteId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Mountain View Campground",
                Location = "Rocky Mountain National Park, Colorado",
                CampsiteType = CampsiteType.Tent,
                Description = "Beautiful mountain views with hiking trails nearby",
                HasElectricity = false,
                HasWater = true,
                CostPerNight = 25.00m,
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-3),
            },
            new Campsite
            {
                CampsiteId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Lakeside RV Park",
                Location = "Lake Tahoe, California",
                CampsiteType = CampsiteType.RV,
                Description = "Full hookup sites with lake access",
                HasElectricity = true,
                HasWater = true,
                CostPerNight = 45.00m,
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
            },
            new Campsite
            {
                CampsiteId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Pine Forest Primitive Site",
                Location = "Olympic National Forest, Washington",
                CampsiteType = CampsiteType.Primitive,
                Description = "Remote backcountry camping experience",
                HasElectricity = false,
                HasWater = false,
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
        };

        context.Campsites.AddRange(campsites);

        // Add sample trips
        var trips = new List<Trip>
        {
            new Trip
            {
                TripId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Summer Mountain Adventure",
                CampsiteId = campsites[0].CampsiteId,
                StartDate = DateTime.UtcNow.AddMonths(2),
                EndDate = DateTime.UtcNow.AddMonths(2).AddDays(3),
                NumberOfPeople = 4,
                Notes = "Bring extra warm clothing for night time",
                CreatedAt = DateTime.UtcNow,
            },
            new Trip
            {
                TripId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Weekend Lake Getaway",
                CampsiteId = campsites[1].CampsiteId,
                StartDate = DateTime.UtcNow.AddMonths(1),
                EndDate = DateTime.UtcNow.AddMonths(1).AddDays(2),
                NumberOfPeople = 2,
                Notes = "Don't forget fishing gear",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Trips.AddRange(trips);

        // Add sample gear checklist
        var gearChecklists = new List<GearChecklist>
        {
            new GearChecklist
            {
                GearChecklistId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                TripId = trips[0].TripId,
                ItemName = "Tent",
                IsPacked = false,
                Quantity = 1,
                Notes = "4-person tent",
                CreatedAt = DateTime.UtcNow,
            },
            new GearChecklist
            {
                GearChecklistId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                TripId = trips[0].TripId,
                ItemName = "Sleeping Bags",
                IsPacked = true,
                Quantity = 4,
                CreatedAt = DateTime.UtcNow,
            },
            new GearChecklist
            {
                GearChecklistId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                TripId = trips[0].TripId,
                ItemName = "Camping Stove",
                IsPacked = false,
                Quantity = 1,
                Notes = "Check propane level",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.GearChecklists.AddRange(gearChecklists);

        // Add sample reviews
        var reviews = new List<Review>
        {
            new Review
            {
                ReviewId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                CampsiteId = campsites[0].CampsiteId,
                Rating = 5,
                ReviewText = "Amazing views and well-maintained facilities. Perfect for families!",
                ReviewDate = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new Review
            {
                ReviewId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                CampsiteId = campsites[2].CampsiteId,
                Rating = 4,
                ReviewText = "Great backcountry experience, but prepare for no amenities.",
                ReviewDate = DateTime.UtcNow.AddDays(-15),
                CreatedAt = DateTime.UtcNow.AddDays(-15),
            },
        };

        context.Reviews.AddRange(reviews);

        await context.SaveChangesAsync();
    }
}
