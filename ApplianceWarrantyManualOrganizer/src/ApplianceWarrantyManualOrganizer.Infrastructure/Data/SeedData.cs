// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ApplianceWarrantyManualOrganizer.Core.Model.UserAggregate;
using ApplianceWarrantyManualOrganizer.Core.Model.UserAggregate.Entities;
using ApplianceWarrantyManualOrganizer.Core.Services;
namespace ApplianceWarrantyManualOrganizer.Infrastructure;

/// <summary>
/// Provides seed data for the ApplianceWarrantyManualOrganizer database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(ApplianceWarrantyManualOrganizerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Appliances.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedAppliancesAsync(context);
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

    private static async Task SeedAppliancesAsync(ApplianceWarrantyManualOrganizerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var appliances = new List<Appliance>
        {
            new Appliance
            {
                ApplianceId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Samsung French Door Refrigerator",
                ApplianceType = ApplianceType.Refrigerator,
                Brand = "Samsung",
                ModelNumber = "RF28R7351SR",
                SerialNumber = "ABC123456789",
                PurchaseDate = new DateTime(2023, 5, 15),
                PurchasePrice = 2499.99m,
                CreatedAt = DateTime.UtcNow,
            },
            new Appliance
            {
                ApplianceId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "GE Gas Range",
                ApplianceType = ApplianceType.Oven,
                Brand = "GE",
                ModelNumber = "JGB735SPSS",
                SerialNumber = "XYZ987654321",
                PurchaseDate = new DateTime(2022, 3, 20),
                PurchasePrice = 1299.99m,
                CreatedAt = DateTime.UtcNow,
            },
            new Appliance
            {
                ApplianceId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Bosch Dishwasher",
                ApplianceType = ApplianceType.Dishwasher,
                Brand = "Bosch",
                ModelNumber = "SHPM65W55N",
                SerialNumber = "DEF555666777",
                PurchaseDate = new DateTime(2023, 8, 10),
                PurchasePrice = 899.99m,
                CreatedAt = DateTime.UtcNow,
            },
            new Appliance
            {
                ApplianceId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "LG Front Load Washer",
                ApplianceType = ApplianceType.WasherDryer,
                Brand = "LG",
                ModelNumber = "WM3900HWA",
                SerialNumber = "GHI111222333",
                PurchaseDate = new DateTime(2024, 1, 5),
                PurchasePrice = 1099.99m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Appliances.AddRange(appliances);

        // Add sample warranties
        var warranties = new List<Warranty>
        {
            new Warranty
            {
                WarrantyId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ApplianceId = appliances[0].ApplianceId,
                Provider = "Samsung",
                StartDate = appliances[0].PurchaseDate,
                EndDate = appliances[0].PurchaseDate?.AddYears(1),
                CoverageDetails = "1 year manufacturer warranty covering parts and labor",
                DocumentUrl = "https://example.com/samsung-warranty.pdf",
                CreatedAt = DateTime.UtcNow,
            },
            new Warranty
            {
                WarrantyId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ApplianceId = appliances[1].ApplianceId,
                Provider = "GE Appliances",
                StartDate = appliances[1].PurchaseDate,
                EndDate = appliances[1].PurchaseDate?.AddYears(1),
                CoverageDetails = "1 year limited warranty on parts and labor",
                DocumentUrl = "https://example.com/ge-warranty.pdf",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Warranties.AddRange(warranties);

        // Add sample manuals
        var manuals = new List<Manual>
        {
            new Manual
            {
                ManualId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ApplianceId = appliances[0].ApplianceId,
                Title = "Samsung RF28R7351SR User Manual",
                FileUrl = "https://example.com/samsung-rf28r7351sr-manual.pdf",
                FileType = "PDF",
                CreatedAt = DateTime.UtcNow,
            },
            new Manual
            {
                ManualId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ApplianceId = appliances[2].ApplianceId,
                Title = "Bosch SHPM65W55N Installation Guide",
                FileUrl = "https://example.com/bosch-dishwasher-install.pdf",
                FileType = "PDF",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Manuals.AddRange(manuals);

        // Add sample service records
        var serviceRecords = new List<ServiceRecord>
        {
            new ServiceRecord
            {
                ServiceRecordId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ApplianceId = appliances[1].ApplianceId,
                ServiceDate = new DateTime(2024, 6, 15),
                ServiceProvider = "ABC Appliance Repair",
                Description = "Replaced igniter on gas range",
                Cost = 175.00m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.ServiceRecords.AddRange(serviceRecords);

        await context.SaveChangesAsync();
    }
}
