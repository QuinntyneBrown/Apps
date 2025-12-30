// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MedicationReminderSystemContext.
/// </summary>
[TestFixture]
public class MedicationReminderSystemContextTests
{
    private MedicationReminderSystemContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MedicationReminderSystemContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MedicationReminderSystemContext(options);
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
    /// Tests that Medications can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Medications_CanAddAndRetrieve()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Medication",
            MedicationType = MedicationType.Tablet,
            Dosage = "10mg",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Medications.Add(medication);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Medications.FindAsync(medication.MedicationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Medication"));
        Assert.That(retrieved.MedicationType, Is.EqualTo(MedicationType.Tablet));
    }

    /// <summary>
    /// Tests that DoseSchedules can be added and retrieved.
    /// </summary>
    [Test]
    public async Task DoseSchedules_CanAddAndRetrieve()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Med",
            MedicationType = MedicationType.Tablet,
            Dosage = "5mg",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = medication.UserId,
            MedicationId = medication.MedicationId,
            ScheduledTime = new TimeSpan(9, 0, 0),
            DaysOfWeek = "Mon,Wed,Fri",
            Frequency = "3 times per week",
            ReminderEnabled = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Medications.Add(medication);
        _context.DoseSchedules.Add(doseSchedule);
        await _context.SaveChangesAsync();

        var retrieved = await _context.DoseSchedules.FindAsync(doseSchedule.DoseScheduleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ScheduledTime, Is.EqualTo(new TimeSpan(9, 0, 0)));
        Assert.That(retrieved.Frequency, Is.EqualTo("3 times per week"));
    }

    /// <summary>
    /// Tests that Refills can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Refills_CanAddAndRetrieve()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Med",
            MedicationType = MedicationType.Capsule,
            Dosage = "20mg",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = medication.UserId,
            MedicationId = medication.MedicationId,
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            Cost = 25.99m,
            RefillsRemaining = 3,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Medications.Add(medication);
        _context.Refills.Add(refill);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Refills.FindAsync(refill.RefillId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Quantity, Is.EqualTo(30));
        Assert.That(retrieved.Cost, Is.EqualTo(25.99m));
    }

    /// <summary>
    /// Tests cascade delete behavior.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Med",
            MedicationType = MedicationType.Tablet,
            Dosage = "10mg",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = medication.UserId,
            MedicationId = medication.MedicationId,
            ScheduledTime = new TimeSpan(8, 0, 0),
            DaysOfWeek = "Daily",
            Frequency = "Daily",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Medications.Add(medication);
        _context.DoseSchedules.Add(doseSchedule);
        await _context.SaveChangesAsync();

        // Act
        _context.Medications.Remove(medication);
        await _context.SaveChangesAsync();

        var retrievedSchedule = await _context.DoseSchedules.FindAsync(doseSchedule.DoseScheduleId);

        // Assert
        Assert.That(retrievedSchedule, Is.Null);
    }
}
