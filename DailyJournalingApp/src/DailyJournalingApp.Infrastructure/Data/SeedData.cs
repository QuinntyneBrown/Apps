// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DailyJournalingApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Infrastructure;

/// <summary>
/// Provides seed data for the DailyJournalingApp database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(DailyJournalingAppContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Prompts.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedPromptsAsync(context);
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

    private static async Task SeedPromptsAsync(DailyJournalingAppContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Seed system prompts
        var prompts = new List<Prompt>
        {
            new Prompt
            {
                PromptId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Text = "What are three things you're grateful for today?",
                Category = "Gratitude",
                IsSystemPrompt = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Prompt
            {
                PromptId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Text = "Describe a challenge you faced today and how you overcame it.",
                Category = "Growth",
                IsSystemPrompt = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Prompt
            {
                PromptId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Text = "What made you smile today?",
                Category = "Positivity",
                IsSystemPrompt = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Prompt
            {
                PromptId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Text = "What is one thing you learned today?",
                Category = "Learning",
                IsSystemPrompt = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Prompt
            {
                PromptId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                Text = "How are you feeling right now and why?",
                Category = "Self-Reflection",
                IsSystemPrompt = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Prompts.AddRange(prompts);

        // Seed sample tags
        var tags = new List<Tag>
        {
            new Tag
            {
                TagId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Work",
                Color = "#3498db",
                UsageCount = 0,
                CreatedAt = DateTime.UtcNow,
            },
            new Tag
            {
                TagId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Personal",
                Color = "#2ecc71",
                UsageCount = 0,
                CreatedAt = DateTime.UtcNow,
            },
            new Tag
            {
                TagId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Health",
                Color = "#e74c3c",
                UsageCount = 0,
                CreatedAt = DateTime.UtcNow,
            },
            new Tag
            {
                TagId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Relationships",
                Color = "#9b59b6",
                UsageCount = 0,
                CreatedAt = DateTime.UtcNow,
            },
            new Tag
            {
                TagId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Goals",
                Color = "#f39c12",
                UsageCount = 0,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Tags.AddRange(tags);

        // Seed sample journal entries
        var entries = new List<JournalEntry>
        {
            new JournalEntry
            {
                JournalEntryId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "A Great Day",
                Content = "Today was amazing! I completed my project at work and received positive feedback from my manager. It feels great to be recognized for my hard work.",
                EntryDate = DateTime.UtcNow.AddDays(-2),
                Mood = Mood.VeryHappy,
                PromptId = prompts[0].PromptId,
                Tags = "Work,Goals",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
            new JournalEntry
            {
                JournalEntryId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Reflection on Growth",
                Content = "I faced a difficult conversation today with a colleague. Initially, I felt anxious, but I approached it with empathy and active listening. We found a resolution together.",
                EntryDate = DateTime.UtcNow.AddDays(-1),
                Mood = Mood.Calm,
                PromptId = prompts[1].PromptId,
                Tags = "Work,Personal",
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            },
            new JournalEntry
            {
                JournalEntryId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Morning Thoughts",
                Content = "Started my day with meditation and a healthy breakfast. I'm grateful for my family, my health, and the opportunities I have.",
                EntryDate = DateTime.UtcNow,
                Mood = Mood.Happy,
                PromptId = prompts[0].PromptId,
                Tags = "Health,Personal",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.JournalEntries.AddRange(entries);

        await context.SaveChangesAsync();
    }
}
