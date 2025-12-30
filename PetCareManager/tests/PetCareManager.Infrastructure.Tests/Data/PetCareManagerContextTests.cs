// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PetCareManagerContext.
/// </summary>
[TestFixture]
public class PetCareManagerContextTests
{
    private PetCareManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PetCareManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PetCareManagerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Pets can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Pets_CanAddAndRetrieve()
    {
        // Arrange
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Buddy",
            PetType = PetType.Dog,
            Breed = "Labrador",
            DateOfBirth = new DateTime(2020, 6, 15),
            Color = "Yellow",
            Weight = 30.5m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Pets.FindAsync(pet.PetId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Buddy"));
        Assert.That(retrieved.PetType, Is.EqualTo(PetType.Dog));
        Assert.That(retrieved.Breed, Is.EqualTo("Labrador"));
    }

    /// <summary>
    /// Tests that VetAppointments can be added and retrieved.
    /// </summary>
    [Test]
    public async Task VetAppointments_CanAddAndRetrieve()
    {
        // Arrange
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Buddy",
            PetType = PetType.Dog,
            CreatedAt = DateTime.UtcNow,
        };

        var appointment = new VetAppointment
        {
            VetAppointmentId = Guid.NewGuid(),
            PetId = pet.PetId,
            AppointmentDate = DateTime.UtcNow.AddDays(7),
            VetName = "Dr. Smith",
            Reason = "Annual checkup",
            Cost = 100.00m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Pets.Add(pet);
        _context.VetAppointments.Add(appointment);
        await _context.SaveChangesAsync();

        var retrieved = await _context.VetAppointments.FindAsync(appointment.VetAppointmentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.VetName, Is.EqualTo("Dr. Smith"));
        Assert.That(retrieved.Reason, Is.EqualTo("Annual checkup"));
        Assert.That(retrieved.Cost, Is.EqualTo(100.00m));
    }

    /// <summary>
    /// Tests that Medications can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Medications_CanAddAndRetrieve()
    {
        // Arrange
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Buddy",
            PetType = PetType.Dog,
            CreatedAt = DateTime.UtcNow,
        };

        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            PetId = pet.PetId,
            Name = "Flea Treatment",
            Dosage = "1 tablet",
            Frequency = "Monthly",
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Pets.Add(pet);
        _context.Medications.Add(medication);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Medications.FindAsync(medication.MedicationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Flea Treatment"));
        Assert.That(retrieved.Dosage, Is.EqualTo("1 tablet"));
        Assert.That(retrieved.Frequency, Is.EqualTo("Monthly"));
    }

    /// <summary>
    /// Tests that Vaccinations can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Vaccinations_CanAddAndRetrieve()
    {
        // Arrange
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Buddy",
            PetType = PetType.Dog,
            CreatedAt = DateTime.UtcNow,
        };

        var vaccination = new Vaccination
        {
            VaccinationId = Guid.NewGuid(),
            PetId = pet.PetId,
            Name = "Rabies",
            DateAdministered = DateTime.UtcNow.AddMonths(-6),
            NextDueDate = DateTime.UtcNow.AddYears(3),
            VetName = "Dr. Johnson",
            CreatedAt = DateTime.UtcNow.AddMonths(-6),
        };

        // Act
        _context.Pets.Add(pet);
        _context.Vaccinations.Add(vaccination);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Vaccinations.FindAsync(vaccination.VaccinationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Rabies"));
        Assert.That(retrieved.VetName, Is.EqualTo("Dr. Johnson"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Buddy",
            PetType = PetType.Dog,
            CreatedAt = DateTime.UtcNow,
        };

        var appointment = new VetAppointment
        {
            VetAppointmentId = Guid.NewGuid(),
            PetId = pet.PetId,
            AppointmentDate = DateTime.UtcNow.AddDays(7),
            VetName = "Dr. Smith",
            CreatedAt = DateTime.UtcNow,
        };

        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            PetId = pet.PetId,
            Name = "Flea Treatment",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Pets.Add(pet);
        _context.VetAppointments.Add(appointment);
        _context.Medications.Add(medication);
        await _context.SaveChangesAsync();

        // Act
        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();

        var retrievedAppointment = await _context.VetAppointments.FindAsync(appointment.VetAppointmentId);
        var retrievedMedication = await _context.Medications.FindAsync(medication.MedicationId);

        // Assert
        Assert.That(retrievedAppointment, Is.Null);
        Assert.That(retrievedMedication, Is.Null);
    }
}
