// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Infrastructure.Tests;

/// <summary>
/// Unit tests for the AnnualHealthScreeningReminderContext.
/// </summary>
[TestFixture]
public class AnnualHealthScreeningReminderContextTests
{
    private AnnualHealthScreeningReminderContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AnnualHealthScreeningReminderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AnnualHealthScreeningReminderContext(options);
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
    /// Tests that Screenings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Screenings_CanAddAndRetrieve()
    {
        // Arrange
        var screening = new Screening
        {
            ScreeningId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ScreeningType = ScreeningType.PhysicalExam,
            Name = "Annual Physical",
            RecommendedFrequencyMonths = 12,
            NextDueDate = DateTime.UtcNow.AddMonths(12),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Screenings.Add(screening);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Screenings.FindAsync(screening.ScreeningId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Annual Physical"));
        Assert.That(retrieved.ScreeningType, Is.EqualTo(ScreeningType.PhysicalExam));
    }

    /// <summary>
    /// Tests that Appointments can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Appointments_CanAddAndRetrieve()
    {
        // Arrange
        var screening = new Screening
        {
            ScreeningId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ScreeningType = ScreeningType.DentalCheckup,
            Name = "Dental Cleaning",
            RecommendedFrequencyMonths = 6,
            CreatedAt = DateTime.UtcNow,
        };

        var appointment = new Appointment
        {
            AppointmentId = Guid.NewGuid(),
            UserId = screening.UserId,
            ScreeningId = screening.ScreeningId,
            AppointmentDate = DateTime.UtcNow.AddDays(30),
            Location = "123 Dental Ave",
            Provider = "Dr. Smith",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Screenings.Add(screening);
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Appointments.FindAsync(appointment.AppointmentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Location, Is.EqualTo("123 Dental Ave"));
        Assert.That(retrieved.IsCompleted, Is.False);
    }

    /// <summary>
    /// Tests that Reminders can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reminders_CanAddAndRetrieve()
    {
        // Arrange
        var screening = new Screening
        {
            ScreeningId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ScreeningType = ScreeningType.BloodWork,
            Name = "Blood Work",
            RecommendedFrequencyMonths = 12,
            CreatedAt = DateTime.UtcNow,
        };

        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = screening.UserId,
            ScreeningId = screening.ScreeningId,
            ReminderDate = DateTime.UtcNow.AddDays(7),
            Message = "Blood work appointment coming up",
            IsSent = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Screenings.Add(screening);
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reminders.FindAsync(reminder.ReminderId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Message, Is.EqualTo("Blood work appointment coming up"));
        Assert.That(retrieved.IsSent, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedAppointments()
    {
        // Arrange
        var screening = new Screening
        {
            ScreeningId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ScreeningType = ScreeningType.PhysicalExam,
            Name = "Physical",
            RecommendedFrequencyMonths = 12,
            CreatedAt = DateTime.UtcNow,
        };

        var appointment = new Appointment
        {
            AppointmentId = Guid.NewGuid(),
            UserId = screening.UserId,
            ScreeningId = screening.ScreeningId,
            AppointmentDate = DateTime.UtcNow.AddDays(30),
            Location = "Medical Center",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Screenings.Add(screening);
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        // Act
        _context.Screenings.Remove(screening);
        await _context.SaveChangesAsync();

        var retrievedAppointment = await _context.Appointments.FindAsync(appointment.AppointmentId);

        // Assert
        Assert.That(retrievedAppointment, Is.Null);
    }
}
