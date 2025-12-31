// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using KidsActivitySportsTracker.Core.Model.UserAggregate;
using KidsActivitySportsTracker.Core.Model.UserAggregate.Entities;
using KidsActivitySportsTracker.Core.Services;
namespace KidsActivitySportsTracker.Infrastructure;

/// <summary>
/// Provides seed data for the KidsActivitySportsTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(KidsActivitySportsTrackerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Activities.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedActivitiesAsync(context);
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

    private static async Task SeedActivitiesAsync(KidsActivitySportsTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var activities = new List<Activity>
        {
            new Activity
            {
                ActivityId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ChildName = "Emma",
                Name = "Soccer",
                ActivityType = ActivityType.Team,
                Organization = "Youth Soccer League",
                CoachName = "Coach Smith",
                CoachContact = "coach.smith@example.com",
                Season = "Fall 2024",
                StartDate = new DateTime(2024, 9, 1),
                EndDate = new DateTime(2024, 11, 30),
                Notes = "Practice on Tuesdays and Thursdays, games on Saturdays",
                CreatedAt = DateTime.UtcNow,
            },
            new Activity
            {
                ActivityId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                ChildName = "Liam",
                Name = "Piano Lessons",
                ActivityType = ActivityType.Individual,
                Organization = "Music Academy",
                CoachName = "Ms. Johnson",
                CoachContact = "johnson@musicacademy.com",
                Season = "Year-round",
                StartDate = new DateTime(2024, 1, 15),
                Notes = "Weekly 30-minute lessons",
                CreatedAt = DateTime.UtcNow,
            },
            new Activity
            {
                ActivityId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                ChildName = "Sophia",
                Name = "Ballet",
                ActivityType = ActivityType.Class,
                Organization = "Dance Studio",
                CoachName = "Miss Anderson",
                CoachContact = "anderson@dancestudio.com",
                Season = "Spring 2024",
                StartDate = new DateTime(2024, 3, 1),
                EndDate = new DateTime(2024, 5, 31),
                Notes = "Preparing for spring recital",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Activities.AddRange(activities);

        // Add sample schedules for the first activity
        var schedules = new List<Schedule>
        {
            new Schedule
            {
                ScheduleId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ActivityId = activities[0].ActivityId,
                EventType = "Practice",
                DateTime = new DateTime(2024, 9, 3, 17, 0, 0),
                Location = "Field 3, Community Sports Complex",
                DurationMinutes = 90,
                IsConfirmed = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Schedule
            {
                ScheduleId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ActivityId = activities[0].ActivityId,
                EventType = "Game",
                DateTime = new DateTime(2024, 9, 7, 9, 0, 0),
                Location = "Main Stadium",
                DurationMinutes = 60,
                Notes = "Home game vs. Blue Eagles",
                IsConfirmed = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Schedules.AddRange(schedules);

        // Add sample carpool
        var carpools = new List<Carpool>
        {
            new Carpool
            {
                CarpoolId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Soccer Practice Carpool",
                DriverName = "John Doe",
                DriverContact = "john.doe@example.com",
                PickupTime = new DateTime(2024, 9, 3, 16, 30, 0),
                PickupLocation = "123 Main Street",
                DropoffTime = new DateTime(2024, 9, 3, 18, 45, 0),
                DropoffLocation = "123 Main Street",
                Participants = "Emma, Sarah, Michael",
                Notes = "Rotating drivers - check weekly schedule",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Carpools.AddRange(carpools);

        await context.SaveChangesAsync();
    }
}
