// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core.Tests;

public class ScreeningTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesScreening()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var screeningType = ScreeningType.PhysicalExam;
        var name = "Annual Physical";
        var recommendedFrequency = 12;
        var lastScreeningDate = DateTime.UtcNow.AddMonths(-6);
        var nextDueDate = DateTime.UtcNow.AddMonths(6);
        var provider = "Dr. Williams";
        var notes = "Routine annual screening";

        // Act
        var screening = new Screening
        {
            ScreeningId = screeningId,
            UserId = userId,
            ScreeningType = screeningType,
            Name = name,
            RecommendedFrequencyMonths = recommendedFrequency,
            LastScreeningDate = lastScreeningDate,
            NextDueDate = nextDueDate,
            Provider = provider,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(screening.ScreeningId, Is.EqualTo(screeningId));
            Assert.That(screening.UserId, Is.EqualTo(userId));
            Assert.That(screening.ScreeningType, Is.EqualTo(screeningType));
            Assert.That(screening.Name, Is.EqualTo(name));
            Assert.That(screening.RecommendedFrequencyMonths, Is.EqualTo(recommendedFrequency));
            Assert.That(screening.LastScreeningDate, Is.EqualTo(lastScreeningDate));
            Assert.That(screening.NextDueDate, Is.EqualTo(nextDueDate));
            Assert.That(screening.Provider, Is.EqualTo(provider));
            Assert.That(screening.Notes, Is.EqualTo(notes));
            Assert.That(screening.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var screening = new Screening();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(screening.Name, Is.EqualTo(string.Empty));
            Assert.That(screening.RecommendedFrequencyMonths, Is.EqualTo(12));
            Assert.That(screening.LastScreeningDate, Is.Null);
            Assert.That(screening.NextDueDate, Is.Null);
            Assert.That(screening.Provider, Is.Null);
            Assert.That(screening.Notes, Is.Null);
            Assert.That(screening.Appointments, Is.Not.Null);
            Assert.That(screening.Appointments, Is.Empty);
            Assert.That(screening.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsDueSoon_NextDueDateWithinOneMonth_ReturnsTrue()
    {
        // Arrange
        var screening = new Screening
        {
            NextDueDate = DateTime.UtcNow.AddDays(15)
        };

        // Act
        var result = screening.IsDueSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsDueSoon_NextDueDateExactlyOneMonthAway_ReturnsTrue()
    {
        // Arrange
        var screening = new Screening
        {
            NextDueDate = DateTime.UtcNow.AddMonths(1)
        };

        // Act
        var result = screening.IsDueSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsDueSoon_NextDueDateOverOneMonthAway_ReturnsFalse()
    {
        // Arrange
        var screening = new Screening
        {
            NextDueDate = DateTime.UtcNow.AddMonths(2)
        };

        // Act
        var result = screening.IsDueSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsDueSoon_NextDueDateInPast_ReturnsTrue()
    {
        // Arrange
        var screening = new Screening
        {
            NextDueDate = DateTime.UtcNow.AddDays(-7)
        };

        // Act
        var result = screening.IsDueSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsDueSoon_NextDueDateNull_ReturnsFalse()
    {
        // Arrange
        var screening = new Screening
        {
            NextDueDate = null
        };

        // Act
        var result = screening.IsDueSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ScreeningType_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var screening = new Screening();

        // Act
        screening.ScreeningType = ScreeningType.Mammogram;

        // Assert
        Assert.That(screening.ScreeningType, Is.EqualTo(ScreeningType.Mammogram));
    }

    [Test]
    public void RecommendedFrequencyMonths_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var screening = new Screening();

        // Act
        screening.RecommendedFrequencyMonths = 24;

        // Assert
        Assert.That(screening.RecommendedFrequencyMonths, Is.EqualTo(24));
    }

    [Test]
    public void Appointments_CanAddAppointments_AddsCorrectly()
    {
        // Arrange
        var screening = new Screening();
        var appointment = new Appointment { AppointmentId = Guid.NewGuid() };

        // Act
        screening.Appointments.Add(appointment);

        // Assert
        Assert.That(screening.Appointments, Has.Count.EqualTo(1));
        Assert.That(screening.Appointments, Contains.Item(appointment));
    }

    [Test]
    public void LastScreeningDate_CanBeSetAndCleared_UpdatesCorrectly()
    {
        // Arrange
        var screening = new Screening { LastScreeningDate = DateTime.UtcNow };

        // Act
        screening.LastScreeningDate = null;

        // Assert
        Assert.That(screening.LastScreeningDate, Is.Null);
    }

    [Test]
    public void NextDueDate_CanBeSetAndCleared_UpdatesCorrectly()
    {
        // Arrange
        var screening = new Screening { NextDueDate = DateTime.UtcNow.AddMonths(1) };

        // Act
        screening.NextDueDate = null;

        // Assert
        Assert.That(screening.NextDueDate, Is.Null);
    }
}
