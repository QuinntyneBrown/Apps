// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleMaintenanceLogger.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Infrastructure;

/// <summary>
/// Provides seed data for the VehicleMaintenanceLogger database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(VehicleMaintenanceLoggerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

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

    private static async Task SeedVehiclesAsync(VehicleMaintenanceLoggerContext context)
    {
        var vehicle1 = new Vehicle
        {
            VehicleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Make = "Toyota",
            Model = "Camry",
            Year = 2020,
            VIN = "1HGBH41JXMN109186",
            LicensePlate = "ABC123",
            VehicleType = VehicleType.Sedan,
            CurrentMileage = 45000,
            PurchaseDate = new DateTime(2020, 3, 15),
            IsActive = true,
            Notes = "Primary family vehicle",
        };

        var vehicle2 = new Vehicle
        {
            VehicleId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Make = "Honda",
            Model = "CR-V",
            Year = 2019,
            VIN = "2HGFC2F59JH123456",
            LicensePlate = "XYZ789",
            VehicleType = VehicleType.SUV,
            CurrentMileage = 62000,
            PurchaseDate = new DateTime(2019, 6, 20),
            IsActive = true,
            Notes = "Weekend trips and grocery shopping",
        };

        context.Vehicles.AddRange(vehicle1, vehicle2);

        // Add service records for vehicle 1
        var serviceRecord1 = new ServiceRecord
        {
            ServiceRecordId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            VehicleId = vehicle1.VehicleId,
            ServiceType = ServiceType.OilChange,
            ServiceDate = new DateTime(2024, 10, 15),
            MileageAtService = 42000,
            Cost = 45.99m,
            ServiceProvider = "Quick Lube Plus",
            Description = "Regular oil change with synthetic oil",
            PartsReplaced = new List<string> { "Oil Filter", "Engine Oil" },
            InvoiceNumber = "QL-2024-1015",
        };

        var serviceRecord2 = new ServiceRecord
        {
            ServiceRecordId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            VehicleId = vehicle1.VehicleId,
            ServiceType = ServiceType.TireRotation,
            ServiceDate = new DateTime(2024, 11, 1),
            MileageAtService = 43500,
            Cost = 30.00m,
            ServiceProvider = "Tire Pros",
            Description = "Tire rotation and balance",
            Notes = "Recommended to check alignment next service",
        };

        var serviceRecord3 = new ServiceRecord
        {
            ServiceRecordId = Guid.Parse("33333333-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            VehicleId = vehicle2.VehicleId,
            ServiceType = ServiceType.BrakeService,
            ServiceDate = new DateTime(2024, 9, 20),
            MileageAtService = 60000,
            Cost = 425.50m,
            ServiceProvider = "Honda Service Center",
            Description = "Front brake pad replacement",
            PartsReplaced = new List<string> { "Front Brake Pads", "Brake Fluid" },
            InvoiceNumber = "HSC-2024-0920",
            WarrantyExpirationDate = new DateTime(2025, 9, 20),
        };

        context.ServiceRecords.AddRange(serviceRecord1, serviceRecord2, serviceRecord3);

        // Add maintenance schedules for vehicle 1
        var schedule1 = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111"),
            VehicleId = vehicle1.VehicleId,
            ServiceType = ServiceType.OilChange,
            Description = "Regular oil change schedule",
            MileageInterval = 5000,
            MonthsInterval = 6,
            LastServiceMileage = 42000,
            LastServiceDate = new DateTime(2024, 10, 15),
            NextServiceMileage = 47000,
            NextServiceDate = new DateTime(2025, 4, 15),
            IsActive = true,
        };

        var schedule2 = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.Parse("aaaaaaaa-2222-2222-2222-222222222222"),
            VehicleId = vehicle1.VehicleId,
            ServiceType = ServiceType.TireRotation,
            Description = "Tire rotation schedule",
            MileageInterval = 7500,
            LastServiceMileage = 43500,
            LastServiceDate = new DateTime(2024, 11, 1),
            NextServiceMileage = 51000,
            IsActive = true,
        };

        var schedule3 = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.Parse("bbbbbbbb-1111-1111-1111-111111111111"),
            VehicleId = vehicle2.VehicleId,
            ServiceType = ServiceType.BrakeService,
            Description = "Brake inspection schedule",
            MileageInterval = 30000,
            LastServiceMileage = 60000,
            LastServiceDate = new DateTime(2024, 9, 20),
            NextServiceMileage = 90000,
            IsActive = true,
            Notes = "Check brake pads and rotors",
        };

        context.MaintenanceSchedules.AddRange(schedule1, schedule2, schedule3);

        await context.SaveChangesAsync();
    }
}
