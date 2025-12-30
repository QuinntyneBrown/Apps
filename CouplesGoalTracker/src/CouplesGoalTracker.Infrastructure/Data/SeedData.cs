// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CouplesGoalTracker.Infrastructure;

/// <summary>
/// Provides seed data for the CouplesGoalTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(CouplesGoalTrackerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Goals.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedGoalsAsync(context);
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

    private static async Task SeedGoalsAsync(CouplesGoalTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var goals = new List<Goal>
        {
            new Goal
            {
                GoalId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Plan a Weekend Getaway",
                Description = "Research and plan a romantic weekend trip to the mountains",
                Category = GoalCategory.AdventureAndTravel,
                Status = GoalStatus.InProgress,
                TargetDate = DateTime.UtcNow.AddMonths(2),
                Priority = 4,
                IsShared = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Goal
            {
                GoalId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Save for Home Down Payment",
                Description = "Save $50,000 for a down payment on our first home",
                Category = GoalCategory.Financial,
                Status = GoalStatus.InProgress,
                TargetDate = DateTime.UtcNow.AddYears(2),
                Priority = 5,
                IsShared = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Goal
            {
                GoalId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "Weekly Date Night",
                Description = "Dedicate every Friday evening as our date night",
                Category = GoalCategory.QualityTime,
                Status = GoalStatus.InProgress,
                Priority = 5,
                IsShared = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Goal
            {
                GoalId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Title = "Learn Couples Dance",
                Description = "Take salsa dancing classes together",
                Category = GoalCategory.PersonalGrowth,
                Status = GoalStatus.NotStarted,
                TargetDate = DateTime.UtcNow.AddMonths(6),
                Priority = 3,
                IsShared = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Goal
            {
                GoalId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Title = "Practice Active Listening",
                Description = "Improve our communication by practicing active listening techniques",
                Category = GoalCategory.Communication,
                Status = GoalStatus.InProgress,
                Priority = 5,
                IsShared = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Goals.AddRange(goals);

        // Add sample milestones for the first goal
        var milestones = new List<Milestone>
        {
            new Milestone
            {
                MilestoneId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[0].GoalId,
                UserId = sampleUserId,
                Title = "Research destinations",
                Description = "Look up mountain resorts within 3 hours drive",
                TargetDate = DateTime.UtcNow.AddWeeks(2),
                IsCompleted = true,
                CompletedDate = DateTime.UtcNow.AddDays(-5),
                SortOrder = 1,
                CreatedAt = DateTime.UtcNow,
            },
            new Milestone
            {
                MilestoneId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[0].GoalId,
                UserId = sampleUserId,
                Title = "Book accommodation",
                Description = "Reserve cabin or hotel room",
                TargetDate = DateTime.UtcNow.AddWeeks(4),
                IsCompleted = false,
                SortOrder = 2,
                CreatedAt = DateTime.UtcNow,
            },
            new Milestone
            {
                MilestoneId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[0].GoalId,
                UserId = sampleUserId,
                Title = "Plan activities",
                Description = "Create itinerary for hiking, dining, and relaxation",
                TargetDate = DateTime.UtcNow.AddWeeks(6),
                IsCompleted = false,
                SortOrder = 3,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Milestones.AddRange(milestones);

        // Add sample progress for the second goal (financial)
        var progresses = new List<Progress>
        {
            new Progress
            {
                ProgressId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[1].GoalId,
                UserId = sampleUserId,
                ProgressDate = DateTime.UtcNow.AddMonths(-3),
                Notes = "Started savings account with initial deposit",
                CompletionPercentage = 5,
                IsSignificant = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Progress
            {
                ProgressId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[1].GoalId,
                UserId = sampleUserId,
                ProgressDate = DateTime.UtcNow.AddMonths(-1),
                Notes = "Added tax refund to savings",
                CompletionPercentage = 12,
                IsSignificant = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Progress
            {
                ProgressId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[1].GoalId,
                UserId = sampleUserId,
                ProgressDate = DateTime.UtcNow,
                Notes = "Monthly savings contribution",
                CompletionPercentage = 15,
                IsSignificant = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Progresses.AddRange(progresses);

        await context.SaveChangesAsync();
    }
}
