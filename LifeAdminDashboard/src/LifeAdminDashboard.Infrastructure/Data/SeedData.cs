// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LifeAdminDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using LifeAdminDashboard.Core.Model.UserAggregate;
using LifeAdminDashboard.Core.Model.UserAggregate.Entities;
using LifeAdminDashboard.Core.Services;
namespace LifeAdminDashboard.Infrastructure;

/// <summary>
/// Provides seed data for the LifeAdminDashboard database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(LifeAdminDashboardContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Tasks.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedTasksAsync(context);
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

    private static async Task SeedTasksAsync(LifeAdminDashboardContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var tasks = new List<AdminTask>
        {
            new AdminTask
            {
                AdminTaskId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "File Taxes",
                Description = "Prepare and file annual tax return",
                Category = TaskCategory.Financial,
                Priority = TaskPriority.High,
                DueDate = new DateTime(2024, 4, 15),
                IsCompleted = false,
                IsRecurring = true,
                RecurrencePattern = "Yearly",
                CreatedAt = DateTime.UtcNow,
            },
            new AdminTask
            {
                AdminTaskId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Schedule Annual Medical Checkup",
                Description = "Book appointment with primary care physician",
                Category = TaskCategory.Medical,
                Priority = TaskPriority.Medium,
                DueDate = DateTime.UtcNow.AddMonths(1),
                IsCompleted = false,
                IsRecurring = true,
                RecurrencePattern = "Yearly",
                CreatedAt = DateTime.UtcNow,
            },
            new AdminTask
            {
                AdminTaskId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "Update Insurance Beneficiaries",
                Description = "Review and update life insurance beneficiary information",
                Category = TaskCategory.Legal,
                Priority = TaskPriority.Medium,
                DueDate = DateTime.UtcNow.AddDays(30),
                IsCompleted = false,
                Notes = "Check all policies including employer-provided coverage",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Tasks.AddRange(tasks);

        var renewals = new List<Renewal>
        {
            new Renewal
            {
                RenewalId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Car Insurance",
                RenewalType = "Insurance",
                Provider = "State Farm",
                RenewalDate = DateTime.UtcNow.AddMonths(2),
                Cost = 850.00m,
                Frequency = "Yearly",
                IsAutoRenewal = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Renewal
            {
                RenewalId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Netflix Subscription",
                RenewalType = "Subscription",
                Provider = "Netflix",
                RenewalDate = DateTime.UtcNow.AddDays(15),
                Cost = 15.99m,
                Frequency = "Monthly",
                IsAutoRenewal = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Renewal
            {
                RenewalId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Driver's License",
                RenewalType = "License",
                Provider = "DMV",
                RenewalDate = new DateTime(2025, 6, 1),
                Cost = 35.00m,
                Frequency = "Every 5 years",
                IsAutoRenewal = false,
                IsActive = true,
                Notes = "Bring proof of residence",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Renewals.AddRange(renewals);

        var deadlines = new List<Deadline>
        {
            new Deadline
            {
                DeadlineId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Title = "Property Tax Payment",
                Description = "Pay annual property tax",
                DeadlineDateTime = new DateTime(2024, 12, 15),
                Category = "Financial",
                IsCompleted = false,
                RemindersEnabled = true,
                ReminderDaysAdvance = 30,
                Notes = "Check county website for exact amount",
                CreatedAt = DateTime.UtcNow,
            },
            new Deadline
            {
                DeadlineId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Title = "Health Insurance Open Enrollment",
                Description = "Review and select health insurance plan",
                DeadlineDateTime = new DateTime(2024, 11, 30),
                Category = "Medical",
                IsCompleted = false,
                RemindersEnabled = true,
                ReminderDaysAdvance = 14,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Deadlines.AddRange(deadlines);

        await context.SaveChangesAsync();
    }
}
