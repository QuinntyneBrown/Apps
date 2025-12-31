// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeMaintenanceSchedule.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using HomeMaintenanceSchedule.Core.Model.UserAggregate;
using HomeMaintenanceSchedule.Core.Model.UserAggregate.Entities;
using HomeMaintenanceSchedule.Core.Services;
namespace HomeMaintenanceSchedule.Infrastructure;

/// <summary>
/// Provides seed data for the HomeMaintenanceSchedule database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HomeMaintenanceScheduleContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.MaintenanceTasks.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedMaintenanceTasksAsync(context);
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

    private static async Task SeedMaintenanceTasksAsync(HomeMaintenanceScheduleContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Add contractors first
        var contractors = new List<Contractor>
        {
            new Contractor
            {
                ContractorId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "ABC HVAC Services",
                Specialty = "Heating & Cooling",
                PhoneNumber = "555-0101",
                Email = "info@abchvac.com",
                Website = "https://abchvac.com",
                IsInsured = true,
                Rating = 5,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Contractor
            {
                ContractorId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Smith Plumbing",
                Specialty = "Plumbing",
                PhoneNumber = "555-0202",
                Email = "contact@smithplumbing.com",
                IsInsured = true,
                Rating = 4,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Contractor
            {
                ContractorId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Elite Landscaping",
                Specialty = "Landscaping",
                PhoneNumber = "555-0303",
                Email = "service@elitelandscaping.com",
                Website = "https://elitelandscaping.com",
                IsInsured = true,
                Rating = 5,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Contractors.AddRange(contractors);

        // Add maintenance tasks
        var tasks = new List<MaintenanceTask>
        {
            new MaintenanceTask
            {
                MaintenanceTaskId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "HVAC Filter Replacement",
                Description = "Replace air filters in HVAC system",
                MaintenanceType = MaintenanceType.Preventive,
                Status = TaskStatus.Scheduled,
                DueDate = DateTime.UtcNow.AddDays(30),
                RecurrenceFrequencyDays = 90,
                EstimatedCost = 50m,
                Priority = 2,
                Location = "Basement",
                ContractorId = contractors[0].ContractorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new MaintenanceTask
            {
                MaintenanceTaskId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Gutter Cleaning",
                Description = "Clean gutters and downspouts",
                MaintenanceType = MaintenanceType.Seasonal,
                Status = TaskStatus.Scheduled,
                DueDate = DateTime.UtcNow.AddDays(15),
                RecurrenceFrequencyDays = 180,
                EstimatedCost = 150m,
                Priority = 3,
                Location = "Exterior",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new MaintenanceTask
            {
                MaintenanceTaskId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Annual HVAC Inspection",
                Description = "Professional inspection and tune-up of HVAC system",
                MaintenanceType = MaintenanceType.Inspection,
                Status = TaskStatus.Completed,
                DueDate = new DateTime(2024, 10, 1),
                CompletedDate = new DateTime(2024, 10, 5),
                RecurrenceFrequencyDays = 365,
                EstimatedCost = 200m,
                ActualCost = 185m,
                Priority = 1,
                Location = "Basement",
                ContractorId = contractors[0].ContractorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new MaintenanceTask
            {
                MaintenanceTaskId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Water Heater Flush",
                Description = "Drain and flush water heater to remove sediment",
                MaintenanceType = MaintenanceType.Preventive,
                Status = TaskStatus.Scheduled,
                DueDate = DateTime.UtcNow.AddDays(45),
                RecurrenceFrequencyDays = 365,
                EstimatedCost = 100m,
                Priority = 2,
                Location = "Garage",
                ContractorId = contractors[1].ContractorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new MaintenanceTask
            {
                MaintenanceTaskId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Lawn Fertilization",
                Description = "Apply spring fertilizer to lawn",
                MaintenanceType = MaintenanceType.Seasonal,
                Status = TaskStatus.InProgress,
                DueDate = DateTime.UtcNow.AddDays(7),
                RecurrenceFrequencyDays = 90,
                EstimatedCost = 75m,
                Priority = 3,
                Location = "Yard",
                ContractorId = contractors[2].ContractorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
        };

        context.MaintenanceTasks.AddRange(tasks);

        // Add service log for completed task
        var serviceLog = new ServiceLog
        {
            ServiceLogId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            MaintenanceTaskId = tasks[2].MaintenanceTaskId,
            ServiceDate = new DateTime(2024, 10, 5),
            Description = "Performed annual HVAC inspection and tune-up",
            ContractorId = contractors[0].ContractorId,
            Cost = 185m,
            Notes = "System is in excellent condition. Replaced one worn belt.",
            PartsUsed = "HVAC belt (model XYZ-123)",
            LaborHours = 2.5m,
            WarrantyExpiresAt = new DateTime(2025, 10, 5),
            CreatedAt = DateTime.UtcNow,
        };

        context.ServiceLogs.Add(serviceLog);

        await context.SaveChangesAsync();
    }
}
