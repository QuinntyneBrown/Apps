// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AnnualHealthScreeningReminder.Core.Models.UserAggregate;
using AnnualHealthScreeningReminder.Core.Models.UserAggregate.Entities;
using AnnualHealthScreeningReminder.Core.Services;
namespace AnnualHealthScreeningReminder.Infrastructure;

/// <summary>
/// Provides seed data for the AnnualHealthScreeningReminder database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(AnnualHealthScreeningReminderContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Screenings.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedScreeningsAsync(context);
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

    private static async Task SeedScreeningsAsync(AnnualHealthScreeningReminderContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var screenings = new List<Screening>
        {
            new Screening
            {
                ScreeningId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ScreeningType = ScreeningType.PhysicalExam,
                Name = "Annual Physical Exam",
                RecommendedFrequencyMonths = 12,
                LastScreeningDate = new DateTime(2024, 1, 15),
                NextDueDate = new DateTime(2025, 1, 15),
                Provider = "Dr. Smith",
                Notes = "Check blood pressure and cholesterol",
                CreatedAt = DateTime.UtcNow,
            },
            new Screening
            {
                ScreeningId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                ScreeningType = ScreeningType.DentalCheckup,
                Name = "Dental Cleaning & Checkup",
                RecommendedFrequencyMonths = 6,
                LastScreeningDate = new DateTime(2024, 6, 20),
                NextDueDate = new DateTime(2024, 12, 20),
                Provider = "Dr. Johnson (Dentist)",
                Notes = "Routine cleaning and cavity check",
                CreatedAt = DateTime.UtcNow,
            },
            new Screening
            {
                ScreeningId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                ScreeningType = ScreeningType.VisionTest,
                Name = "Eye Exam",
                RecommendedFrequencyMonths = 24,
                LastScreeningDate = new DateTime(2023, 3, 10),
                NextDueDate = new DateTime(2025, 3, 10),
                Provider = "Vision Care Center",
                Notes = "Check for prescription changes",
                CreatedAt = DateTime.UtcNow,
            },
            new Screening
            {
                ScreeningId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                ScreeningType = ScreeningType.BloodWork,
                Name = "Blood Work Panel",
                RecommendedFrequencyMonths = 12,
                NextDueDate = new DateTime(2025, 2, 1),
                Provider = "LabCorp",
                Notes = "Fasting required - cholesterol, glucose, thyroid",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Screenings.AddRange(screenings);

        // Add sample appointments
        var appointments = new List<Appointment>
        {
            new Appointment
            {
                AppointmentId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ScreeningId = screenings[0].ScreeningId,
                AppointmentDate = new DateTime(2025, 1, 15, 9, 0, 0),
                Location = "123 Main St, Medical Center",
                Provider = "Dr. Smith",
                IsCompleted = false,
                Notes = "Bring previous test results",
                CreatedAt = DateTime.UtcNow,
            },
            new Appointment
            {
                AppointmentId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ScreeningId = screenings[1].ScreeningId,
                AppointmentDate = new DateTime(2024, 6, 20, 14, 30, 0),
                Location = "456 Dental Ave",
                Provider = "Dr. Johnson",
                IsCompleted = true,
                Notes = "No cavities found",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Appointments.AddRange(appointments);

        // Add sample reminders
        var reminders = new List<Reminder>
        {
            new Reminder
            {
                ReminderId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ScreeningId = screenings[0].ScreeningId,
                ReminderDate = new DateTime(2025, 1, 8),
                Message = "Don't forget your annual physical exam on January 15th!",
                IsSent = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Reminder
            {
                ReminderId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ScreeningId = screenings[3].ScreeningId,
                ReminderDate = new DateTime(2025, 1, 25),
                Message = "Schedule your blood work panel - remember to fast 12 hours before",
                IsSent = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Reminders.AddRange(reminders);

        await context.SaveChangesAsync();
    }
}
