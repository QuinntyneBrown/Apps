// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RunningLogRaceTracker.Core;

using RunningLogRaceTracker.Core.Model.UserAggregate;
using RunningLogRaceTracker.Core.Model.UserAggregate.Entities;
using RunningLogRaceTracker.Core.Services;
namespace RunningLogRaceTracker.Infrastructure;

/// <summary>
/// Provides seed data for the RunningLogRaceTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(RunningLogRaceTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Runs.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedDataAsync(context);
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

    private static async Task SeedDataAsync(RunningLogRaceTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var races = new List<Race>
        {
            new Race
            {
                RaceId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Boston Marathon 2025",
                RaceType = RaceType.Marathon,
                RaceDate = new DateTime(2025, 4, 21),
                Location = "Boston, MA",
                Distance = 42.2m,
                GoalTimeMinutes = 240,
                IsCompleted = false,
                Notes = "First marathon attempt",
                CreatedAt = DateTime.UtcNow,
            },
            new Race
            {
                RaceId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Local 5K Fun Run",
                RaceType = RaceType.FiveK,
                RaceDate = new DateTime(2024, 10, 15),
                Location = "Central Park, NY",
                Distance = 5.0m,
                FinishTimeMinutes = 25,
                GoalTimeMinutes = 26,
                Placement = 15,
                IsCompleted = true,
                Notes = "Great weather, beat my goal!",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Races.AddRange(races);

        var trainingPlans = new List<TrainingPlan>
        {
            new TrainingPlan
            {
                TrainingPlanId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Marathon Training Plan",
                RaceId = races[0].RaceId,
                StartDate = new DateTime(2025, 1, 1),
                EndDate = new DateTime(2025, 4, 20),
                WeeklyMileageGoal = 50,
                PlanDetails = "{\"weeks\": 16, \"peakMileage\": 60}",
                IsActive = true,
                Notes = "16-week marathon prep",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.TrainingPlans.AddRange(trainingPlans);

        var runs = new List<Run>
        {
            new Run
            {
                RunId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Distance = 8.5m,
                DurationMinutes = 48,
                CompletedAt = DateTime.UtcNow.AddDays(-1),
                AveragePace = 5.65m,
                AverageHeartRate = 155,
                ElevationGain = 120,
                CaloriesBurned = 650,
                Route = "Riverside Path",
                Weather = "Sunny, 18°C",
                Notes = "Easy recovery run",
                EffortRating = 6,
                CreatedAt = DateTime.UtcNow,
            },
            new Run
            {
                RunId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Distance = 12.0m,
                DurationMinutes = 65,
                CompletedAt = DateTime.UtcNow.AddDays(-3),
                AveragePace = 5.42m,
                AverageHeartRate = 162,
                ElevationGain = 200,
                CaloriesBurned = 920,
                Route = "Mountain Trail",
                Weather = "Cloudy, 15°C",
                Notes = "Long run - felt strong!",
                EffortRating = 8,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Runs.AddRange(runs);

        await context.SaveChangesAsync();
    }
}
