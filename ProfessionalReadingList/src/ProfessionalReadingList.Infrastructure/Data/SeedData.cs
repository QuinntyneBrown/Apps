// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfessionalReadingList.Core;

using ProfessionalReadingList.Core.Model.UserAggregate;
using ProfessionalReadingList.Core.Model.UserAggregate.Entities;
using ProfessionalReadingList.Core.Services;
namespace ProfessionalReadingList.Infrastructure;

/// <summary>
/// Provides seed data for the ProfessionalReadingList database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(ProfessionalReadingListContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Resources.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedReadingDataAsync(context);
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

    private static async Task SeedReadingDataAsync(ProfessionalReadingListContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Seed Resources
        var resources = new List<Resource>
        {
            new Resource
            {
                ResourceId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                ResourceType = ResourceType.Book,
                Author = "Robert C. Martin",
                Publisher = "Prentice Hall",
                PublicationDate = new DateTime(2008, 8, 1),
                Isbn = "978-0132350884",
                TotalPages = 464,
                Topics = new List<string> { "software-engineering", "best-practices", "clean-code" },
                DateAdded = DateTime.UtcNow.AddDays(-90),
                Notes = "Essential reading for any software developer",
                CreatedAt = DateTime.UtcNow,
            },
            new Resource
            {
                ResourceId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Designing Data-Intensive Applications",
                ResourceType = ResourceType.Book,
                Author = "Martin Kleppmann",
                Publisher = "O'Reilly Media",
                PublicationDate = new DateTime(2017, 3, 16),
                Isbn = "978-1449373320",
                TotalPages = 616,
                Topics = new List<string> { "distributed-systems", "databases", "architecture" },
                DateAdded = DateTime.UtcNow.AddDays(-60),
                Notes = "Deep dive into modern data systems",
                CreatedAt = DateTime.UtcNow,
            },
            new Resource
            {
                ResourceId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "The Pragmatic Programmer: Your Journey to Mastery",
                ResourceType = ResourceType.Book,
                Author = "David Thomas, Andrew Hunt",
                Publisher = "Addison-Wesley",
                PublicationDate = new DateTime(2019, 9, 13),
                Isbn = "978-0135957059",
                TotalPages = 352,
                Topics = new List<string> { "programming", "career-development", "best-practices" },
                DateAdded = DateTime.UtcNow.AddDays(-30),
                Notes = "20th Anniversary Edition with updated content",
                CreatedAt = DateTime.UtcNow,
            },
            new Resource
            {
                ResourceId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Title = "Understanding Distributed Systems",
                ResourceType = ResourceType.Article,
                Author = "Roberto Vitillo",
                Url = "https://understandingdistributed.systems",
                Topics = new List<string> { "distributed-systems", "scalability" },
                DateAdded = DateTime.UtcNow.AddDays(-15),
                Notes = "Comprehensive online guide",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Resources.AddRange(resources);

        // Seed Reading Progress
        var progressRecords = new List<ReadingProgress>
        {
            new ReadingProgress
            {
                ReadingProgressId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ResourceId = resources[0].ResourceId,
                Status = "Completed",
                CurrentPage = 464,
                ProgressPercentage = 100,
                StartDate = DateTime.UtcNow.AddDays(-80),
                CompletionDate = DateTime.UtcNow.AddDays(-50),
                Rating = 5,
                Review = "Excellent book! Changed how I write code. The principles are timeless and applicable to any programming language.",
                CreatedAt = DateTime.UtcNow,
            },
            new ReadingProgress
            {
                ReadingProgressId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                ResourceId = resources[1].ResourceId,
                Status = "Reading",
                CurrentPage = 250,
                ProgressPercentage = 40,
                StartDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
            },
            new ReadingProgress
            {
                ReadingProgressId = Guid.Parse("33333333-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                ResourceId = resources[2].ResourceId,
                Status = "Not Started",
                ProgressPercentage = 0,
                CreatedAt = DateTime.UtcNow,
            },
            new ReadingProgress
            {
                ReadingProgressId = Guid.Parse("44444444-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                ResourceId = resources[3].ResourceId,
                Status = "Reading",
                ProgressPercentage = 65,
                StartDate = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.ReadingProgress.AddRange(progressRecords);

        // Seed Notes
        var notes = new List<Note>
        {
            new Note
            {
                NoteId = Guid.Parse("55555555-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                ResourceId = resources[0].ResourceId,
                Content = "The Boy Scout Rule: Leave the code cleaner than you found it.",
                PageReference = "Chapter 1",
                Quote = "Indeed, the ratio of time spent reading versus writing is well over 10 to 1.",
                Tags = new List<string> { "best-practice", "key-concept" },
                CreatedAt = DateTime.UtcNow,
            },
            new Note
            {
                NoteId = Guid.Parse("66666666-ffff-ffff-ffff-ffffffffffff"),
                UserId = sampleUserId,
                ResourceId = resources[0].ResourceId,
                Content = "Functions should do one thing. They should do it well. They should do it only.",
                PageReference = "Chapter 3",
                Quote = "The first rule of functions is that they should be small. The second rule of functions is that they should be smaller than that.",
                Tags = new List<string> { "functions", "single-responsibility" },
                CreatedAt = DateTime.UtcNow,
            },
            new Note
            {
                NoteId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ResourceId = resources[1].ResourceId,
                Content = "Replication vs Partitioning - both solve scalability but in different ways",
                PageReference = "Chapter 5",
                Tags = new List<string> { "scalability", "architecture" },
                CreatedAt = DateTime.UtcNow,
            },
            new Note
            {
                NoteId = Guid.Parse("88888888-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                ResourceId = resources[1].ResourceId,
                Content = "CAP theorem implications for distributed databases",
                PageReference = "Chapter 9",
                Quote = "In the presence of a network partition, you have to choose between consistency and availability.",
                Tags = new List<string> { "cap-theorem", "distributed-systems" },
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Notes.AddRange(notes);

        await context.SaveChangesAsync();
    }
}
