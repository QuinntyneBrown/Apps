// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RoadsideAssistanceInfoHub.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using RoadsideAssistanceInfoHub.Core.Model.UserAggregate;
using RoadsideAssistanceInfoHub.Core.Model.UserAggregate.Entities;
using RoadsideAssistanceInfoHub.Core.Services;
namespace RoadsideAssistanceInfoHub.Infrastructure;

/// <summary>
/// Provides seed data for the RoadsideAssistanceInfoHub database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(RoadsideAssistanceInfoHubContext context, ILogger logger, IPasswordHasher passwordHasher)
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

    private static async Task SeedDataAsync(RoadsideAssistanceInfoHubContext context)
    {
        var vehicles = new List<Vehicle>
        {
            new Vehicle
            {
                VehicleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                VIN = "1HGBH41JXMN109186",
                LicensePlate = "ABC1234",
                Color = "Silver",
                CurrentMileage = 45000m,
                OwnerName = "John Smith",
                IsActive = true,
            },
            new Vehicle
            {
                VehicleId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Make = "Honda",
                Model = "CR-V",
                Year = 2019,
                VIN = "5FNRL6H78KB012345",
                LicensePlate = "XYZ5678",
                Color = "Blue",
                CurrentMileage = 62000m,
                OwnerName = "Jane Doe",
                IsActive = true,
            },
            new Vehicle
            {
                VehicleId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Make = "Ford",
                Model = "F-150",
                Year = 2021,
                VIN = "1FTEW1EP5MFA12345",
                LicensePlate = "TRK9999",
                Color = "Black",
                CurrentMileage = 28000m,
                OwnerName = "Mike Johnson",
                IsActive = true,
            },
        };

        context.Vehicles.AddRange(vehicles);

        // Add insurance info for first vehicle
        var insuranceInfos = new List<InsuranceInfo>
        {
            new InsuranceInfo
            {
                InsuranceInfoId = Guid.NewGuid(),
                VehicleId = vehicles[0].VehicleId,
                InsuranceCompany = "State Farm",
                PolicyNumber = "SF-123456789",
                PolicyHolder = "John Smith",
                PolicyStartDate = new DateTime(2024, 1, 1),
                PolicyEndDate = new DateTime(2024, 12, 31),
                AgentName = "Sarah Williams",
                AgentPhone = "(555) 123-4567",
                CompanyPhone = "(800) 111-2222",
                ClaimsPhone = "(800) 333-4444",
                CoverageType = "Full Coverage",
                Deductible = 500m,
                Premium = 1200m,
                IncludesRoadsideAssistance = true,
                Notes = "Includes collision and comprehensive coverage",
            },
            new InsuranceInfo
            {
                InsuranceInfoId = Guid.NewGuid(),
                VehicleId = vehicles[1].VehicleId,
                InsuranceCompany = "Geico",
                PolicyNumber = "GC-987654321",
                PolicyHolder = "Jane Doe",
                PolicyStartDate = new DateTime(2024, 3, 1),
                PolicyEndDate = new DateTime(2025, 2, 28),
                CompanyPhone = "(800) 555-6666",
                ClaimsPhone = "(800) 777-8888",
                CoverageType = "Liability Only",
                Deductible = 1000m,
                Premium = 800m,
                IncludesRoadsideAssistance = false,
            },
        };

        context.InsuranceInfos.AddRange(insuranceInfos);

        // Add emergency contacts
        var emergencyContacts = new List<EmergencyContact>
        {
            new EmergencyContact
            {
                EmergencyContactId = Guid.NewGuid(),
                Name = "AAA Roadside Assistance",
                Relationship = "Service Provider",
                PhoneNumber = "(800) 222-4357",
                ContactType = "Tow Service",
                ServiceArea = "National",
                IsPrimaryContact = true,
                IsActive = true,
                Notes = "24/7 emergency roadside assistance",
            },
            new EmergencyContact
            {
                EmergencyContactId = Guid.NewGuid(),
                Name = "Bob's Towing",
                Relationship = "Local Tow Service",
                PhoneNumber = "(555) 987-6543",
                ContactType = "Tow Service",
                ServiceArea = "City and surrounding areas",
                IsPrimaryContact = false,
                IsActive = true,
                Notes = "Local 24-hour towing service",
            },
            new EmergencyContact
            {
                EmergencyContactId = Guid.NewGuid(),
                Name = "Mary Johnson",
                Relationship = "Spouse",
                PhoneNumber = "(555) 111-2222",
                AlternatePhone = "(555) 333-4444",
                Email = "mary.johnson@email.com",
                ContactType = "Personal",
                IsPrimaryContact = false,
                IsActive = true,
            },
        };

        context.EmergencyContacts.AddRange(emergencyContacts);

        // Add policies
        var policies = new List<Policy>
        {
            new Policy
            {
                PolicyId = Guid.NewGuid(),
                VehicleId = vehicles[0].VehicleId,
                Provider = "AAA Plus",
                PolicyNumber = "AAA-123456",
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 12, 31),
                EmergencyPhone = "(800) 222-4357",
                MaxTowingDistance = 100,
                ServiceCallsPerYear = 4,
                CoveredServices = new List<string> { "Towing", "Battery Jump", "Flat Tire", "Fuel Delivery", "Lockout" },
                AnnualPremium = 120m,
                CoversBatteryService = true,
                CoversFlatTire = true,
                CoversFuelDelivery = true,
                CoversLockout = true,
                Notes = "Premium roadside assistance plan",
            },
            new Policy
            {
                PolicyId = Guid.NewGuid(),
                VehicleId = vehicles[1].VehicleId,
                Provider = "Better World Club",
                PolicyNumber = "BWC-789012",
                StartDate = new DateTime(2024, 6, 1),
                EndDate = new DateTime(2025, 5, 31),
                EmergencyPhone = "(866) 238-1137",
                MaxTowingDistance = 50,
                ServiceCallsPerYear = 3,
                CoveredServices = new List<string> { "Towing", "Battery Jump", "Flat Tire" },
                AnnualPremium = 79m,
                CoversBatteryService = true,
                CoversFlatTire = true,
                CoversFuelDelivery = false,
                CoversLockout = false,
                Notes = "Eco-friendly roadside assistance",
            },
        };

        context.Policies.AddRange(policies);

        await context.SaveChangesAsync();
    }
}
