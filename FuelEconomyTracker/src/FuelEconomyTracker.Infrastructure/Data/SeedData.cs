// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FuelEconomyTracker.Core.Models.UserAggregate;
using FuelEconomyTracker.Core.Models.UserAggregate.Entities;
using FuelEconomyTracker.Core.Services;
namespace FuelEconomyTracker.Infrastructure;

/// <summary>
/// Provides seed data for the FuelEconomyTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FuelEconomyTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
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
                await SeedVehiclesAndFillUpsAsync(context);
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

    private static async Task SeedVehiclesAndFillUpsAsync(FuelEconomyTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var vehicles = new List<Vehicle>
        {
            new Vehicle
            {
                VehicleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                VIN = "1HGBH41JXMN109186",
                LicensePlate = "ABC1234",
                FuelType = FuelType.Gasoline,
                TankCapacity = 15.8m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-180),
            },
            new Vehicle
            {
                VehicleId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Make = "Honda",
                Model = "Civic",
                Year = 2019,
                FuelType = FuelType.Gasoline,
                TankCapacity = 12.4m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-90),
            },
        };

        context.Vehicles.AddRange(vehicles);

        var fillUps = new List<FillUp>
        {
            new FillUp
            {
                FillUpId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                VehicleId = vehicles[0].VehicleId,
                UserId = sampleUserId,
                Date = DateTime.UtcNow.AddDays(-30),
                Odometer = 25000,
                GallonsFilled = 12.5m,
                PricePerGallon = 3.45m,
                TotalCost = 43.13m,
                IsFullTank = true,
                Location = "Gas Station A",
                Notes = "Regular unleaded",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
            new FillUp
            {
                FillUpId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                VehicleId = vehicles[0].VehicleId,
                UserId = sampleUserId,
                Date = DateTime.UtcNow.AddDays(-23),
                Odometer = 25350,
                GallonsFilled = 11.2m,
                PricePerGallon = 3.52m,
                TotalCost = 39.42m,
                IsFullTank = true,
                Location = "Gas Station B",
                CreatedAt = DateTime.UtcNow.AddDays(-23),
            },
            new FillUp
            {
                FillUpId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                VehicleId = vehicles[0].VehicleId,
                UserId = sampleUserId,
                Date = DateTime.UtcNow.AddDays(-16),
                Odometer = 25720,
                GallonsFilled = 12.8m,
                PricePerGallon = 3.38m,
                TotalCost = 43.26m,
                IsFullTank = true,
                Location = "Gas Station A",
                CreatedAt = DateTime.UtcNow.AddDays(-16),
            },
        };

        context.FillUps.AddRange(fillUps);

        var trips = new List<Trip>
        {
            new Trip
            {
                TripId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                VehicleId = vehicles[0].VehicleId,
                UserId = sampleUserId,
                StartDate = DateTime.UtcNow.AddDays(-20),
                EndDate = DateTime.UtcNow.AddDays(-18),
                StartOdometer = 25350,
                EndOdometer = 25720,
                TripType = TripType.Business,
                Purpose = "Client meeting in another city",
                Notes = "Highway driving, good weather",
                CreatedAt = DateTime.UtcNow.AddDays(-18),
            },
            new Trip
            {
                TripId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                VehicleId = vehicles[0].VehicleId,
                UserId = sampleUserId,
                StartDate = DateTime.UtcNow.AddDays(-10),
                EndDate = DateTime.UtcNow.AddDays(-9),
                StartOdometer = 25720,
                EndOdometer = 25850,
                TripType = TripType.Personal,
                Purpose = "Weekend getaway",
                CreatedAt = DateTime.UtcNow.AddDays(-9),
            },
        };

        context.Trips.AddRange(trips);

        var reports = new List<EfficiencyReport>
        {
            new EfficiencyReport
            {
                EfficiencyReportId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                VehicleId = vehicles[0].VehicleId,
                UserId = sampleUserId,
                PeriodStartDate = DateTime.UtcNow.AddDays(-30),
                PeriodEndDate = DateTime.UtcNow,
                TotalMilesDriven = 850,
                TotalGallonsUsed = 36.5m,
                AverageMPG = 23.3m,
                TotalCost = 125.81m,
                AverageCostPerMile = 0.148m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.EfficiencyReports.AddRange(reports);

        await context.SaveChangesAsync();
    }
}
