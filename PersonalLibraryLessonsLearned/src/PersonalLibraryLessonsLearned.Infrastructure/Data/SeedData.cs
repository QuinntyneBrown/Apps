// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLibraryLessonsLearned.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PersonalLibraryLessonsLearned.Core.Model.UserAggregate;
using PersonalLibraryLessonsLearned.Core.Model.UserAggregate.Entities;
using PersonalLibraryLessonsLearned.Core.Services;
namespace PersonalLibraryLessonsLearned.Infrastructure;

/// <summary>
/// Provides seed data for the PersonalLibraryLessonsLearned database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PersonalLibraryLessonsLearnedContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Lessons.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedLessonsAsync(context);
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

    private static async Task SeedLessonsAsync(PersonalLibraryLessonsLearnedContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Add sample sources
        var sources = new List<Source>
        {
            new Source
            {
                SourceId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Atomic Habits",
                Author = "James Clear",
                SourceType = "Book",
                Url = "https://jamesclear.com/atomic-habits",
                DateConsumed = new DateTime(2024, 6, 15),
                Notes = "Excellent book on building better habits through small changes",
                CreatedAt = DateTime.UtcNow,
            },
            new Source
            {
                SourceId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "The Pragmatic Programmer",
                Author = "David Thomas, Andrew Hunt",
                SourceType = "Book",
                Url = "https://pragprog.com/titles/tpp20/",
                DateConsumed = new DateTime(2024, 8, 20),
                Notes = "Classic software engineering principles",
                CreatedAt = DateTime.UtcNow,
            },
            new Source
            {
                SourceId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "How to Take Smart Notes",
                Author = "Sönke Ahrens",
                SourceType = "Book",
                DateConsumed = new DateTime(2024, 9, 10),
                Notes = "Zettelkasten method for knowledge management",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Sources.AddRange(sources);

        // Add sample lessons
        var lessons = new List<Lesson>
        {
            new Lesson
            {
                LessonId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                SourceId = sources[0].SourceId,
                Title = "Make habits obvious",
                Content = "The more obvious a habit is, the more likely you are to follow through. Use implementation intentions: \"I will [BEHAVIOR] at [TIME] in [LOCATION]\"",
                Category = LessonCategory.Personal,
                Tags = "habits, behavior-change, productivity",
                DateLearned = new DateTime(2024, 6, 15),
                Application = "Created visual cues for morning routine and posted implementation intentions on bathroom mirror",
                IsApplied = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Lesson
            {
                LessonId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                SourceId = sources[0].SourceId,
                Title = "Use habit stacking",
                Content = "After [CURRENT HABIT], I will [NEW HABIT]. This leverages existing habits to build new ones.",
                Category = LessonCategory.Personal,
                Tags = "habits, productivity",
                DateLearned = new DateTime(2024, 6, 16),
                Application = "After pouring morning coffee, I will review my daily goals for 2 minutes",
                IsApplied = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Lesson
            {
                LessonId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                SourceId = sources[1].SourceId,
                Title = "Don't Repeat Yourself (DRY)",
                Content = "Every piece of knowledge must have a single, unambiguous, authoritative representation within a system.",
                Category = LessonCategory.Technical,
                Tags = "software-engineering, best-practices, code-quality",
                DateLearned = new DateTime(2024, 8, 21),
                Application = "Refactored duplicate validation logic into reusable utility functions",
                IsApplied = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Lesson
            {
                LessonId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                SourceId = sources[2].SourceId,
                Title = "Connect ideas instead of collecting them",
                Content = "The goal of note-taking isn't to collect information but to develop ideas, arguments, and discussions. Always link new notes to existing ones.",
                Category = LessonCategory.Learning,
                Tags = "knowledge-management, note-taking, learning",
                DateLearned = new DateTime(2024, 9, 12),
                Application = "Started using bidirectional links in my note-taking system",
                IsApplied = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Lessons.AddRange(lessons);

        // Add sample reminders for some lessons
        var reminders = new List<LessonReminder>
        {
            new LessonReminder
            {
                LessonReminderId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                LessonId = lessons[3].LessonId,
                UserId = sampleUserId,
                ReminderDateTime = DateTime.UtcNow.AddDays(7),
                Message = "Review and practice linking ideas in your notes",
                IsSent = false,
                IsDismissed = false,
                CreatedAt = DateTime.UtcNow,
            },
            new LessonReminder
            {
                LessonReminderId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                LessonId = lessons[0].LessonId,
                UserId = sampleUserId,
                ReminderDateTime = DateTime.UtcNow.AddDays(30),
                Message = "Review habit implementation intentions and adjust if needed",
                IsSent = false,
                IsDismissed = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Reminders.AddRange(reminders);

        await context.SaveChangesAsync();
    }
}
