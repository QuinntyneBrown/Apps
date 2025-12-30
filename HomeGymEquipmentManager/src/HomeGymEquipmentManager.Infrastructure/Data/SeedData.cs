// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeGymEquipmentManager.Infrastructure;

/// <summary>
/// Provides seed data for the HomeGymEquipmentManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HomeGymEquipmentManagerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Equipment.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedEquipmentAsync(context);
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

    private static async Task SeedEquipmentAsync(HomeGymEquipmentManagerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var equipment = new List<Equipment>
        {
            new Equipment
            {
                EquipmentId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Treadmill",
                EquipmentType = EquipmentType.Cardio,
                Brand = "NordicTrack",
                Model = "C990",
                PurchaseDate = new DateTime(2023, 6, 15),
                PurchasePrice = 1299.99m,
                Location = "Basement",
                Notes = "Excellent condition, used regularly",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Equipment
            {
                EquipmentId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Adjustable Dumbbells",
                EquipmentType = EquipmentType.Strength,
                Brand = "Bowflex",
                Model = "SelectTech 552",
                PurchaseDate = new DateTime(2023, 8, 20),
                PurchasePrice = 349.99m,
                Location = "Home Gym",
                Notes = "5-52.5 lbs per dumbbell",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Equipment
            {
                EquipmentId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Yoga Mat",
                EquipmentType = EquipmentType.Flexibility,
                Brand = "Manduka",
                Model = "Pro Mat",
                PurchaseDate = new DateTime(2023, 5, 10),
                PurchasePrice = 89.99m,
                Location = "Living Room",
                Notes = "Purple color, extra thick",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Equipment
            {
                EquipmentId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "Resistance Bands Set",
                EquipmentType = EquipmentType.Accessory,
                Brand = "Fit Simplify",
                Model = "Premium Set",
                PurchaseDate = new DateTime(2023, 7, 5),
                PurchasePrice = 24.99m,
                Location = "Storage Closet",
                Notes = "5 bands with different resistance levels",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Equipment.AddRange(equipment);

        // Add sample maintenance for treadmill
        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            EquipmentId = equipment[0].EquipmentId,
            MaintenanceDate = new DateTime(2024, 11, 1),
            Description = "Belt lubrication and general inspection",
            Cost = 0.00m,
            NextMaintenanceDate = new DateTime(2025, 5, 1),
            Notes = "Everything working smoothly",
            CreatedAt = DateTime.UtcNow,
        };

        context.Maintenances.Add(maintenance);

        // Add sample workout mappings
        var workoutMappings = new List<WorkoutMapping>
        {
            new WorkoutMapping
            {
                WorkoutMappingId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                EquipmentId = equipment[1].EquipmentId,
                ExerciseName = "Dumbbell Bench Press",
                MuscleGroup = "Chest",
                Instructions = "Lie on bench, press dumbbells up from chest level",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new WorkoutMapping
            {
                WorkoutMappingId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                EquipmentId = equipment[1].EquipmentId,
                ExerciseName = "Dumbbell Rows",
                MuscleGroup = "Back",
                Instructions = "Bend at waist, pull dumbbell to hip",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new WorkoutMapping
            {
                WorkoutMappingId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                EquipmentId = equipment[0].EquipmentId,
                ExerciseName = "Interval Running",
                MuscleGroup = "Cardio",
                Instructions = "Alternate between 2 min sprint and 1 min jog",
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.WorkoutMappings.AddRange(workoutMappings);

        await context.SaveChangesAsync();
    }
}
