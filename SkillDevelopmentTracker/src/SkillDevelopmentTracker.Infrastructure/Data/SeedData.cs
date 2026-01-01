// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SkillDevelopmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SkillDevelopmentTracker.Core.Model.UserAggregate;
using SkillDevelopmentTracker.Core.Model.UserAggregate.Entities;
using SkillDevelopmentTracker.Core.Services;
namespace SkillDevelopmentTracker.Infrastructure;

/// <summary>
/// Provides seed data for the SkillDevelopmentTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(SkillDevelopmentTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
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
                await SeedSkillsAsync(context);
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

    private static async Task SeedSkillsAsync(SkillDevelopmentTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var skills = new List<Skill>
        {
            new Skill
            {
                SkillId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "React",
                Category = "Programming",
                ProficiencyLevel = ProficiencyLevel.Intermediate,
                TargetLevel = ProficiencyLevel.Advanced,
                StartDate = new DateTime(2023, 1, 1),
                TargetDate = new DateTime(2024, 12, 31),
                HoursSpent = 120m,
                Notes = "Building modern web applications",
                CreatedAt = DateTime.UtcNow,
            },
            new Skill
            {
                SkillId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Python",
                Category = "Programming",
                ProficiencyLevel = ProficiencyLevel.Advanced,
                TargetLevel = ProficiencyLevel.Expert,
                StartDate = new DateTime(2022, 6, 1),
                HoursSpent = 250m,
                Notes = "Data science and backend development",
                CreatedAt = DateTime.UtcNow,
            },
            new Skill
            {
                SkillId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "UI/UX Design",
                Category = "Design",
                ProficiencyLevel = ProficiencyLevel.Novice,
                TargetLevel = ProficiencyLevel.Intermediate,
                StartDate = new DateTime(2024, 1, 15),
                TargetDate = new DateTime(2024, 6, 30),
                HoursSpent = 30m,
                Notes = "Learning Figma and design principles",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Skills.AddRange(skills);

        // Add sample courses
        var courses = new List<Course>
        {
            new Course
            {
                CourseId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Advanced React Patterns",
                Provider = "Frontend Masters",
                Instructor = "Kent C. Dodds",
                CourseUrl = "https://frontendmasters.com/courses/advanced-react-patterns/",
                StartDate = new DateTime(2024, 1, 10),
                CompletionDate = new DateTime(2024, 2, 15),
                ProgressPercentage = 100,
                EstimatedHours = 8m,
                ActualHours = 10m,
                IsCompleted = true,
                SkillIds = new List<Guid> { skills[0].SkillId },
                CreatedAt = DateTime.UtcNow,
            },
            new Course
            {
                CourseId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Machine Learning with Python",
                Provider = "Coursera",
                Instructor = "Andrew Ng",
                CourseUrl = "https://www.coursera.org/learn/machine-learning",
                StartDate = new DateTime(2024, 2, 1),
                ProgressPercentage = 65,
                EstimatedHours = 55m,
                ActualHours = 40m,
                IsCompleted = false,
                SkillIds = new List<Guid> { skills[1].SkillId },
                Notes = "Working on week 7 assignments",
                CreatedAt = DateTime.UtcNow,
            },
            new Course
            {
                CourseId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "UI Design Fundamentals",
                Provider = "Udemy",
                Instructor = "Gary Simon",
                StartDate = new DateTime(2024, 1, 20),
                ProgressPercentage = 45,
                EstimatedHours = 12m,
                ActualHours = 6m,
                IsCompleted = false,
                SkillIds = new List<Guid> { skills[2].SkillId },
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Courses.AddRange(courses);

        // Add sample certifications
        var certifications = new List<Certification>
        {
            new Certification
            {
                CertificationId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "AWS Certified Developer - Associate",
                IssuingOrganization = "Amazon Web Services",
                IssueDate = new DateTime(2023, 6, 15),
                ExpirationDate = new DateTime(2026, 6, 15),
                CredentialId = "AWS-12345-ABCDE",
                CredentialUrl = "https://aws.amazon.com/verification",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Certification
            {
                CertificationId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Professional Scrum Master I",
                IssuingOrganization = "Scrum.org",
                IssueDate = new DateTime(2023, 3, 10),
                CredentialId = "PSM-67890",
                IsActive = true,
                Notes = "Lifetime certification, no expiration",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Certifications.AddRange(certifications);

        // Add sample learning paths
        var learningPaths = new List<LearningPath>
        {
            new LearningPath
            {
                LearningPathId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Full Stack Developer Journey",
                Description = "Become proficient in modern web development with React and Node.js",
                StartDate = new DateTime(2024, 1, 1),
                TargetDate = new DateTime(2024, 12, 31),
                CourseIds = new List<Guid> { courses[0].CourseId },
                SkillIds = new List<Guid> { skills[0].SkillId },
                ProgressPercentage = 30,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
            },
            new LearningPath
            {
                LearningPathId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Data Science Specialization",
                Description = "Master data science and machine learning with Python",
                StartDate = new DateTime(2024, 2, 1),
                TargetDate = new DateTime(2025, 2, 1),
                CourseIds = new List<Guid> { courses[1].CourseId },
                SkillIds = new List<Guid> { skills[1].SkillId },
                ProgressPercentage = 20,
                IsCompleted = false,
                Notes = "Focus on practical projects and portfolio building",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.LearningPaths.AddRange(learningPaths);

        await context.SaveChangesAsync();
    }
}
