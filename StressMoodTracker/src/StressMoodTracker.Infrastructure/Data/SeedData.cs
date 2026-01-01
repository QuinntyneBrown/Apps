// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using StressMoodTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using StressMoodTracker.Core.Model.UserAggregate;
using StressMoodTracker.Core.Model.UserAggregate.Entities;
using StressMoodTracker.Core.Services;
namespace StressMoodTracker.Infrastructure;

/// <summary>
/// Provides seed data for the StressMoodTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(StressMoodTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.MoodEntries.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedMoodEntriesAsync(context);
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

    private static async Task SeedMoodEntriesAsync(StressMoodTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var moodEntries = new List<MoodEntry>
        {
            new MoodEntry
            {
                MoodEntryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MoodLevel = MoodLevel.Good,
                StressLevel = StressLevel.Low,
                EntryTime = DateTime.UtcNow.AddDays(-7),
                Notes = "Had a great day at work, completed several tasks",
                Activities = "Work, Exercise, Reading",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new MoodEntry
            {
                MoodEntryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                MoodLevel = MoodLevel.Neutral,
                StressLevel = StressLevel.Moderate,
                EntryTime = DateTime.UtcNow.AddDays(-6),
                Notes = "Busy day with meetings",
                Activities = "Work, Meetings",
                CreatedAt = DateTime.UtcNow.AddDays(-6),
            },
            new MoodEntry
            {
                MoodEntryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                MoodLevel = MoodLevel.Poor,
                StressLevel = StressLevel.High,
                EntryTime = DateTime.UtcNow.AddDays(-5),
                Notes = "Stressful deadline approaching",
                Activities = "Work, Late hours",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new MoodEntry
            {
                MoodEntryId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                MoodLevel = MoodLevel.Excellent,
                StressLevel = StressLevel.Low,
                EntryTime = DateTime.UtcNow.AddDays(-4),
                Notes = "Finished project successfully, went for a long walk",
                Activities = "Work, Exercise, Socializing",
                CreatedAt = DateTime.UtcNow.AddDays(-4),
            },
            new MoodEntry
            {
                MoodEntryId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                MoodLevel = MoodLevel.Good,
                StressLevel = StressLevel.Moderate,
                EntryTime = DateTime.UtcNow.AddDays(-3),
                Notes = "Normal day, balanced activities",
                Activities = "Work, Exercise, Family time",
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
        };

        context.MoodEntries.AddRange(moodEntries);

        var triggers = new List<Trigger>
        {
            new Trigger
            {
                TriggerId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Work Deadlines",
                Description = "Tight deadlines at work cause stress",
                TriggerType = "Work",
                ImpactLevel = 4,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new Trigger
            {
                TriggerId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Lack of Sleep",
                Description = "Not getting enough sleep affects mood",
                TriggerType = "Health",
                ImpactLevel = 5,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new Trigger
            {
                TriggerId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Exercise",
                Description = "Regular exercise improves mood",
                TriggerType = "Health",
                ImpactLevel = 4,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
        };

        context.Triggers.AddRange(triggers);

        var journals = new List<Journal>
        {
            new Journal
            {
                JournalId = Guid.Parse("aaaaaaaa-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Reflection on Work-Life Balance",
                Content = "Today I realized that I need to focus more on maintaining a healthy work-life balance. The past few weeks have been intense with project deadlines, but I've noticed a positive change when I take time for exercise and family.",
                EntryDate = DateTime.UtcNow.AddDays(-2),
                Tags = "work-life-balance, reflection, self-care",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
            new Journal
            {
                JournalId = Guid.Parse("bbbbbbbb-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "Gratitude Practice",
                Content = "Three things I'm grateful for today: 1) Supportive colleagues who helped with the project, 2) A beautiful morning walk, 3) Quality time with family in the evening.",
                EntryDate = DateTime.UtcNow.AddDays(-1),
                Tags = "gratitude, positivity",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            },
        };

        context.Journals.AddRange(journals);

        await context.SaveChangesAsync();
    }
}
