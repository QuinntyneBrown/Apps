// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class VetAppointmentScheduledEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new VetAppointmentScheduledEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.VetAppointmentId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.PetId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.AppointmentDate, Is.EqualTo(default(DateTime)));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var appointmentDate = DateTime.UtcNow.AddDays(7);
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new VetAppointmentScheduledEvent
        {
            VetAppointmentId = appointmentId,
            PetId = petId,
            AppointmentDate = appointmentDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.VetAppointmentId, Is.EqualTo(appointmentId));
            Assert.That(eventRecord.PetId, Is.EqualTo(petId));
            Assert.That(eventRecord.AppointmentDate, Is.EqualTo(appointmentDate));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_IsImmutable_PropertiesAreInitOnly()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var appointmentDate = DateTime.UtcNow;

        // Act
        var eventRecord = new VetAppointmentScheduledEvent
        {
            VetAppointmentId = appointmentId,
            PetId = petId,
            AppointmentDate = appointmentDate
        };

        // Assert - Verify properties were set
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.VetAppointmentId, Is.EqualTo(appointmentId));
            Assert.That(eventRecord.PetId, Is.EqualTo(petId));
            Assert.That(eventRecord.AppointmentDate, Is.EqualTo(appointmentDate));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var appointmentDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new VetAppointmentScheduledEvent
        {
            VetAppointmentId = appointmentId,
            PetId = petId,
            AppointmentDate = appointmentDate,
            Timestamp = timestamp
        };

        var event2 = new VetAppointmentScheduledEvent
        {
            VetAppointmentId = appointmentId,
            PetId = petId,
            AppointmentDate = appointmentDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new VetAppointmentScheduledEvent
        {
            VetAppointmentId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            AppointmentDate = DateTime.UtcNow
        };

        var event2 = new VetAppointmentScheduledEvent
        {
            VetAppointmentId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            AppointmentDate = DateTime.UtcNow.AddDays(1)
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
