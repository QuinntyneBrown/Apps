// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core.Tests;

public class AppointmentTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesAppointment()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var screeningId = Guid.NewGuid();
        var appointmentDate = DateTime.UtcNow.AddDays(7);
        var location = "City Medical Center";
        var provider = "Dr. Smith";
        var notes = "Annual checkup";

        // Act
        var appointment = new Appointment
        {
            AppointmentId = appointmentId,
            UserId = userId,
            ScreeningId = screeningId,
            AppointmentDate = appointmentDate,
            Location = location,
            Provider = provider,
            Notes = notes,
            IsCompleted = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(appointment.AppointmentId, Is.EqualTo(appointmentId));
            Assert.That(appointment.UserId, Is.EqualTo(userId));
            Assert.That(appointment.ScreeningId, Is.EqualTo(screeningId));
            Assert.That(appointment.AppointmentDate, Is.EqualTo(appointmentDate));
            Assert.That(appointment.Location, Is.EqualTo(location));
            Assert.That(appointment.Provider, Is.EqualTo(provider));
            Assert.That(appointment.Notes, Is.EqualTo(notes));
            Assert.That(appointment.IsCompleted, Is.False);
            Assert.That(appointment.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var appointment = new Appointment();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(appointment.Location, Is.EqualTo(string.Empty));
            Assert.That(appointment.Provider, Is.Null);
            Assert.That(appointment.Notes, Is.Null);
            Assert.That(appointment.IsCompleted, Is.False);
            Assert.That(appointment.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsUpcoming_FutureAppointmentNotCompleted_ReturnsTrue()
    {
        // Arrange
        var appointment = new Appointment
        {
            AppointmentDate = DateTime.UtcNow.AddDays(7),
            IsCompleted = false
        };

        // Act
        var result = appointment.IsUpcoming();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsUpcoming_PastAppointmentNotCompleted_ReturnsFalse()
    {
        // Arrange
        var appointment = new Appointment
        {
            AppointmentDate = DateTime.UtcNow.AddDays(-7),
            IsCompleted = false
        };

        // Act
        var result = appointment.IsUpcoming();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsUpcoming_FutureAppointmentCompleted_ReturnsFalse()
    {
        // Arrange
        var appointment = new Appointment
        {
            AppointmentDate = DateTime.UtcNow.AddDays(7),
            IsCompleted = true
        };

        // Act
        var result = appointment.IsUpcoming();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsUpcoming_PastAppointmentCompleted_ReturnsFalse()
    {
        // Arrange
        var appointment = new Appointment
        {
            AppointmentDate = DateTime.UtcNow.AddDays(-7),
            IsCompleted = true
        };

        // Act
        var result = appointment.IsUpcoming();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Location_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var appointment = new Appointment();
        var location = "Downtown Clinic";

        // Act
        appointment.Location = location;

        // Assert
        Assert.That(appointment.Location, Is.EqualTo(location));
    }

    [Test]
    public void Provider_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var appointment = new Appointment();
        var provider = "Dr. Johnson";

        // Act
        appointment.Provider = provider;

        // Assert
        Assert.That(appointment.Provider, Is.EqualTo(provider));
    }

    [Test]
    public void IsCompleted_CanBeToggled_UpdatesCorrectly()
    {
        // Arrange
        var appointment = new Appointment { IsCompleted = false };

        // Act
        appointment.IsCompleted = true;

        // Assert
        Assert.That(appointment.IsCompleted, Is.True);
    }

    [Test]
    public void Screening_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var appointment = new Appointment();
        var screening = new Screening { ScreeningId = Guid.NewGuid() };

        // Act
        appointment.Screening = screening;

        // Assert
        Assert.That(appointment.Screening, Is.EqualTo(screening));
    }

    [Test]
    public void Notes_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var appointment = new Appointment { Notes = "Some notes" };

        // Act
        appointment.Notes = null;

        // Assert
        Assert.That(appointment.Notes, Is.Null);
    }
}
