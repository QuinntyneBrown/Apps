// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeeklyReviewSystem.Core;

using WeeklyReviewSystem.Core.Model.UserAggregate;
using WeeklyReviewSystem.Core.Model.UserAggregate.Entities;
using WeeklyReviewSystem.Core.Services;
namespace WeeklyReviewSystem.Infrastructure;

/// <summary>
/// Provides seed data for the WeeklyReviewSystem database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(WeeklyReviewSystemContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Reviews.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedDataAsync(context);
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

    private static async Task SeedDataAsync(WeeklyReviewSystemContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var reviews = new List<WeeklyReview>
        {
            new WeeklyReview
            {
                WeeklyReviewId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                WeekStartDate = DateTime.UtcNow.AddDays(-7),
                WeekEndDate = DateTime.UtcNow.AddDays(-1),
                OverallRating = 8,
                Reflections = "A productive week with good progress on major projects.",
                LessonsLearned = "Learned the importance of taking breaks for better focus.",
                Gratitude = "Grateful for supportive team members and good health.",
                ImprovementAreas = "Need to improve time management for meetings.",
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow,
            },
            new WeeklyReview
            {
                WeeklyReviewId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                WeekStartDate = DateTime.UtcNow,
                WeekEndDate = DateTime.UtcNow.AddDays(6),
                OverallRating = null,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Reviews.AddRange(reviews);

        var accomplishments = new List<Accomplishment>
        {
            new Accomplishment
            {
                AccomplishmentId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                WeeklyReviewId = reviews[0].WeeklyReviewId,
                Title = "Completed Project Milestone",
                Description = "Successfully delivered the Q4 project milestone ahead of schedule.",
                Category = "Work",
                ImpactLevel = 9,
                CreatedAt = DateTime.UtcNow,
            },
            new Accomplishment
            {
                AccomplishmentId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                WeeklyReviewId = reviews[0].WeeklyReviewId,
                Title = "Ran 5K Personal Best",
                Description = "Achieved a new personal best time of 24 minutes.",
                Category = "Health",
                ImpactLevel = 7,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Accomplishments.AddRange(accomplishments);

        var challenges = new List<Challenge>
        {
            new Challenge
            {
                ChallengeId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                WeeklyReviewId = reviews[0].WeeklyReviewId,
                Title = "Technical Blocker",
                Description = "Encountered a difficult bug in the payment system.",
                Resolution = "Collaborated with the team and found the root cause.",
                IsResolved = true,
                LessonsLearned = "Always check edge cases in financial calculations.",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Challenges.AddRange(challenges);

        var priorities = new List<WeeklyPriority>
        {
            new WeeklyPriority
            {
                WeeklyPriorityId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                WeeklyReviewId = reviews[1].WeeklyReviewId,
                Title = "Complete Code Review",
                Description = "Review and merge all pending pull requests.",
                Level = PriorityLevel.High,
                Category = "Work",
                TargetDate = DateTime.UtcNow.AddDays(3),
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
            },
            new WeeklyPriority
            {
                WeeklyPriorityId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                WeeklyReviewId = reviews[1].WeeklyReviewId,
                Title = "Exercise 4 times",
                Description = "Maintain fitness routine.",
                Level = PriorityLevel.Medium,
                Category = "Health",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Priorities.AddRange(priorities);

        await context.SaveChangesAsync();
    }
}
