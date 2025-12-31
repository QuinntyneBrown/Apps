// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BucketListManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BucketListManager.Core.Model.UserAggregate;
using BucketListManager.Core.Model.UserAggregate.Entities;
using BucketListManager.Core.Services;
namespace BucketListManager.Infrastructure;

/// <summary>
/// Provides seed data for the BucketListManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(BucketListManagerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.BucketListItems.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedBucketListItemsAsync(context);
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

    private static async Task SeedBucketListItemsAsync(BucketListManagerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var bucketListItems = new List<BucketListItem>
        {
            new BucketListItem
            {
                BucketListItemId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Learn to play guitar",
                Description = "Take guitar lessons and learn to play at least 5 songs",
                Category = Category.Learning,
                Priority = Priority.High,
                Status = ItemStatus.InProgress,
                TargetDate = DateTime.UtcNow.AddMonths(6),
                Notes = "Already bought a guitar!",
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
            },
            new BucketListItem
            {
                BucketListItemId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Visit Japan",
                Description = "Experience the culture, food, and visit Tokyo, Kyoto, and Osaka",
                Category = Category.Travel,
                Priority = Priority.High,
                Status = ItemStatus.NotStarted,
                TargetDate = DateTime.UtcNow.AddYears(1),
                Notes = "Saving money for this trip",
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new BucketListItem
            {
                BucketListItemId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "Run a marathon",
                Description = "Complete a full 26.2-mile marathon",
                Category = Category.Fitness,
                Priority = Priority.Medium,
                Status = ItemStatus.Completed,
                TargetDate = DateTime.UtcNow.AddMonths(-1),
                CompletedDate = DateTime.UtcNow.AddDays(-10),
                Notes = "Completed the City Marathon!",
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
            },
        };

        context.BucketListItems.AddRange(bucketListItems);

        // Add sample milestones
        var milestones = new List<Milestone>
        {
            new Milestone
            {
                MilestoneId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BucketListItemId = bucketListItems[0].BucketListItemId,
                Title = "Learn basic chords",
                Description = "Master C, D, E, G, and A chords",
                IsCompleted = true,
                CompletedDate = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
            },
            new Milestone
            {
                MilestoneId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BucketListItemId = bucketListItems[0].BucketListItemId,
                Title = "Learn first song",
                Description = "Play 'Wonderwall' by Oasis",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
        };

        context.Milestones.AddRange(milestones);

        // Add sample memories
        var memories = new List<Memory>
        {
            new Memory
            {
                MemoryId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BucketListItemId = bucketListItems[2].BucketListItemId,
                Title = "Marathon Finish Line",
                Description = "Crossed the finish line in 4 hours and 15 minutes!",
                MemoryDate = DateTime.UtcNow.AddDays(-10),
                PhotoUrl = "https://example.com/photos/marathon-finish.jpg",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
        };

        context.Memories.AddRange(memories);

        await context.SaveChangesAsync();
    }
}
