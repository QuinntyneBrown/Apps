// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MarriageEnrichmentJournal.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MarriageEnrichmentJournal.Core.Model.UserAggregate;
using MarriageEnrichmentJournal.Core.Model.UserAggregate.Entities;
using MarriageEnrichmentJournal.Core.Services;
namespace MarriageEnrichmentJournal.Infrastructure;

/// <summary>
/// Provides seed data for the MarriageEnrichmentJournal database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MarriageEnrichmentJournalContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.JournalEntries.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedJournalEntriesAsync(context);
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

    private static async Task SeedJournalEntriesAsync(MarriageEnrichmentJournalContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var journalEntries = new List<JournalEntry>
        {
            new JournalEntry
            {
                JournalEntryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Our First Year Together",
                Content = "Reflecting on the wonderful first year of our marriage...",
                EntryType = EntryType.Reflection,
                EntryDate = DateTime.UtcNow,
                IsSharedWithPartner = true,
                IsPrivate = false,
                Tags = "anniversary,milestone,celebration",
                CreatedAt = DateTime.UtcNow,
            },
            new JournalEntry
            {
                JournalEntryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Date Night Ideas",
                Content = "Collection of fun date night ideas we want to try...",
                EntryType = EntryType.General,
                EntryDate = DateTime.UtcNow.AddDays(-5),
                IsSharedWithPartner = true,
                IsPrivate = false,
                Tags = "dates,fun,planning",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new JournalEntry
            {
                JournalEntryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "Conflict Resolution",
                Content = "What I learned from our recent disagreement...",
                EntryType = EntryType.Challenge,
                EntryDate = DateTime.UtcNow.AddDays(-10),
                IsSharedWithPartner = false,
                IsPrivate = true,
                Tags = "growth,communication",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
        };

        context.JournalEntries.AddRange(journalEntries);

        var gratitudes = new List<Gratitude>
        {
            new Gratitude
            {
                GratitudeId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                JournalEntryId = journalEntries[0].JournalEntryId,
                UserId = sampleUserId,
                Text = "Grateful for your patience and understanding",
                GratitudeDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
            new Gratitude
            {
                GratitudeId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                JournalEntryId = journalEntries[0].JournalEntryId,
                UserId = sampleUserId,
                Text = "Thankful for the laughter we share every day",
                GratitudeDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
            new Gratitude
            {
                GratitudeId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Text = "Appreciative of the small gestures of love",
                GratitudeDate = DateTime.UtcNow.AddDays(-2),
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
        };

        context.Gratitudes.AddRange(gratitudes);

        var reflections = new List<Reflection>
        {
            new Reflection
            {
                ReflectionId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                JournalEntryId = journalEntries[0].JournalEntryId,
                UserId = sampleUserId,
                Text = "Our communication has improved significantly this year",
                Topic = "Communication",
                ReflectionDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
            new Reflection
            {
                ReflectionId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                JournalEntryId = journalEntries[2].JournalEntryId,
                UserId = sampleUserId,
                Text = "Learning to listen better and validate feelings",
                Topic = "Conflict Resolution",
                ReflectionDate = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
        };

        context.Reflections.AddRange(reflections);

        await context.SaveChangesAsync();
    }
}
