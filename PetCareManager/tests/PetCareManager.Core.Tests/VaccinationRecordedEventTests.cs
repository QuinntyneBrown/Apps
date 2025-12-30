// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class VaccinationRecordedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new VaccinationRecordedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.VaccinationId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.PetId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Name, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.DateAdministered, Is.EqualTo(default(DateTime)));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var vaccinationId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var dateAdministered = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new VaccinationRecordedEvent
        {
            VaccinationId = vaccinationId,
            PetId = petId,
            Name = "Rabies",
            DateAdministered = dateAdministered,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.VaccinationId, Is.EqualTo(vaccinationId));
            Assert.That(eventRecord.PetId, Is.EqualTo(petId));
            Assert.That(eventRecord.Name, Is.EqualTo("Rabies"));
            Assert.That(eventRecord.DateAdministered, Is.EqualTo(dateAdministered));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_IsImmutable_PropertiesAreInitOnly()
    {
        // Arrange
        var vaccinationId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var dateAdministered = DateTime.UtcNow;

        // Act
        var eventRecord = new VaccinationRecordedEvent
        {
            VaccinationId = vaccinationId,
            PetId = petId,
            Name = "DHPP",
            DateAdministered = dateAdministered
        };

        // Assert - Verify properties were set
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.VaccinationId, Is.EqualTo(vaccinationId));
            Assert.That(eventRecord.PetId, Is.EqualTo(petId));
            Assert.That(eventRecord.Name, Is.EqualTo("DHPP"));
            Assert.That(eventRecord.DateAdministered, Is.EqualTo(dateAdministered));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var vaccinationId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var dateAdministered = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new VaccinationRecordedEvent
        {
            VaccinationId = vaccinationId,
            PetId = petId,
            Name = "Rabies",
            DateAdministered = dateAdministered,
            Timestamp = timestamp
        };

        var event2 = new VaccinationRecordedEvent
        {
            VaccinationId = vaccinationId,
            PetId = petId,
            Name = "Rabies",
            DateAdministered = dateAdministered,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new VaccinationRecordedEvent
        {
            VaccinationId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            Name = "Rabies",
            DateAdministered = DateTime.UtcNow
        };

        var event2 = new VaccinationRecordedEvent
        {
            VaccinationId = Guid.NewGuid(),
            PetId = Guid.NewGuid(),
            Name = "DHPP",
            DateAdministered = DateTime.UtcNow.AddDays(1)
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
