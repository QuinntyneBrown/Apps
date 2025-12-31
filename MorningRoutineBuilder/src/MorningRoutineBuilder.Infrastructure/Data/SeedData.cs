// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MorningRoutineBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MorningRoutineBuilder.Core.Model.UserAggregate;
using MorningRoutineBuilder.Core.Model.UserAggregate.Entities;
using MorningRoutineBuilder.Core.Services;
namespace MorningRoutineBuilder.Infrastructure;

/// <summary>
/// Provides seed data for the MorningRoutineBuilder database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MorningRoutineBuilderContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Routines.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedRoutinesAsync(context);
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

    private static async Task SeedRoutinesAsync(MorningRoutineBuilderContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Create routines
        var routines = new List<Routine>
        {
            new Routine
            {
                RoutineId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Energizing Morning Routine",
                Description = "A routine to start the day with energy and focus",
                TargetStartTime = new TimeSpan(6, 0, 0), // 6:00 AM
                EstimatedDurationMinutes = 60,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Routine
            {
                RoutineId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Quick Morning Routine",
                Description = "A condensed routine for busy mornings",
                TargetStartTime = new TimeSpan(7, 0, 0), // 7:00 AM
                EstimatedDurationMinutes = 30,
                IsActive = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Routines.AddRange(routines);

        // Create routine tasks
        var tasks = new List<RoutineTask>
        {
            new RoutineTask
            {
                RoutineTaskId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoutineId = routines[0].RoutineId,
                Name = "Drink water",
                TaskType = TaskType.Hydration,
                Description = "Drink a full glass of water",
                EstimatedDurationMinutes = 2,
                SortOrder = 1,
                IsOptional = false,
                CreatedAt = DateTime.UtcNow,
            },
            new RoutineTask
            {
                RoutineTaskId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoutineId = routines[0].RoutineId,
                Name = "Meditation",
                TaskType = TaskType.Mindfulness,
                Description = "10 minutes of guided meditation",
                EstimatedDurationMinutes = 10,
                SortOrder = 2,
                IsOptional = false,
                CreatedAt = DateTime.UtcNow,
            },
            new RoutineTask
            {
                RoutineTaskId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoutineId = routines[0].RoutineId,
                Name = "Exercise",
                TaskType = TaskType.Exercise,
                Description = "20 minutes of cardio or strength training",
                EstimatedDurationMinutes = 20,
                SortOrder = 3,
                IsOptional = false,
                CreatedAt = DateTime.UtcNow,
            },
            new RoutineTask
            {
                RoutineTaskId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoutineId = routines[0].RoutineId,
                Name = "Shower and grooming",
                TaskType = TaskType.Hygiene,
                Description = "Morning shower and personal care",
                EstimatedDurationMinutes = 15,
                SortOrder = 4,
                IsOptional = false,
                CreatedAt = DateTime.UtcNow,
            },
            new RoutineTask
            {
                RoutineTaskId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoutineId = routines[0].RoutineId,
                Name = "Healthy breakfast",
                TaskType = TaskType.Nutrition,
                Description = "Prepare and eat a nutritious breakfast",
                EstimatedDurationMinutes = 15,
                SortOrder = 5,
                IsOptional = false,
                CreatedAt = DateTime.UtcNow,
            },
            new RoutineTask
            {
                RoutineTaskId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoutineId = routines[0].RoutineId,
                Name = "Review daily goals",
                TaskType = TaskType.Planning,
                Description = "Review and plan the day ahead",
                EstimatedDurationMinutes = 5,
                SortOrder = 6,
                IsOptional = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Tasks.AddRange(tasks);

        // Create completion logs
        var completionLogs = new List<CompletionLog>
        {
            new CompletionLog
            {
                CompletionLogId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoutineId = routines[0].RoutineId,
                UserId = sampleUserId,
                CompletionDate = DateTime.UtcNow.Date.AddDays(-1),
                ActualStartTime = DateTime.UtcNow.AddDays(-1).Date.AddHours(6).AddMinutes(15),
                ActualEndTime = DateTime.UtcNow.AddDays(-1).Date.AddHours(7).AddMinutes(20),
                TasksCompleted = 6,
                TotalTasks = 6,
                MoodRating = 8,
                Notes = "Felt great after the workout!",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            },
            new CompletionLog
            {
                CompletionLogId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoutineId = routines[0].RoutineId,
                UserId = sampleUserId,
                CompletionDate = DateTime.UtcNow.Date.AddDays(-2),
                ActualStartTime = DateTime.UtcNow.AddDays(-2).Date.AddHours(6),
                ActualEndTime = DateTime.UtcNow.AddDays(-2).Date.AddHours(7),
                TasksCompleted = 5,
                TotalTasks = 6,
                MoodRating = 7,
                Notes = "Skipped the planning task due to time constraints",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
        };

        context.CompletionLogs.AddRange(completionLogs);

        // Create streak
        var streak = new Streak
        {
            StreakId = Guid.Parse("99999999-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            RoutineId = routines[0].RoutineId,
            UserId = sampleUserId,
            CurrentStreak = 2,
            LongestStreak = 7,
            LastCompletionDate = DateTime.UtcNow.Date.AddDays(-1),
            StreakStartDate = DateTime.UtcNow.Date.AddDays(-2),
            IsActive = true,
            CreatedAt = DateTime.UtcNow.AddDays(-7),
        };

        context.Streaks.Add(streak);

        await context.SaveChangesAsync();
    }
}
