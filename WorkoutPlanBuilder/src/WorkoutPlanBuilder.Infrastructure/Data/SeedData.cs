// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WorkoutPlanBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WorkoutPlanBuilder.Infrastructure;

/// <summary>
/// Provides seed data for the WorkoutPlanBuilder database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(WorkoutPlanBuilderContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Exercises.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedExercisesAndWorkoutsAsync(context);
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

    private static async Task SeedExercisesAndWorkoutsAsync(WorkoutPlanBuilderContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var exercises = new List<Exercise>
        {
            new Exercise
            {
                ExerciseId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Barbell Squat",
                Description = "Compound lower body exercise for building leg strength",
                ExerciseType = ExerciseType.Strength,
                PrimaryMuscleGroup = MuscleGroup.Legs,
                SecondaryMuscleGroups = "Glutes, Core",
                Equipment = "Barbell, Squat Rack",
                DifficultyLevel = 3,
                VideoUrl = "https://example.com/squat",
                IsCustom = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Exercise
            {
                ExerciseId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Bench Press",
                Description = "Classic upper body pressing movement",
                ExerciseType = ExerciseType.Strength,
                PrimaryMuscleGroup = MuscleGroup.Chest,
                SecondaryMuscleGroups = "Shoulders, Triceps",
                Equipment = "Barbell, Bench",
                DifficultyLevel = 2,
                VideoUrl = "https://example.com/bench",
                IsCustom = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Exercise
            {
                ExerciseId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Pull-ups",
                Description = "Bodyweight back exercise",
                ExerciseType = ExerciseType.Strength,
                PrimaryMuscleGroup = MuscleGroup.Back,
                SecondaryMuscleGroups = "Biceps, Core",
                Equipment = "Pull-up Bar",
                DifficultyLevel = 4,
                VideoUrl = "https://example.com/pullups",
                IsCustom = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Exercise
            {
                ExerciseId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "Running",
                Description = "Cardiovascular endurance exercise",
                ExerciseType = ExerciseType.Cardio,
                PrimaryMuscleGroup = MuscleGroup.Legs,
                SecondaryMuscleGroups = "Cardiovascular System",
                Equipment = "None",
                DifficultyLevel = 2,
                IsCustom = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Exercise
            {
                ExerciseId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Name = "Yoga Flow",
                Description = "Flexibility and balance routine",
                ExerciseType = ExerciseType.Flexibility,
                PrimaryMuscleGroup = MuscleGroup.Core,
                SecondaryMuscleGroups = "Full Body",
                Equipment = "Yoga Mat",
                DifficultyLevel = 2,
                IsCustom = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Exercises.AddRange(exercises);

        var workouts = new List<Workout>
        {
            new Workout
            {
                WorkoutId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Full Body Strength",
                Description = "Complete full body workout for building overall strength",
                TargetDurationMinutes = 60,
                DifficultyLevel = 3,
                Goal = "Strength",
                IsTemplate = true,
                IsActive = true,
                ScheduledDays = "[\"Monday\",\"Wednesday\",\"Friday\"]",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
            new Workout
            {
                WorkoutId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Morning Cardio",
                Description = "Quick cardio session to start the day",
                TargetDurationMinutes = 30,
                DifficultyLevel = 2,
                Goal = "Weight Loss",
                IsTemplate = false,
                IsActive = true,
                ScheduledDays = "[\"Tuesday\",\"Thursday\",\"Saturday\"]",
                CreatedAt = DateTime.UtcNow.AddDays(-20),
            },
            new Workout
            {
                WorkoutId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Upper Body Focus",
                Description = "Intense upper body workout targeting chest, back, and arms",
                TargetDurationMinutes = 45,
                DifficultyLevel = 4,
                Goal = "Muscle Growth",
                IsTemplate = true,
                IsActive = true,
                ScheduledDays = "[\"Monday\",\"Thursday\"]",
                CreatedAt = DateTime.UtcNow.AddDays(-15),
            },
            new Workout
            {
                WorkoutId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Flexibility & Recovery",
                Description = "Gentle stretching and yoga for recovery days",
                TargetDurationMinutes = 30,
                DifficultyLevel = 1,
                Goal = "Flexibility",
                IsTemplate = false,
                IsActive = true,
                ScheduledDays = "[\"Sunday\"]",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
        };

        context.Workouts.AddRange(workouts);

        // Add sample progress records for the first workout
        var progressRecords = new List<ProgressRecord>
        {
            new ProgressRecord
            {
                ProgressRecordId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                WorkoutId = workouts[0].WorkoutId,
                ActualDurationMinutes = 65,
                CaloriesBurned = 450,
                PerformanceRating = 4,
                Notes = "Great workout, felt strong on all lifts",
                ExerciseDetails = "{\"exercises\": [{\"name\": \"Barbell Squat\", \"sets\": 4, \"reps\": 8, \"weight\": 225}]}",
                CompletedAt = DateTime.UtcNow.AddDays(-3),
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new ProgressRecord
            {
                ProgressRecordId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                WorkoutId = workouts[0].WorkoutId,
                ActualDurationMinutes = 58,
                CaloriesBurned = 420,
                PerformanceRating = 5,
                Notes = "Personal record on bench press!",
                ExerciseDetails = "{\"exercises\": [{\"name\": \"Bench Press\", \"sets\": 3, \"reps\": 5, \"weight\": 185}]}",
                CompletedAt = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            },
        };

        context.ProgressRecords.AddRange(progressRecords);

        await context.SaveChangesAsync();
    }
}
