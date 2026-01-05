// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using InjuryPreventionRecoveryTracker.Core.Models.UserAggregate;
using InjuryPreventionRecoveryTracker.Core.Models.UserAggregate.Entities;
using InjuryPreventionRecoveryTracker.Core.Services;
namespace InjuryPreventionRecoveryTracker.Infrastructure;

/// <summary>
/// Provides seed data for the InjuryPreventionRecoveryTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(InjuryPreventionRecoveryTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Injuries.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedInjuryDataAsync(context);
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

    private static async Task SeedInjuryDataAsync(InjuryPreventionRecoveryTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Seed Injuries
        var injuries = new List<Injury>
        {
            new Injury
            {
                InjuryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryType = InjuryType.Sprain,
                Severity = InjurySeverity.Moderate,
                BodyPart = "Right Ankle",
                InjuryDate = DateTime.UtcNow.AddDays(-14),
                Description = "Twisted ankle during basketball game",
                Diagnosis = "Grade 2 ankle sprain",
                ExpectedRecoveryDays = 21,
                Status = "Recovering",
                ProgressPercentage = 40,
                Notes = "Ice and rest recommended, started physical therapy",
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
            new Injury
            {
                InjuryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                InjuryType = InjuryType.Strain,
                Severity = InjurySeverity.Mild,
                BodyPart = "Lower Back",
                InjuryDate = DateTime.UtcNow.AddDays(-7),
                Description = "Improper lifting technique at gym",
                Diagnosis = "Lumbar muscle strain",
                ExpectedRecoveryDays = 14,
                Status = "Recovering",
                ProgressPercentage = 60,
                Notes = "Avoid heavy lifting, gentle stretching recommended",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
        };

        context.Injuries.AddRange(injuries);

        // Seed Recovery Exercises
        var exercises = new List<RecoveryExercise>
        {
            new RecoveryExercise
            {
                RecoveryExerciseId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryId = injuries[0].InjuryId,
                Name = "Ankle Circles",
                Description = "Rotate ankle slowly in both directions",
                Frequency = "3 times daily",
                SetsAndReps = "3 sets of 10 rotations each direction",
                DurationMinutes = 5,
                Instructions = "Perform while seated, keep movements slow and controlled",
                LastCompleted = DateTime.UtcNow.AddHours(-8),
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
            new RecoveryExercise
            {
                RecoveryExerciseId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryId = injuries[0].InjuryId,
                Name = "Calf Raises",
                Description = "Stand on toes, hold for 3 seconds, lower slowly",
                Frequency = "2 times daily",
                SetsAndReps = "2 sets of 15 repetitions",
                DurationMinutes = 10,
                Instructions = "Use wall for support if needed, progress to single leg when stronger",
                LastCompleted = DateTime.UtcNow.AddDays(-1),
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new RecoveryExercise
            {
                RecoveryExerciseId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryId = injuries[1].InjuryId,
                Name = "Cat-Cow Stretch",
                Description = "Alternate between arching and rounding spine on hands and knees",
                Frequency = "2 times daily",
                SetsAndReps = "3 sets of 10 repetitions",
                DurationMinutes = 8,
                Instructions = "Move slowly, breathe deeply with each movement",
                LastCompleted = DateTime.UtcNow.AddHours(-4),
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new RecoveryExercise
            {
                RecoveryExerciseId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryId = injuries[1].InjuryId,
                Name = "Pelvic Tilts",
                Description = "Lying on back, tilt pelvis to flatten lower back",
                Frequency = "3 times daily",
                SetsAndReps = "2 sets of 12 repetitions",
                DurationMinutes = 6,
                Instructions = "Keep movements small and controlled, engage core muscles",
                LastCompleted = DateTime.UtcNow.AddHours(-2),
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
        };

        context.RecoveryExercises.AddRange(exercises);

        // Seed Milestones
        var milestones = new List<Milestone>
        {
            new Milestone
            {
                MilestoneId = Guid.Parse("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryId = injuries[0].InjuryId,
                Name = "Walk without limping",
                Description = "Able to walk normally without pain or limping",
                TargetDate = DateTime.UtcNow.AddDays(7),
                AchievedDate = null,
                IsAchieved = false,
                Notes = "Progress slowly, don't push too hard",
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
            new Milestone
            {
                MilestoneId = Guid.Parse("bbbb1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryId = injuries[0].InjuryId,
                Name = "Return to light jogging",
                Description = "Able to jog lightly for 10 minutes without pain",
                TargetDate = DateTime.UtcNow.AddDays(14),
                AchievedDate = null,
                IsAchieved = false,
                Notes = "Start on soft surface",
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
            new Milestone
            {
                MilestoneId = Guid.Parse("cccc1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryId = injuries[1].InjuryId,
                Name = "Pain-free daily activities",
                Description = "Able to perform all daily tasks without back pain",
                TargetDate = DateTime.UtcNow.AddDays(3),
                AchievedDate = DateTime.UtcNow.AddDays(-1),
                IsAchieved = true,
                Notes = "Achieved ahead of schedule!",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new Milestone
            {
                MilestoneId = Guid.Parse("dddd1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                InjuryId = injuries[1].InjuryId,
                Name = "Return to gym workouts",
                Description = "Able to resume regular gym routine with proper form",
                TargetDate = DateTime.UtcNow.AddDays(7),
                AchievedDate = null,
                IsAchieved = false,
                Notes = "Focus on form, use lighter weights initially",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
        };

        context.Milestones.AddRange(milestones);

        await context.SaveChangesAsync();
    }
}
