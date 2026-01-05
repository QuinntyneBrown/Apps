// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FocusSessionTracker.Core.Models.UserAggregate;
using FocusSessionTracker.Core.Models.UserAggregate.Entities;
using FocusSessionTracker.Core.Services;
namespace FocusSessionTracker.Infrastructure;

/// <summary>
/// Provides seed data for the FocusSessionTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FocusSessionTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Sessions.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedSessionsAsync(context);
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

    private static async Task SeedSessionsAsync(FocusSessionTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var sessions = new List<FocusSession>
        {
            new FocusSession
            {
                FocusSessionId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                SessionType = SessionType.DeepWork,
                Name = "Writing project proposal",
                PlannedDurationMinutes = 90,
                StartTime = DateTime.UtcNow.AddDays(-5).AddHours(9),
                EndTime = DateTime.UtcNow.AddDays(-5).AddHours(10).AddMinutes(30),
                Notes = "Very productive session, completed first draft",
                FocusScore = 9,
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new FocusSession
            {
                FocusSessionId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                SessionType = SessionType.Pomodoro,
                Name = "Code review and testing",
                PlannedDurationMinutes = 25,
                StartTime = DateTime.UtcNow.AddDays(-3).AddHours(14),
                EndTime = DateTime.UtcNow.AddDays(-3).AddHours(14).AddMinutes(25),
                Notes = "Completed one Pomodoro cycle",
                FocusScore = 8,
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new FocusSession
            {
                FocusSessionId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                SessionType = SessionType.Learning,
                Name = "Study new framework documentation",
                PlannedDurationMinutes = 60,
                StartTime = DateTime.UtcNow.AddDays(-2).AddHours(10),
                EndTime = DateTime.UtcNow.AddDays(-2).AddHours(10).AddMinutes(45),
                Notes = "Got interrupted but made good progress",
                FocusScore = 6,
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
            new FocusSession
            {
                FocusSessionId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                SessionType = SessionType.DeepWork,
                Name = "Design system architecture",
                PlannedDurationMinutes = 120,
                StartTime = DateTime.UtcNow.AddDays(-1).AddHours(8),
                EndTime = DateTime.UtcNow.AddDays(-1).AddHours(10).AddMinutes(15),
                Notes = "Excellent flow state achieved",
                FocusScore = 10,
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            },
        };

        context.Sessions.AddRange(sessions);

        var distractions = new List<Distraction>
        {
            new Distraction
            {
                DistractionId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                FocusSessionId = sessions[0].FocusSessionId,
                Type = "Email notification",
                Description = "Urgent email from client",
                OccurredAt = sessions[0].StartTime.AddMinutes(30),
                DurationMinutes = 5,
                IsInternal = false,
                CreatedAt = sessions[0].StartTime.AddMinutes(30),
            },
            new Distraction
            {
                DistractionId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                FocusSessionId = sessions[2].FocusSessionId,
                Type = "Phone call",
                Description = "Unexpected call from colleague",
                OccurredAt = sessions[2].StartTime.AddMinutes(40),
                DurationMinutes = 15,
                IsInternal = false,
                CreatedAt = sessions[2].StartTime.AddMinutes(40),
            },
            new Distraction
            {
                DistractionId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                FocusSessionId = sessions[2].FocusSessionId,
                Type = "Mind wandering",
                Description = "Thinking about weekend plans",
                OccurredAt = sessions[2].StartTime.AddMinutes(20),
                DurationMinutes = 3,
                IsInternal = true,
                CreatedAt = sessions[2].StartTime.AddMinutes(20),
            },
        };

        context.Distractions.AddRange(distractions);

        var analytics = new List<SessionAnalytics>
        {
            new SessionAnalytics
            {
                SessionAnalyticsId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                PeriodStartDate = DateTime.UtcNow.AddDays(-7).Date,
                PeriodEndDate = DateTime.UtcNow.Date,
                TotalSessions = 12,
                TotalFocusMinutes = 720,
                AverageFocusScore = 7.5,
                TotalDistractions = 8,
                CompletionRate = 91.67,
                MostProductiveSessionType = SessionType.DeepWork,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Analytics.AddRange(analytics);

        await context.SaveChangesAsync();
    }
}
