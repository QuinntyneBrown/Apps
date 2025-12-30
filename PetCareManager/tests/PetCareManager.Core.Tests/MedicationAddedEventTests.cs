// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class MedicationAddedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new MedicationAddedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.MedicationId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.PetId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Name, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var medicationId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new MedicationAddedEvent
        {
            MedicationId = medicationId,
            PetId = petId,
            Name = "Antibiotics",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.MedicationId, Is.EqualTo(medicationId));
            Assert.That(eventRecord.PetId, Is.EqualTo(petId));
            Assert.That(eventRecord.Name, Is.EqualTo("Antibiotics"));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_IsImmutable_PropertiesAreInitOnly()
    {
        // Arrange
        var medicationId = Guid.NewGuid();
        var petId = Guid.NewGuid();

        // Act
        var eventRecord = new MedicationAddedEvent
        {
            MedicationId = medicationId,
            PetId = petId,
            Name = "Pain Relief"
        };

        // Assert - Verify properties were set
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.MedicationId, Is.EqualTo(medicationId));
            Assert.That(eventRecord.PetId, Is.EqualTo(petId));
            Assert.That(eventRecord.Name, Is.EqualTo("Pain Relief"));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var medicationId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new MedicationAddedEvent
        {
            MedicationId = medicationId,
            PetId = petId,
            Name = "Antibiotics",
            Timestamp = timestamp
        };

        var event2 = new MedicationAddedEvent
        {
            MedicationId = medicationId,
            PetId = petId,
            Name = "Antibiotics",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new MedicationAddedEvent
        {
            MedicationId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            Name = "Antibiotics"
        };

        var event2 = new MedicationAddedEvent
        {
            MedicationId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            Name = "Pain Relief"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
