// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core.Tests;

public class DomainEventsTests
{
    [Test]
    public void DoseTakenEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var doseScheduleId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var medicationId = Guid.NewGuid();
        var takenAt = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new DoseTakenEvent
        {
            DoseScheduleId = doseScheduleId,
            UserId = userId,
            MedicationId = medicationId,
            TakenAt = takenAt,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DoseScheduleId, Is.EqualTo(doseScheduleId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.MedicationId, Is.EqualTo(medicationId));
            Assert.That(evt.TakenAt, Is.EqualTo(takenAt));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DoseTakenEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new DoseTakenEvent
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            TakenAt = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void MedicationAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var medicationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Aspirin";
        var medicationType = MedicationType.Tablet;
        var dosage = "500mg";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MedicationAddedEvent
        {
            MedicationId = medicationId,
            UserId = userId,
            Name = name,
            MedicationType = medicationType,
            Dosage = dosage,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MedicationId, Is.EqualTo(medicationId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.MedicationType, Is.EqualTo(medicationType));
            Assert.That(evt.Dosage, Is.EqualTo(dosage));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MedicationAddedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new MedicationAddedEvent
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Medicine",
            MedicationType = MedicationType.Tablet,
            Dosage = "100mg"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void MedicationAddedEvent_AllMedicationTypes_CanBeSet()
    {
        // Arrange
        var medicationId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act & Assert - Tablet
        var evt1 = new MedicationAddedEvent
        {
            MedicationId = medicationId,
            UserId = userId,
            Name = "Test",
            MedicationType = MedicationType.Tablet,
            Dosage = "100mg"
        };
        Assert.That(evt1.MedicationType, Is.EqualTo(MedicationType.Tablet));

        // Act & Assert - Liquid
        var evt2 = new MedicationAddedEvent
        {
            MedicationId = medicationId,
            UserId = userId,
            Name = "Test",
            MedicationType = MedicationType.Liquid,
            Dosage = "5ml"
        };
        Assert.That(evt2.MedicationType, Is.EqualTo(MedicationType.Liquid));

        // Act & Assert - Injection
        var evt3 = new MedicationAddedEvent
        {
            MedicationId = medicationId,
            UserId = userId,
            Name = "Test",
            MedicationType = MedicationType.Injection,
            Dosage = "10 units"
        };
        Assert.That(evt3.MedicationType, Is.EqualTo(MedicationType.Injection));
    }

    [Test]
    public void RefillRecordedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var refillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var medicationId = Guid.NewGuid();
        var refillDate = DateTime.UtcNow;
        var quantity = 30;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RefillRecordedEvent
        {
            RefillId = refillId,
            UserId = userId,
            MedicationId = medicationId,
            RefillDate = refillDate,
            Quantity = quantity,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RefillId, Is.EqualTo(refillId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.MedicationId, Is.EqualTo(medicationId));
            Assert.That(evt.RefillDate, Is.EqualTo(refillDate));
            Assert.That(evt.Quantity, Is.EqualTo(quantity));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void RefillRecordedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new RefillRecordedEvent
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void RefillRecordedEvent_DifferentQuantities_CanBeSet()
    {
        // Arrange
        var refillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var medicationId = Guid.NewGuid();
        var refillDate = DateTime.UtcNow;

        // Act & Assert - 30 quantity
        var evt1 = new RefillRecordedEvent
        {
            RefillId = refillId,
            UserId = userId,
            MedicationId = medicationId,
            RefillDate = refillDate,
            Quantity = 30
        };
        Assert.That(evt1.Quantity, Is.EqualTo(30));

        // Act & Assert - 90 quantity
        var evt2 = new RefillRecordedEvent
        {
            RefillId = refillId,
            UserId = userId,
            MedicationId = medicationId,
            RefillDate = refillDate,
            Quantity = 90
        };
        Assert.That(evt2.Quantity, Is.EqualTo(90));
    }
}
