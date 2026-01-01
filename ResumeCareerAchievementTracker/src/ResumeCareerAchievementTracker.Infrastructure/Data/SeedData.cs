// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ResumeCareerAchievementTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ResumeCareerAchievementTracker.Core.Model.UserAggregate;
using ResumeCareerAchievementTracker.Core.Model.UserAggregate.Entities;
using ResumeCareerAchievementTracker.Core.Services;
namespace ResumeCareerAchievementTracker.Infrastructure;

/// <summary>
/// Provides seed data for the ResumeCareerAchievementTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(ResumeCareerAchievementTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Skills.AnyAsync())
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

    private static async Task SeedDataAsync(ResumeCareerAchievementTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var skills = new List<Skill>
        {
            new Skill
            {
                SkillId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "C# Programming",
                Category = "Programming",
                ProficiencyLevel = "Expert",
                YearsOfExperience = 8,
                LastUsedDate = DateTime.UtcNow.AddDays(-5),
                IsFeatured = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Skill
            {
                SkillId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "ASP.NET Core",
                Category = "Framework",
                ProficiencyLevel = "Advanced",
                YearsOfExperience = 6,
                LastUsedDate = DateTime.UtcNow.AddDays(-3),
                IsFeatured = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Skill
            {
                SkillId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Leadership",
                Category = "Soft Skills",
                ProficiencyLevel = "Advanced",
                YearsOfExperience = 5,
                LastUsedDate = DateTime.UtcNow.AddDays(-1),
                IsFeatured = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Skills.AddRange(skills);

        var projects = new List<Project>
        {
            new Project
            {
                ProjectId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "E-Commerce Platform Migration",
                Description = "Led migration of legacy e-commerce platform to modern microservices architecture",
                Organization = "TechCorp Inc.",
                Role = "Lead Developer",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 9, 30),
                Technologies = new List<string> { "C#", "ASP.NET Core", "Docker", "Kubernetes" },
                Outcomes = new List<string> { "Reduced page load time by 60%", "Improved system reliability to 99.9% uptime" },
                IsFeatured = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Project
            {
                ProjectId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Name = "Mobile App Development",
                Description = "Developed cross-platform mobile application for customer engagement",
                Organization = "StartupXYZ",
                Role = "Senior Developer",
                StartDate = new DateTime(2022, 3, 1),
                EndDate = new DateTime(2022, 12, 15),
                Technologies = new List<string> { "React Native", "TypeScript", "Firebase" },
                Outcomes = new List<string> { "Achieved 100K+ downloads in first 3 months", "4.7 star rating on app stores" },
                IsFeatured = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Projects.AddRange(projects);

        var achievements = new List<Achievement>
        {
            new Achievement
            {
                AchievementId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Architected Scalable Microservices Platform",
                Description = "Designed and implemented a microservices architecture that improved system scalability by 300%",
                AchievementType = AchievementType.TechnicalAccomplishment,
                AchievedDate = new DateTime(2023, 6, 15),
                Organization = "TechCorp Inc.",
                Impact = "Enabled company to handle 3x traffic with same infrastructure cost",
                SkillIds = new List<Guid> { skills[0].SkillId, skills[1].SkillId },
                ProjectIds = new List<Guid> { projects[0].ProjectId },
                IsFeatured = true,
                Tags = new List<string> { "Architecture", "Scalability", "Cloud" },
                CreatedAt = DateTime.UtcNow,
            },
            new Achievement
            {
                AchievementId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Led Team of 8 Developers",
                Description = "Successfully led a team of 8 developers through critical project delivery",
                AchievementType = AchievementType.Leadership,
                AchievedDate = new DateTime(2023, 9, 1),
                Organization = "TechCorp Inc.",
                Impact = "Delivered project 2 weeks ahead of schedule with zero critical bugs",
                SkillIds = new List<Guid> { skills[2].SkillId },
                ProjectIds = new List<Guid> { projects[0].ProjectId },
                IsFeatured = true,
                Tags = new List<string> { "Team Management", "Project Delivery" },
                CreatedAt = DateTime.UtcNow,
            },
            new Achievement
            {
                AchievementId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "AWS Certified Solutions Architect",
                Description = "Earned AWS Solutions Architect - Professional certification",
                AchievementType = AchievementType.Certification,
                AchievedDate = new DateTime(2023, 3, 20),
                Impact = "Enhanced cloud architecture capabilities",
                IsFeatured = false,
                Tags = new List<string> { "AWS", "Certification", "Cloud" },
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Achievements.AddRange(achievements);

        await context.SaveChangesAsync();
    }
}
