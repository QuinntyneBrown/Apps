// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using CarModificationPartsDatabase.Core.Models.UserAggregate;
using CarModificationPartsDatabase.Core.Models.UserAggregate.Entities;
using CarModificationPartsDatabase.Core.Services;
namespace CarModificationPartsDatabase.Infrastructure;

/// <summary>
/// Provides seed data for the CarModificationPartsDatabase database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(CarModificationPartsDatabaseContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Modifications.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedModificationsAsync(context);
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

    private static async Task SeedModificationsAsync(CarModificationPartsDatabaseContext context)
    {
        var modifications = new List<Modification>
        {
            new Modification
            {
                ModificationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Cold Air Intake System",
                Category = ModCategory.Engine,
                Description = "High-flow cold air intake system for improved engine performance",
                Manufacturer = "K&N Performance",
                EstimatedCost = 350.00m,
                DifficultyLevel = 2,
                EstimatedInstallationTime = 1.5m,
                PerformanceGain = "10-15 HP increase, improved throttle response",
                CompatibleVehicles = new List<string> { "Honda Civic 2016-2021", "Honda Accord 2018-2023" },
                RequiredTools = new List<string> { "Socket Set", "Screwdrivers", "Pliers" },
                Notes = "May require ECU tuning for optimal performance",
            },
            new Modification
            {
                ModificationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Performance Exhaust System",
                Category = ModCategory.Exhaust,
                Description = "Cat-back exhaust system with improved flow and sound",
                Manufacturer = "Borla",
                EstimatedCost = 1200.00m,
                DifficultyLevel = 3,
                EstimatedInstallationTime = 3.0m,
                PerformanceGain = "15-20 HP increase, deeper exhaust note",
                CompatibleVehicles = new List<string> { "Ford Mustang GT 2015-2023", "Ford F-150 5.0L 2018-2023" },
                RequiredTools = new List<string> { "Impact Wrench", "Jack Stands", "Penetrating Oil" },
                Notes = "Professional installation recommended",
            },
        };

        context.Modifications.AddRange(modifications);

        // Add sample parts
        var parts = new List<Part>
        {
            new Part
            {
                PartId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "High-Flow Air Filter",
                PartNumber = "KN-33-2304",
                Manufacturer = "K&N Performance",
                Description = "Washable and reusable air filter with improved airflow",
                Price = 59.99m,
                Category = ModCategory.Engine,
                CompatibleVehicles = new List<string> { "Honda Civic 2016-2021", "Honda Accord 2018-2023" },
                WarrantyInfo = "10-year/million-mile limited warranty",
                Weight = 1.5m,
                Dimensions = "14 x 10 x 3 inches",
                InStock = true,
                Supplier = "Performance Auto Parts Inc",
                Notes = "Clean and re-oil every 50,000 miles",
            },
            new Part
            {
                PartId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Stainless Steel Muffler",
                PartNumber = "BOR-11832",
                Manufacturer = "Borla",
                Description = "Performance muffler with aggressive sound",
                Price = 449.99m,
                Category = ModCategory.Exhaust,
                CompatibleVehicles = new List<string> { "Ford Mustang GT 2015-2023" },
                WarrantyInfo = "Lifetime warranty",
                Weight = 25.0m,
                Dimensions = "24 x 12 x 12 inches",
                InStock = true,
                Supplier = "Exhaust Systems Direct",
            },
        };

        context.Parts.AddRange(parts);

        // Add sample installations
        var installations = new List<Installation>
        {
            new Installation
            {
                InstallationId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ModificationId = modifications[0].ModificationId,
                VehicleInfo = "2020 Honda Civic Sport",
                InstallationDate = DateTime.UtcNow.AddMonths(-2),
                InstalledBy = "John's Auto Shop",
                InstallationCost = 150.00m,
                PartsCost = 350.00m,
                LaborHours = 1.5m,
                PartsUsed = new List<string> { "K&N Cold Air Intake", "Clamps", "Heat Shield" },
                Notes = "Installation went smoothly, noticeable performance improvement",
                DifficultyRating = 2,
                SatisfactionRating = 5,
                Photos = new List<string> { "https://example.com/intake-install-1.jpg", "https://example.com/intake-install-2.jpg" },
                IsCompleted = true,
            },
        };

        context.Installations.AddRange(installations);

        await context.SaveChangesAsync();
    }
}
