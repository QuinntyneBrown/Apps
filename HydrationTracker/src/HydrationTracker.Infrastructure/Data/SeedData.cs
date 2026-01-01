// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using HydrationTracker.Core.Model.UserAggregate;
using HydrationTracker.Core.Model.UserAggregate.Entities;
using HydrationTracker.Core.Services;
namespace HydrationTracker.Infrastructure;

/// <summary>
/// Provides seed data for the HydrationTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HydrationTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Goals.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedHydrationDataAsync(context);
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

    private static async Task SeedHydrationDataAsync(HydrationTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Seed Goals
        var goals = new List<Goal>
        {
            new Goal
            {
                GoalId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                DailyGoalMl = 2000m,
                StartDate = DateTime.UtcNow.Date,
                IsActive = true,
                Notes = "Standard daily hydration goal",
                CreatedAt = DateTime.UtcNow,
            },
            new Goal
            {
                GoalId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                DailyGoalMl = 2500m,
                StartDate = DateTime.UtcNow.Date.AddDays(-30),
                IsActive = false,
                Notes = "Previous higher goal",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
        };

        context.Goals.AddRange(goals);

        // Seed Intakes
        var intakes = new List<Intake>
        {
            new Intake
            {
                IntakeId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BeverageType = BeverageType.Water,
                AmountMl = 250m,
                IntakeTime = DateTime.UtcNow.AddHours(-6),
                Notes = "Morning glass of water",
                CreatedAt = DateTime.UtcNow.AddHours(-6),
            },
            new Intake
            {
                IntakeId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BeverageType = BeverageType.Coffee,
                AmountMl = 200m,
                IntakeTime = DateTime.UtcNow.AddHours(-5),
                Notes = "Morning coffee",
                CreatedAt = DateTime.UtcNow.AddHours(-5),
            },
            new Intake
            {
                IntakeId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BeverageType = BeverageType.Water,
                AmountMl = 500m,
                IntakeTime = DateTime.UtcNow.AddHours(-3),
                Notes = "Water bottle at work",
                CreatedAt = DateTime.UtcNow.AddHours(-3),
            },
            new Intake
            {
                IntakeId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BeverageType = BeverageType.Juice,
                AmountMl = 300m,
                IntakeTime = DateTime.UtcNow.AddHours(-2),
                Notes = "Orange juice with lunch",
                CreatedAt = DateTime.UtcNow.AddHours(-2),
            },
            new Intake
            {
                IntakeId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BeverageType = BeverageType.Water,
                AmountMl = 250m,
                IntakeTime = DateTime.UtcNow.AddHours(-1),
                Notes = "Afternoon hydration",
                CreatedAt = DateTime.UtcNow.AddHours(-1),
            },
        };

        context.Intakes.AddRange(intakes);

        // Seed Reminders
        var reminders = new List<Reminder>
        {
            new Reminder
            {
                ReminderId = Guid.Parse("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReminderTime = new TimeSpan(9, 0, 0),
                Message = "Time for your morning hydration!",
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Reminder
            {
                ReminderId = Guid.Parse("bbbb1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReminderTime = new TimeSpan(12, 0, 0),
                Message = "Drink water with lunch",
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Reminder
            {
                ReminderId = Guid.Parse("cccc1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReminderTime = new TimeSpan(15, 0, 0),
                Message = "Afternoon hydration break",
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Reminder
            {
                ReminderId = Guid.Parse("dddd1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReminderTime = new TimeSpan(18, 0, 0),
                Message = "Evening water reminder",
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Reminders.AddRange(reminders);

        await context.SaveChangesAsync();
    }
}
