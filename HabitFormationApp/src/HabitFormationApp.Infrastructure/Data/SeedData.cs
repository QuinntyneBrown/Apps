// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HabitFormationApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using HabitFormationApp.Core.Model.UserAggregate;
using HabitFormationApp.Core.Model.UserAggregate.Entities;
using HabitFormationApp.Core.Services;
namespace HabitFormationApp.Infrastructure;

/// <summary>
/// Provides seed data for the HabitFormationApp database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HabitFormationAppContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Habits.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedHabitsAsync(context);
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

    private static async Task SeedHabitsAsync(HabitFormationAppContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var habits = new List<Habit>
        {
            new Habit
            {
                HabitId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Morning Exercise",
                Description = "30 minutes of cardio or strength training",
                Frequency = HabitFrequency.Daily,
                TargetDaysPerWeek = 7,
                StartDate = DateTime.UtcNow.AddDays(-30),
                IsActive = true,
                Notes = "Best time is 6:00 AM",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
            new Habit
            {
                HabitId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Read for 30 Minutes",
                Description = "Read books or articles for personal development",
                Frequency = HabitFrequency.Daily,
                TargetDaysPerWeek = 7,
                StartDate = DateTime.UtcNow.AddDays(-20),
                IsActive = true,
                Notes = "Before bedtime",
                CreatedAt = DateTime.UtcNow.AddDays(-20),
            },
            new Habit
            {
                HabitId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Meditate",
                Description = "10 minutes of mindfulness meditation",
                Frequency = HabitFrequency.Daily,
                TargetDaysPerWeek = 7,
                StartDate = DateTime.UtcNow.AddDays(-15),
                IsActive = true,
                Notes = "Use Headspace app",
                CreatedAt = DateTime.UtcNow.AddDays(-15),
            },
            new Habit
            {
                HabitId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "Drink 8 Glasses of Water",
                Description = "Stay hydrated throughout the day",
                Frequency = HabitFrequency.Daily,
                TargetDaysPerWeek = 7,
                StartDate = DateTime.UtcNow.AddDays(-10),
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new Habit
            {
                HabitId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Name = "Practice Guitar",
                Description = "Practice guitar for skill improvement",
                Frequency = HabitFrequency.Weekly,
                TargetDaysPerWeek = 3,
                StartDate = DateTime.UtcNow.AddDays(-45),
                IsActive = true,
                Notes = "Focus on scales and chord progressions",
                CreatedAt = DateTime.UtcNow.AddDays(-45),
            },
        };

        context.Habits.AddRange(habits);

        // Add streaks for the habits
        var streaks = new List<Streak>
        {
            new Streak
            {
                StreakId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                HabitId = habits[0].HabitId,
                CurrentStreak = 15,
                LongestStreak = 22,
                LastCompletedDate = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
            new Streak
            {
                StreakId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                HabitId = habits[1].HabitId,
                CurrentStreak = 12,
                LongestStreak = 18,
                LastCompletedDate = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-20),
            },
            new Streak
            {
                StreakId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                HabitId = habits[2].HabitId,
                CurrentStreak = 8,
                LongestStreak = 10,
                LastCompletedDate = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-15),
            },
            new Streak
            {
                StreakId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                HabitId = habits[3].HabitId,
                CurrentStreak = 5,
                LongestStreak = 7,
                LastCompletedDate = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new Streak
            {
                StreakId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                HabitId = habits[4].HabitId,
                CurrentStreak = 3,
                LongestStreak = 5,
                LastCompletedDate = DateTime.UtcNow.AddDays(-2),
                CreatedAt = DateTime.UtcNow.AddDays(-45),
            },
        };

        context.Streaks.AddRange(streaks);

        // Add reminders
        var reminders = new List<Reminder>
        {
            new Reminder
            {
                ReminderId = Guid.Parse("11111111-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                HabitId = habits[0].HabitId,
                ReminderTime = new TimeSpan(6, 0, 0),
                Message = "Time for your morning exercise!",
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
            new Reminder
            {
                ReminderId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                HabitId = habits[1].HabitId,
                ReminderTime = new TimeSpan(21, 0, 0),
                Message = "Don't forget to read for 30 minutes",
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-20),
            },
            new Reminder
            {
                ReminderId = Guid.Parse("33333333-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                HabitId = habits[2].HabitId,
                ReminderTime = new TimeSpan(7, 30, 0),
                Message = "Time to meditate",
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
            },
        };

        context.Reminders.AddRange(reminders);

        await context.SaveChangesAsync();
    }
}
