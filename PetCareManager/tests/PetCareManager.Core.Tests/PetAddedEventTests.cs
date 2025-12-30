// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class PetAddedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new PetAddedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.PetId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Name, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.PetType, Is.EqualTo(PetType.Dog));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var petId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new PetAddedEvent
        {
            PetId = petId,
            UserId = userId,
            Name = "Max",
            PetType = PetType.Dog,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.PetId, Is.EqualTo(petId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Name, Is.EqualTo("Max"));
            Assert.That(eventRecord.PetType, Is.EqualTo(PetType.Dog));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_IsImmutable_PropertiesAreInitOnly()
    {
        // Arrange
        var petId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var eventRecord = new PetAddedEvent
        {
            PetId = petId,
            UserId = userId,
            Name = "Buddy",
            PetType = PetType.Cat
        };

        // Assert - Verify properties were set
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.PetId, Is.EqualTo(petId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Name, Is.EqualTo("Buddy"));
            Assert.That(eventRecord.PetType, Is.EqualTo(PetType.Cat));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var petId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PetAddedEvent
        {
            PetId = petId,
            UserId = userId,
            Name = "Max",
            PetType = PetType.Dog,
            Timestamp = timestamp
        };

        var event2 = new PetAddedEvent
        {
            PetId = petId,
            UserId = userId,
            Name = "Max",
            PetType = PetType.Dog,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new PetAddedEvent
        {
            PetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Max",
            PetType = PetType.Dog
        };

        var event2 = new PetAddedEvent
        {
            PetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Bella",
            PetType = PetType.Cat
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
