// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MedicationReminderSystem.Core.Model.UserAggregate;
using MedicationReminderSystem.Core.Model.UserAggregate.Entities;
using MedicationReminderSystem.Core.Services;
namespace MedicationReminderSystem.Infrastructure;

/// <summary>
/// Provides seed data for the MedicationReminderSystem database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MedicationReminderSystemContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Medications.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedMedicationsAsync(context);
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

    private static async Task SeedMedicationsAsync(MedicationReminderSystemContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Create medications
        var medications = new List<Medication>
        {
            new Medication
            {
                MedicationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Lisinopril",
                MedicationType = MedicationType.Tablet,
                Dosage = "10mg",
                PrescribingDoctor = "Dr. Smith",
                PrescriptionDate = DateTime.UtcNow.AddMonths(-3),
                Purpose = "Blood pressure management",
                Instructions = "Take once daily in the morning",
                SideEffects = "Dizziness, dry cough",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Medication
            {
                MedicationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Vitamin D3",
                MedicationType = MedicationType.Capsule,
                Dosage = "2000 IU",
                PrescribingDoctor = "Dr. Johnson",
                PrescriptionDate = DateTime.UtcNow.AddMonths(-6),
                Purpose = "Vitamin D deficiency",
                Instructions = "Take with food",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Medication
            {
                MedicationId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Ibuprofen",
                MedicationType = MedicationType.Tablet,
                Dosage = "400mg",
                Purpose = "Pain relief",
                Instructions = "Take as needed with food, maximum 3 times daily",
                SideEffects = "Stomach upset, heartburn",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Medications.AddRange(medications);

        // Create dose schedules
        var doseSchedules = new List<DoseSchedule>
        {
            new DoseSchedule
            {
                DoseScheduleId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MedicationId = medications[0].MedicationId,
                ScheduledTime = new TimeSpan(8, 0, 0), // 8:00 AM
                DaysOfWeek = "Mon,Tue,Wed,Thu,Fri,Sat,Sun",
                Frequency = "Daily",
                ReminderEnabled = true,
                ReminderOffsetMinutes = 15,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new DoseSchedule
            {
                DoseScheduleId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MedicationId = medications[1].MedicationId,
                ScheduledTime = new TimeSpan(9, 0, 0), // 9:00 AM
                DaysOfWeek = "Mon,Tue,Wed,Thu,Fri,Sat,Sun",
                Frequency = "Daily",
                ReminderEnabled = true,
                ReminderOffsetMinutes = 10,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.DoseSchedules.AddRange(doseSchedules);

        // Create refill records
        var refills = new List<Refill>
        {
            new Refill
            {
                RefillId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MedicationId = medications[0].MedicationId,
                RefillDate = DateTime.UtcNow.AddDays(-15),
                Quantity = 90,
                PharmacyName = "CVS Pharmacy",
                Cost = 15.50m,
                NextRefillDate = DateTime.UtcNow.AddDays(75),
                RefillsRemaining = 3,
                Notes = "90-day supply",
                CreatedAt = DateTime.UtcNow,
            },
            new Refill
            {
                RefillId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MedicationId = medications[1].MedicationId,
                RefillDate = DateTime.UtcNow.AddDays(-30),
                Quantity = 120,
                PharmacyName = "Walgreens",
                Cost = 8.99m,
                NextRefillDate = DateTime.UtcNow.AddDays(90),
                RefillsRemaining = 5,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Refills.AddRange(refills);

        await context.SaveChangesAsync();
    }
}
