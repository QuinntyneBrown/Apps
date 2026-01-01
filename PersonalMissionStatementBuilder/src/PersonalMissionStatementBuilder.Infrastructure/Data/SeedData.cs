// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalMissionStatementBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PersonalMissionStatementBuilder.Core.Model.UserAggregate;
using PersonalMissionStatementBuilder.Core.Model.UserAggregate.Entities;
using PersonalMissionStatementBuilder.Core.Services;
namespace PersonalMissionStatementBuilder.Infrastructure;

/// <summary>
/// Provides seed data for the PersonalMissionStatementBuilder database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PersonalMissionStatementBuilderContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.MissionStatements.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedMissionStatementsAsync(context);
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

    private static async Task SeedMissionStatementsAsync(PersonalMissionStatementBuilderContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var missionStatement = new MissionStatement
        {
            MissionStatementId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            Title = "My Personal Mission Statement",
            Text = "To live a life of purpose, contribute to my community, maintain strong relationships, " +
                   "and continuously grow both personally and professionally. I strive to be authentic, " +
                   "compassionate, and make a positive impact on the world around me.",
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        context.MissionStatements.Add(missionStatement);

        // Add sample values
        var values = new List<Value>
        {
            new Value
            {
                ValueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                MissionStatementId = missionStatement.MissionStatementId,
                UserId = sampleUserId,
                Name = "Integrity",
                Description = "Always act with honesty and strong moral principles",
                Priority = 1,
                CreatedAt = DateTime.UtcNow,
            },
            new Value
            {
                ValueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                MissionStatementId = missionStatement.MissionStatementId,
                UserId = sampleUserId,
                Name = "Growth",
                Description = "Continuously learn and improve in all areas of life",
                Priority = 2,
                CreatedAt = DateTime.UtcNow,
            },
            new Value
            {
                ValueId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                MissionStatementId = missionStatement.MissionStatementId,
                UserId = sampleUserId,
                Name = "Compassion",
                Description = "Show empathy and kindness to others",
                Priority = 3,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Values.AddRange(values);

        // Add sample goals
        var goal1 = new Goal
        {
            GoalId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            MissionStatementId = missionStatement.MissionStatementId,
            UserId = sampleUserId,
            Title = "Complete Professional Certification",
            Description = "Earn a professional certification in my field to enhance my skills and career prospects",
            Status = GoalStatus.InProgress,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            CreatedAt = DateTime.UtcNow,
        };

        var goal2 = new Goal
        {
            GoalId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            MissionStatementId = missionStatement.MissionStatementId,
            UserId = sampleUserId,
            Title = "Volunteer Monthly",
            Description = "Dedicate at least 8 hours per month to community service",
            Status = GoalStatus.InProgress,
            TargetDate = DateTime.UtcNow.AddYears(1),
            CreatedAt = DateTime.UtcNow,
        };

        context.Goals.Add(goal1);
        context.Goals.Add(goal2);

        // Add sample progress entries
        var progress = new Progress
        {
            ProgressId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
            GoalId = goal1.GoalId,
            UserId = sampleUserId,
            ProgressDate = DateTime.UtcNow.AddDays(-7),
            Notes = "Completed first two modules of the certification course",
            CompletionPercentage = 25,
            CreatedAt = DateTime.UtcNow.AddDays(-7),
        };

        context.Progresses.Add(progress);

        await context.SaveChangesAsync();
    }
}
