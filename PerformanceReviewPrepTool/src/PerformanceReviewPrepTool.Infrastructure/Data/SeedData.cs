// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PerformanceReviewPrepTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PerformanceReviewPrepTool.Core.Model.UserAggregate;
using PerformanceReviewPrepTool.Core.Model.UserAggregate.Entities;
using PerformanceReviewPrepTool.Core.Services;
namespace PerformanceReviewPrepTool.Infrastructure;

/// <summary>
/// Provides seed data for the PerformanceReviewPrepTool database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PerformanceReviewPrepToolContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.ReviewPeriods.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedReviewPeriodsAsync(context);
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

    private static async Task SeedReviewPeriodsAsync(PerformanceReviewPrepToolContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var reviewPeriods = new List<ReviewPeriod>
        {
            new ReviewPeriod
            {
                ReviewPeriodId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Q4 2024 Performance Review",
                StartDate = new DateTime(2024, 10, 1),
                EndDate = new DateTime(2024, 12, 31),
                ReviewDueDate = new DateTime(2025, 1, 15),
                ReviewerName = "Jane Smith",
                IsCompleted = false,
                Notes = "Focus on technical leadership and project delivery",
                CreatedAt = DateTime.UtcNow,
            },
            new ReviewPeriod
            {
                ReviewPeriodId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Q3 2024 Performance Review",
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 9, 30),
                ReviewDueDate = new DateTime(2024, 10, 15),
                ReviewerName = "Jane Smith",
                IsCompleted = true,
                CompletedDate = new DateTime(2024, 10, 10),
                Notes = "Successfully completed all objectives",
                CreatedAt = DateTime.UtcNow.AddMonths(-3),
            },
            new ReviewPeriod
            {
                ReviewPeriodId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "Q2 2024 Performance Review",
                StartDate = new DateTime(2024, 4, 1),
                EndDate = new DateTime(2024, 6, 30),
                ReviewDueDate = new DateTime(2024, 7, 15),
                ReviewerName = "Jane Smith",
                IsCompleted = true,
                CompletedDate = new DateTime(2024, 7, 12),
                Notes = "Strong performance in cross-team collaboration",
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
            },
        };

        context.ReviewPeriods.AddRange(reviewPeriods);

        // Add achievements for the current review period
        var achievements = new List<Achievement>
        {
            new Achievement
            {
                AchievementId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReviewPeriodId = reviewPeriods[0].ReviewPeriodId,
                Title = "Led Migration to Microservices",
                Description = "Successfully led the team in migrating the monolithic application to microservices architecture",
                AchievedDate = new DateTime(2024, 11, 15),
                Impact = "Improved system scalability by 300% and reduced deployment time from 2 hours to 15 minutes",
                Category = "Technical Leadership",
                IsKeyAchievement = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Achievement
            {
                AchievementId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReviewPeriodId = reviewPeriods[0].ReviewPeriodId,
                Title = "Mentored Junior Developers",
                Description = "Mentored 3 junior developers through pair programming and code reviews",
                AchievedDate = new DateTime(2024, 10, 30),
                Impact = "All mentees received positive feedback and one was promoted",
                Category = "Leadership",
                IsKeyAchievement = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Achievements.AddRange(achievements);

        // Add goals for the current review period
        var goals = new List<Goal>
        {
            new Goal
            {
                GoalId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReviewPeriodId = reviewPeriods[0].ReviewPeriodId,
                Title = "Complete AWS Certification",
                Description = "Obtain AWS Solutions Architect Professional certification",
                Status = GoalStatus.InProgress,
                TargetDate = new DateTime(2024, 12, 15),
                ProgressPercentage = 75,
                SuccessMetrics = "Pass the certification exam with score above 750",
                Notes = "Currently preparing with practice exams",
                CreatedAt = DateTime.UtcNow,
            },
            new Goal
            {
                GoalId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReviewPeriodId = reviewPeriods[0].ReviewPeriodId,
                Title = "Improve Code Review Response Time",
                Description = "Reduce average code review response time to under 4 hours",
                Status = GoalStatus.Completed,
                TargetDate = new DateTime(2024, 11, 30),
                CompletedDate = new DateTime(2024, 11, 20),
                ProgressPercentage = 100,
                SuccessMetrics = "Average response time under 4 hours for 90% of reviews",
                Notes = "Achieved 3.2 hour average response time",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Goals.AddRange(goals);

        // Add feedback for the current review period
        var feedbacks = new List<Feedback>
        {
            new Feedback
            {
                FeedbackId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReviewPeriodId = reviewPeriods[0].ReviewPeriodId,
                Source = "Jane Smith (Manager)",
                Content = "Excellent leadership during the microservices migration. The technical approach was sound and well-communicated to stakeholders.",
                ReceivedDate = new DateTime(2024, 11, 20),
                FeedbackType = "Positive",
                Category = "Technical Leadership",
                IsKeyFeedback = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Feedback
            {
                FeedbackId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ReviewPeriodId = reviewPeriods[0].ReviewPeriodId,
                Source = "John Doe (Peer)",
                Content = "Great collaboration on the API design. Would benefit from more documentation on architectural decisions.",
                ReceivedDate = new DateTime(2024, 10, 25),
                FeedbackType = "Constructive",
                Category = "Collaboration",
                IsKeyFeedback = false,
                Notes = "Action: Create ADR (Architecture Decision Records) for major design decisions",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Feedbacks.AddRange(feedbacks);

        await context.SaveChangesAsync();
    }
}
