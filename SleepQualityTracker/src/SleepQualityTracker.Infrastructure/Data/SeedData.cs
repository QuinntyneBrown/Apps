// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SleepQualityTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SleepQualityTracker.Core.Model.UserAggregate;
using SleepQualityTracker.Core.Model.UserAggregate.Entities;
using SleepQualityTracker.Core.Services;
namespace SleepQualityTracker.Infrastructure;

/// <summary>
/// Provides seed data for the SleepQualityTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(SleepQualityTrackerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.SleepSessions.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedSleepSessionsAsync(context);
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

    private static async Task SeedSleepSessionsAsync(SleepQualityTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var sleepSessions = new List<SleepSession>
        {
            new SleepSession
            {
                SleepSessionId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Bedtime = new DateTime(2024, 3, 1, 23, 0, 0),
                WakeTime = new DateTime(2024, 3, 2, 7, 30, 0),
                TotalSleepMinutes = 480,
                SleepQuality = SleepQuality.Good,
                TimesAwakened = 1,
                DeepSleepMinutes = 120,
                RemSleepMinutes = 90,
                SleepEfficiency = 94m,
                Notes = "Felt well-rested",
                CreatedAt = DateTime.UtcNow,
            },
            new SleepSession
            {
                SleepSessionId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Bedtime = new DateTime(2024, 3, 2, 22, 30, 0),
                WakeTime = new DateTime(2024, 3, 3, 6, 45, 0),
                TotalSleepMinutes = 465,
                SleepQuality = SleepQuality.Excellent,
                TimesAwakened = 0,
                DeepSleepMinutes = 135,
                RemSleepMinutes = 105,
                SleepEfficiency = 97m,
                Notes = "Best sleep in weeks",
                CreatedAt = DateTime.UtcNow,
            },
            new SleepSession
            {
                SleepSessionId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Bedtime = new DateTime(2024, 3, 3, 1, 0, 0),
                WakeTime = new DateTime(2024, 3, 3, 8, 0, 0),
                TotalSleepMinutes = 360,
                SleepQuality = SleepQuality.Fair,
                TimesAwakened = 3,
                DeepSleepMinutes = 75,
                RemSleepMinutes = 60,
                SleepEfficiency = 86m,
                Notes = "Went to bed too late, felt groggy",
                CreatedAt = DateTime.UtcNow,
            },
            new SleepSession
            {
                SleepSessionId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Bedtime = new DateTime(2024, 3, 4, 23, 15, 0),
                WakeTime = new DateTime(2024, 3, 5, 7, 0, 0),
                TotalSleepMinutes = 435,
                SleepQuality = SleepQuality.Good,
                TimesAwakened = 2,
                DeepSleepMinutes = 110,
                RemSleepMinutes = 85,
                SleepEfficiency = 92m,
                CreatedAt = DateTime.UtcNow,
            },
            new SleepSession
            {
                SleepSessionId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Bedtime = new DateTime(2024, 3, 5, 22, 45, 0),
                WakeTime = new DateTime(2024, 3, 6, 6, 30, 0),
                TotalSleepMinutes = 450,
                SleepQuality = SleepQuality.Good,
                TimesAwakened = 1,
                DeepSleepMinutes = 125,
                RemSleepMinutes = 95,
                SleepEfficiency = 96m,
                Notes = "Consistent sleep schedule paying off",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.SleepSessions.AddRange(sleepSessions);

        // Add sample habits
        var habits = new List<Habit>
        {
            new Habit
            {
                HabitId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Evening Caffeine",
                Description = "Coffee or tea after 4 PM",
                HabitType = "Caffeine",
                IsPositive = false,
                TypicalTime = new TimeSpan(16, 0, 0),
                ImpactLevel = 4,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Habit
            {
                HabitId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Evening Exercise",
                Description = "Light yoga or stretching before bed",
                HabitType = "Exercise",
                IsPositive = true,
                TypicalTime = new TimeSpan(21, 0, 0),
                ImpactLevel = 5,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Habit
            {
                HabitId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Screen Time",
                Description = "Phone or TV use before bed",
                HabitType = "Electronics",
                IsPositive = false,
                TypicalTime = new TimeSpan(22, 30, 0),
                ImpactLevel = 3,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Habit
            {
                HabitId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Reading",
                Description = "Reading a book before sleep",
                HabitType = "Relaxation",
                IsPositive = true,
                TypicalTime = new TimeSpan(22, 0, 0),
                ImpactLevel = 4,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Habits.AddRange(habits);

        // Add sample patterns
        var patterns = new List<Pattern>
        {
            new Pattern
            {
                PatternId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Weekday Sleep Consistency",
                Description = "Better sleep quality on weekdays with consistent schedule",
                PatternType = "Weekly",
                StartDate = new DateTime(2024, 2, 1),
                EndDate = new DateTime(2024, 3, 1),
                ConfidenceLevel = 85,
                Insights = "Maintaining a consistent bedtime of 11 PM on weekdays results in better sleep efficiency. Consider applying the same schedule to weekends.",
                CreatedAt = DateTime.UtcNow,
            },
            new Pattern
            {
                PatternId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Caffeine Impact",
                Description = "Evening caffeine consumption correlates with reduced deep sleep",
                PatternType = "Daily",
                StartDate = new DateTime(2024, 1, 15),
                EndDate = new DateTime(2024, 3, 1),
                ConfidenceLevel = 92,
                Insights = "On days when caffeine is consumed after 4 PM, deep sleep decreases by an average of 25%. Recommend cutting off caffeine by 2 PM.",
                CreatedAt = DateTime.UtcNow,
            },
            new Pattern
            {
                PatternId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Weekend Sleep Debt",
                Description = "Sleeping in on weekends indicates weekday sleep debt",
                PatternType = "Weekly",
                StartDate = new DateTime(2024, 2, 1),
                EndDate = new DateTime(2024, 3, 6),
                ConfidenceLevel = 78,
                Insights = "Waking up 2+ hours later on weekends suggests insufficient weekday sleep. Try going to bed 30 minutes earlier on weeknights.",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Patterns.AddRange(patterns);

        await context.SaveChangesAsync();
    }
}
