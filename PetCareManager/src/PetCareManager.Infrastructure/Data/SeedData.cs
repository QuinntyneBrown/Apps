// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PetCareManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PetCareManager.Core.Model.UserAggregate;
using PetCareManager.Core.Model.UserAggregate.Entities;
using PetCareManager.Core.Services;
namespace PetCareManager.Infrastructure;

/// <summary>
/// Provides seed data for the PetCareManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PetCareManagerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Pets.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedPetsAsync(context);
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

    private static async Task SeedPetsAsync(PetCareManagerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Add sample pets
        var pet1 = new Pet
        {
            PetId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            Name = "Max",
            PetType = PetType.Dog,
            Breed = "Golden Retriever",
            DateOfBirth = new DateTime(2019, 5, 15),
            Color = "Golden",
            Weight = 32.5m,
            MicrochipNumber = "123456789012345",
            CreatedAt = DateTime.UtcNow.AddYears(-3),
        };

        var pet2 = new Pet
        {
            PetId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = sampleUserId,
            Name = "Luna",
            PetType = PetType.Cat,
            Breed = "Siamese",
            DateOfBirth = new DateTime(2021, 8, 20),
            Color = "Seal Point",
            Weight = 4.2m,
            MicrochipNumber = "987654321098765",
            CreatedAt = DateTime.UtcNow.AddYears(-1),
        };

        context.Pets.AddRange(pet1, pet2);

        // Add sample vet appointments
        var appointment1 = new VetAppointment
        {
            VetAppointmentId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            PetId = pet1.PetId,
            AppointmentDate = DateTime.UtcNow.AddDays(-30),
            VetName = "Dr. Sarah Johnson",
            Reason = "Annual checkup",
            Notes = "Pet is healthy, all vitals normal",
            Cost = 75.00m,
            CreatedAt = DateTime.UtcNow.AddDays(-35),
        };

        var appointment2 = new VetAppointment
        {
            VetAppointmentId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            PetId = pet2.PetId,
            AppointmentDate = DateTime.UtcNow.AddDays(7),
            VetName = "Dr. Michael Chen",
            Reason = "Vaccination booster",
            Notes = "Scheduled for rabies booster",
            CreatedAt = DateTime.UtcNow.AddDays(-5),
        };

        context.VetAppointments.AddRange(appointment1, appointment2);

        // Add sample medications
        var medication = new Medication
        {
            MedicationId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            PetId = pet1.PetId,
            Name = "Heartgard Plus",
            Dosage = "One chewable tablet",
            Frequency = "Monthly",
            StartDate = DateTime.UtcNow.AddMonths(-6),
            EndDate = DateTime.UtcNow.AddMonths(6),
            CreatedAt = DateTime.UtcNow.AddMonths(-6),
        };

        context.Medications.Add(medication);

        // Add sample vaccinations
        var vaccination1 = new Vaccination
        {
            VaccinationId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            PetId = pet1.PetId,
            Name = "Rabies",
            DateAdministered = DateTime.UtcNow.AddYears(-1),
            NextDueDate = DateTime.UtcNow.AddYears(2),
            VetName = "Dr. Sarah Johnson",
            CreatedAt = DateTime.UtcNow.AddYears(-1),
        };

        var vaccination2 = new Vaccination
        {
            VaccinationId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
            PetId = pet2.PetId,
            Name = "FVRCP",
            DateAdministered = DateTime.UtcNow.AddMonths(-6),
            NextDueDate = DateTime.UtcNow.AddYears(1),
            VetName = "Dr. Michael Chen",
            CreatedAt = DateTime.UtcNow.AddMonths(-6),
        };

        context.Vaccinations.AddRange(vaccination1, vaccination2);

        await context.SaveChangesAsync();
    }
}
