// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KnowledgeBaseSecondBrain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Infrastructure;

/// <summary>
/// Provides seed data for the KnowledgeBaseSecondBrain database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(KnowledgeBaseSecondBrainContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Notes.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedNotesAsync(context);
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

    private static async Task SeedNotesAsync(KnowledgeBaseSecondBrainContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Create tags first
        var tags = new List<Tag>
        {
            new Tag
            {
                TagId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Programming",
                Color = "#FF6B6B",
                CreatedAt = DateTime.UtcNow,
            },
            new Tag
            {
                TagId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Personal",
                Color = "#4ECDC4",
                CreatedAt = DateTime.UtcNow,
            },
            new Tag
            {
                TagId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Ideas",
                Color = "#95E1D3",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Tags.AddRange(tags);

        var notes = new List<Note>
        {
            new Note
            {
                NoteId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Getting Started with Second Brain",
                Content = "A second brain is a personal knowledge management system...",
                NoteType = NoteType.Article,
                IsFavorite = true,
                IsArchived = false,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
            },
            new Note
            {
                NoteId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Project Ideas 2024",
                Content = "List of project ideas to explore...",
                NoteType = NoteType.List,
                IsFavorite = false,
                IsArchived = false,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
            },
            new Note
            {
                NoteId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "C# Best Practices",
                Content = "Key best practices for C# development...",
                NoteType = NoteType.Reference,
                IsFavorite = true,
                IsArchived = false,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
            },
            new Note
            {
                NoteId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Quick Thought",
                Content = "Remember to review the architecture document...",
                NoteType = NoteType.QuickNote,
                IsFavorite = false,
                IsArchived = false,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
            },
        };

        context.Notes.AddRange(notes);

        // Add links between notes
        var links = new List<NoteLink>
        {
            new NoteLink
            {
                NoteLinkId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                SourceNoteId = notes[0].NoteId,
                TargetNoteId = notes[1].NoteId,
                LinkType = "Related",
                Description = "Project management connection",
                CreatedAt = DateTime.UtcNow,
            },
            new NoteLink
            {
                NoteLinkId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                SourceNoteId = notes[2].NoteId,
                TargetNoteId = notes[0].NoteId,
                LinkType = "Reference",
                Description = "Technical reference",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Links.AddRange(links);

        // Add sample search query
        var searchQueries = new List<SearchQuery>
        {
            new SearchQuery
            {
                SearchQueryId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                QueryText = "programming best practices",
                Name = "Tech Research",
                IsSaved = true,
                ExecutionCount = 5,
                LastExecutedAt = DateTime.UtcNow.AddDays(-2),
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
        };

        context.SearchQueries.AddRange(searchQueries);

        await context.SaveChangesAsync();
    }
}
