// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalProjectPipeline.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalProjectPipeline.Infrastructure;

/// <summary>
/// Provides seed data for the PersonalProjectPipeline database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PersonalProjectPipelineContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Projects.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedProjectsAsync(context);
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

    private static async Task SeedProjectsAsync(PersonalProjectPipelineContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var project1 = new Project
        {
            ProjectId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            Name = "Website Redesign",
            Description = "Complete redesign of personal portfolio website",
            Status = ProjectStatus.InProgress,
            Priority = ProjectPriority.High,
            StartDate = DateTime.UtcNow.AddDays(-30),
            TargetDate = DateTime.UtcNow.AddDays(30),
            Tags = "web,design,portfolio",
            CreatedAt = DateTime.UtcNow.AddDays(-30),
        };

        var project2 = new Project
        {
            ProjectId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = sampleUserId,
            Name = "Learn Machine Learning",
            Description = "Complete online course and build a practice project",
            Status = ProjectStatus.Planning,
            Priority = ProjectPriority.Medium,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            Tags = "learning,ml,ai",
            CreatedAt = DateTime.UtcNow.AddDays(-10),
        };

        context.Projects.Add(project1);
        context.Projects.Add(project2);

        // Add milestones for project1
        var milestone1 = new Milestone
        {
            MilestoneId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            ProjectId = project1.ProjectId,
            Name = "Design Mockups Complete",
            Description = "Finish all design mockups in Figma",
            TargetDate = DateTime.UtcNow.AddDays(-15),
            IsAchieved = true,
            AchievementDate = DateTime.UtcNow.AddDays(-16),
            CreatedAt = DateTime.UtcNow.AddDays(-30),
        };

        var milestone2 = new Milestone
        {
            MilestoneId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            ProjectId = project1.ProjectId,
            Name = "Frontend Development Complete",
            Description = "Complete all frontend development",
            TargetDate = DateTime.UtcNow.AddDays(15),
            IsAchieved = false,
            CreatedAt = DateTime.UtcNow.AddDays(-30),
        };

        context.Milestones.Add(milestone1);
        context.Milestones.Add(milestone2);

        // Add tasks for project1
        var task1 = new ProjectTask
        {
            ProjectTaskId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            ProjectId = project1.ProjectId,
            MilestoneId = milestone1.MilestoneId,
            Title = "Create wireframes",
            Description = "Design initial wireframes for all pages",
            DueDate = DateTime.UtcNow.AddDays(-20),
            IsCompleted = true,
            CompletionDate = DateTime.UtcNow.AddDays(-21),
            EstimatedHours = 8,
            CreatedAt = DateTime.UtcNow.AddDays(-30),
        };

        var task2 = new ProjectTask
        {
            ProjectTaskId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            ProjectId = project1.ProjectId,
            MilestoneId = milestone2.MilestoneId,
            Title = "Implement responsive navbar",
            Description = "Build a responsive navigation bar component",
            DueDate = DateTime.UtcNow.AddDays(5),
            IsCompleted = false,
            EstimatedHours = 6,
            CreatedAt = DateTime.UtcNow.AddDays(-10),
        };

        var task3 = new ProjectTask
        {
            ProjectTaskId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
            ProjectId = project1.ProjectId,
            MilestoneId = milestone2.MilestoneId,
            Title = "Build homepage",
            Description = "Create the homepage with hero section and features",
            DueDate = DateTime.UtcNow.AddDays(10),
            IsCompleted = false,
            EstimatedHours = 12,
            CreatedAt = DateTime.UtcNow.AddDays(-10),
        };

        context.Tasks.Add(task1);
        context.Tasks.Add(task2);
        context.Tasks.Add(task3);

        await context.SaveChangesAsync();
    }
}
