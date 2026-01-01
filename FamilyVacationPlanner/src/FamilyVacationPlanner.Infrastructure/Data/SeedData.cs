// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyVacationPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FamilyVacationPlanner.Core.Model.UserAggregate;
using FamilyVacationPlanner.Core.Model.UserAggregate.Entities;
using FamilyVacationPlanner.Core.Services;
namespace FamilyVacationPlanner.Infrastructure;

/// <summary>
/// Provides seed data for the FamilyVacationPlanner database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FamilyVacationPlannerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Trips.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedTripsAsync(context);
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

    private static async Task SeedTripsAsync(FamilyVacationPlannerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var trips = new List<Trip>
        {
            new Trip
            {
                TripId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Summer Beach Vacation 2024",
                Destination = "Maui, Hawaii",
                StartDate = new DateTime(2024, 7, 15),
                EndDate = new DateTime(2024, 7, 22),
                CreatedAt = DateTime.UtcNow,
            },
            new Trip
            {
                TripId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "European Adventure",
                Destination = "Paris, France",
                StartDate = new DateTime(2024, 9, 1),
                EndDate = new DateTime(2024, 9, 15),
                CreatedAt = DateTime.UtcNow,
            },
            new Trip
            {
                TripId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Winter Ski Trip",
                Destination = "Aspen, Colorado",
                StartDate = new DateTime(2025, 1, 10),
                EndDate = new DateTime(2025, 1, 17),
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Trips.AddRange(trips);

        var itineraries = new List<Itinerary>
        {
            new Itinerary
            {
                ItineraryId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                Date = new DateTime(2024, 7, 16, 9, 0, 0),
                Activity = "Snorkeling at Molokini Crater",
                Location = "Molokini Crater, Maui",
                CreatedAt = DateTime.UtcNow,
            },
            new Itinerary
            {
                ItineraryId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                Date = new DateTime(2024, 7, 17, 14, 0, 0),
                Activity = "Road to Hana Tour",
                Location = "Hana Highway, Maui",
                CreatedAt = DateTime.UtcNow,
            },
            new Itinerary
            {
                ItineraryId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[1].TripId,
                Date = new DateTime(2024, 9, 2, 10, 0, 0),
                Activity = "Visit Eiffel Tower",
                Location = "Champ de Mars, Paris",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Itineraries.AddRange(itineraries);

        var bookings = new List<Booking>
        {
            new Booking
            {
                BookingId = Guid.Parse("aaa11111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                Type = "Hotel",
                ConfirmationNumber = "MAUI-12345",
                Cost = 2500.00m,
                CreatedAt = DateTime.UtcNow,
            },
            new Booking
            {
                BookingId = Guid.Parse("bbb22222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                Type = "Flight",
                ConfirmationNumber = "AA-67890",
                Cost = 1200.00m,
                CreatedAt = DateTime.UtcNow,
            },
            new Booking
            {
                BookingId = Guid.Parse("ccc33333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[1].TripId,
                Type = "Hotel",
                ConfirmationNumber = "PARIS-54321",
                Cost = 3200.00m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Bookings.AddRange(bookings);

        var budgets = new List<VacationBudget>
        {
            new VacationBudget
            {
                VacationBudgetId = Guid.Parse("111aaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                Category = "Accommodation",
                AllocatedAmount = 3000.00m,
                SpentAmount = 2500.00m,
                CreatedAt = DateTime.UtcNow,
            },
            new VacationBudget
            {
                VacationBudgetId = Guid.Parse("222bbbbb-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                Category = "Food & Dining",
                AllocatedAmount = 1500.00m,
                SpentAmount = 800.00m,
                CreatedAt = DateTime.UtcNow,
            },
            new VacationBudget
            {
                VacationBudgetId = Guid.Parse("333ccccc-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                Category = "Activities",
                AllocatedAmount = 1000.00m,
                SpentAmount = 400.00m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.VacationBudgets.AddRange(budgets);

        var packingLists = new List<PackingList>
        {
            new PackingList
            {
                PackingListId = Guid.Parse("11aa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                ItemName = "Swimsuit",
                IsPacked = true,
                CreatedAt = DateTime.UtcNow,
            },
            new PackingList
            {
                PackingListId = Guid.Parse("22bb2222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                ItemName = "Sunscreen",
                IsPacked = true,
                CreatedAt = DateTime.UtcNow,
            },
            new PackingList
            {
                PackingListId = Guid.Parse("33cc3333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                ItemName = "Camera",
                IsPacked = false,
                CreatedAt = DateTime.UtcNow,
            },
            new PackingList
            {
                PackingListId = Guid.Parse("44dd4444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TripId = trips[0].TripId,
                ItemName = "Beach Towels",
                IsPacked = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.PackingLists.AddRange(packingLists);

        await context.SaveChangesAsync();
    }
}
