// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TimeAuditTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TimeAuditTracker.Core.Model.UserAggregate;
using TimeAuditTracker.Core.Model.UserAggregate.Entities;
using TimeAuditTracker.Core.Services;
namespace TimeAuditTracker.Infrastructure;

/// <summary>
/// Provides seed data for the TimeAuditTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(TimeAuditTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.TimeBlocks.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedTimeBlocksAndGoalsAsync(context);
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

    private static async Task SeedTimeBlocksAndGoalsAsync(TimeAuditTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var timeBlocks = new List<TimeBlock>
        {
            new TimeBlock
            {
                TimeBlockId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Category = ActivityCategory.Work,
                Description = "Project planning and design",
                StartTime = DateTime.UtcNow.AddDays(-7).AddHours(9),
                EndTime = DateTime.UtcNow.AddDays(-7).AddHours(11),
                Notes = "Reviewed project requirements and created initial design",
                Tags = "planning, design, project-alpha",
                IsProductive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new TimeBlock
            {
                TimeBlockId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Category = ActivityCategory.Work,
                Description = "Code implementation",
                StartTime = DateTime.UtcNow.AddDays(-7).AddHours(13),
                EndTime = DateTime.UtcNow.AddDays(-7).AddHours(17),
                Notes = "Implemented core features",
                Tags = "coding, development, project-alpha",
                IsProductive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new TimeBlock
            {
                TimeBlockId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Category = ActivityCategory.Learning,
                Description = "Online course on cloud architecture",
                StartTime = DateTime.UtcNow.AddDays(-6).AddHours(19),
                EndTime = DateTime.UtcNow.AddDays(-6).AddHours(21),
                Notes = "Completed modules 3 and 4",
                Tags = "learning, cloud, aws",
                IsProductive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-6),
            },
            new TimeBlock
            {
                TimeBlockId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Category = ActivityCategory.Exercise,
                Description = "Morning run",
                StartTime = DateTime.UtcNow.AddDays(-5).AddHours(6),
                EndTime = DateTime.UtcNow.AddDays(-5).AddHours(7),
                Notes = "5km run in the park",
                Tags = "running, cardio, health",
                IsProductive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new TimeBlock
            {
                TimeBlockId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Category = ActivityCategory.Entertainment,
                Description = "Watching TV series",
                StartTime = DateTime.UtcNow.AddDays(-5).AddHours(20),
                EndTime = DateTime.UtcNow.AddDays(-5).AddHours(22),
                Notes = "Relaxing evening",
                Tags = "tv, relaxation",
                IsProductive = false,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new TimeBlock
            {
                TimeBlockId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                UserId = sampleUserId,
                Category = ActivityCategory.Work,
                Description = "Team meetings",
                StartTime = DateTime.UtcNow.AddDays(-4).AddHours(10),
                EndTime = DateTime.UtcNow.AddDays(-4).AddHours(12),
                Notes = "Sprint planning and daily standups",
                Tags = "meetings, team, agile",
                IsProductive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-4),
            },
        };

        context.TimeBlocks.AddRange(timeBlocks);

        var goals = new List<Goal>
        {
            new Goal
            {
                GoalId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Category = ActivityCategory.Work,
                TargetHoursPerWeek = 40.0,
                MinimumHoursPerWeek = 35.0,
                Description = "Maintain healthy work hours",
                IsActive = true,
                StartDate = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new Goal
            {
                GoalId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Category = ActivityCategory.Learning,
                TargetHoursPerWeek = 5.0,
                MinimumHoursPerWeek = 3.0,
                Description = "Continuous learning and skill development",
                IsActive = true,
                StartDate = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new Goal
            {
                GoalId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Category = ActivityCategory.Exercise,
                TargetHoursPerWeek = 4.0,
                MinimumHoursPerWeek = 3.0,
                Description = "Regular exercise routine",
                IsActive = true,
                StartDate = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new Goal
            {
                GoalId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Category = ActivityCategory.SocialMedia,
                TargetHoursPerWeek = 2.0,
                MinimumHoursPerWeek = null,
                Description = "Limit social media time",
                IsActive = true,
                StartDate = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
        };

        context.Goals.AddRange(goals);

        var auditReports = new List<AuditReport>
        {
            new AuditReport
            {
                AuditReportId = Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111"),
                UserId = sampleUserId,
                Title = "Weekly Time Audit - Week 1",
                StartDate = DateTime.UtcNow.AddDays(-14),
                EndDate = DateTime.UtcNow.AddDays(-7),
                TotalTrackedHours = 56.0,
                ProductiveHours = 48.0,
                Summary = "Overall productive week with good work-life balance",
                Insights = "Work hours were within target. Learning time exceeded goal. Need more exercise time.",
                Recommendations = "Increase exercise time to meet weekly goal. Continue current work pattern.",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new AuditReport
            {
                AuditReportId = Guid.Parse("bbbbbbbb-1111-1111-1111-111111111111"),
                UserId = sampleUserId,
                Title = "Weekly Time Audit - Week 2",
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow,
                TotalTrackedHours = 52.0,
                ProductiveHours = 44.0,
                Summary = "Balanced week with focus on professional development",
                Insights = "Achieved learning goal. Work hours slightly below target. Good exercise routine maintained.",
                Recommendations = "Focus on completing work tasks more efficiently to reach target hours.",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.AuditReports.AddRange(auditReports);

        await context.SaveChangesAsync();
    }
}
