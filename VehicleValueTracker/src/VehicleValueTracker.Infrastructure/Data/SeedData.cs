// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleValueTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using VehicleValueTracker.Core.Models.UserAggregate;
using VehicleValueTracker.Core.Models.UserAggregate.Entities;
using VehicleValueTracker.Core.Services;
namespace VehicleValueTracker.Infrastructure;

/// <summary>
/// Provides seed data for the VehicleValueTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(VehicleValueTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Vehicles.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedVehiclesAsync(context);
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

    private static async Task SeedVehiclesAsync(VehicleValueTrackerContext context)
    {
        var vehicle1 = new Vehicle
        {
            VehicleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Make = "Tesla",
            Model = "Model 3",
            Year = 2021,
            Trim = "Long Range",
            VIN = "5YJ3E1EA5MF123456",
            CurrentMileage = 25000,
            PurchasePrice = 52000m,
            PurchaseDate = new DateTime(2021, 6, 15),
            Color = "Pearl White",
            InteriorType = "Black Premium",
            EngineType = "Electric",
            Transmission = "Single Speed",
            IsCurrentlyOwned = true,
            Notes = "Excellent condition, autopilot included",
        };

        var vehicle2 = new Vehicle
        {
            VehicleId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Make = "BMW",
            Model = "X5",
            Year = 2019,
            Trim = "xDrive40i",
            VIN = "5UXCR6C52KL123456",
            CurrentMileage = 45000,
            PurchasePrice = 65000m,
            PurchaseDate = new DateTime(2019, 3, 20),
            Color = "Alpine White",
            InteriorType = "Black Leather",
            EngineType = "3.0L I6 Turbo",
            Transmission = "8-Speed Automatic",
            IsCurrentlyOwned = true,
            Notes = "Well maintained, all service records available",
        };

        context.Vehicles.AddRange(vehicle1, vehicle2);

        // Add value assessments for vehicle 1
        var assessment1 = new ValueAssessment
        {
            ValueAssessmentId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            VehicleId = vehicle1.VehicleId,
            AssessmentDate = new DateTime(2024, 1, 15),
            EstimatedValue = 45000m,
            MileageAtAssessment = 20000,
            ConditionGrade = ConditionGrade.Excellent,
            ValuationSource = "KBB",
            ExteriorCondition = "No visible scratches or dents",
            InteriorCondition = "Like new, no wear",
            MechanicalCondition = "All systems functioning perfectly",
            DepreciationAmount = 7000m,
            DepreciationPercentage = 13.46m,
        };

        var assessment2 = new ValueAssessment
        {
            ValueAssessmentId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            VehicleId = vehicle1.VehicleId,
            AssessmentDate = new DateTime(2024, 12, 1),
            EstimatedValue = 41000m,
            MileageAtAssessment = 25000,
            ConditionGrade = ConditionGrade.Excellent,
            ValuationSource = "Edmunds",
            ExteriorCondition = "Minimal wear, one small door ding",
            InteriorCondition = "Very good, slight wear on driver's seat",
            MechanicalCondition = "Battery health at 95%, all systems good",
            DepreciationAmount = 11000m,
            DepreciationPercentage = 21.15m,
        };

        var assessment3 = new ValueAssessment
        {
            ValueAssessmentId = Guid.Parse("33333333-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            VehicleId = vehicle2.VehicleId,
            AssessmentDate = new DateTime(2024, 11, 15),
            EstimatedValue = 38000m,
            MileageAtAssessment = 45000,
            ConditionGrade = ConditionGrade.VeryGood,
            ValuationSource = "KBB",
            ExteriorCondition = "Minor scratches on bumper",
            InteriorCondition = "Good, normal wear for mileage",
            MechanicalCondition = "Recent service, runs excellent",
            DepreciationAmount = 27000m,
            DepreciationPercentage = 41.54m,
            KnownIssues = new List<string> { "Minor wind noise at highway speeds" },
        };

        context.ValueAssessments.AddRange(assessment1, assessment2, assessment3);

        // Add market comparisons for vehicle 1
        var comparison1 = new MarketComparison
        {
            MarketComparisonId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            VehicleId = vehicle1.VehicleId,
            ComparisonDate = new DateTime(2024, 12, 1),
            ListingSource = "Autotrader",
            ComparableYear = 2021,
            ComparableMake = "Tesla",
            ComparableModel = "Model 3",
            ComparableTrim = "Long Range",
            ComparableMileage = 28000,
            AskingPrice = 42500m,
            Location = "Los Angeles, CA",
            Condition = "Excellent",
            ListingUrl = "https://autotrader.com/listing123",
            DaysOnMarket = 12,
            IsActive = true,
        };

        var comparison2 = new MarketComparison
        {
            MarketComparisonId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            VehicleId = vehicle1.VehicleId,
            ComparisonDate = new DateTime(2024, 12, 5),
            ListingSource = "CarGurus",
            ComparableYear = 2021,
            ComparableMake = "Tesla",
            ComparableModel = "Model 3",
            ComparableTrim = "Long Range",
            ComparableMileage = 22000,
            AskingPrice = 44000m,
            Location = "San Francisco, CA",
            Condition = "Excellent",
            ListingUrl = "https://cargurus.com/listing456",
            DaysOnMarket = 8,
            IsActive = true,
            Notes = "Single owner, full warranty remaining",
        };

        var comparison3 = new MarketComparison
        {
            MarketComparisonId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            VehicleId = vehicle2.VehicleId,
            ComparisonDate = new DateTime(2024, 11, 20),
            ListingSource = "Cars.com",
            ComparableYear = 2019,
            ComparableMake = "BMW",
            ComparableModel = "X5",
            ComparableTrim = "xDrive40i",
            ComparableMileage = 50000,
            AskingPrice = 36500m,
            Location = "Chicago, IL",
            Condition = "Very Good",
            ListingUrl = "https://cars.com/listing789",
            DaysOnMarket = 25,
            IsActive = true,
        };

        context.MarketComparisons.AddRange(comparison1, comparison2, comparison3);

        await context.SaveChangesAsync();
    }
}
