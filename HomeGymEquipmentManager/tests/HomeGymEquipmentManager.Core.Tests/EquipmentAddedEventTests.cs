// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core.Tests;

public class EquipmentAddedEventTests
{
    [Test]
    public void EquipmentAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Treadmill";
        var equipmentType = EquipmentType.Cardio;
        var timestamp = DateTime.UtcNow;

        // Act
        var equipmentEvent = new EquipmentAddedEvent
        {
            EquipmentId = equipmentId,
            UserId = userId,
            Name = name,
            EquipmentType = equipmentType,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(equipmentEvent.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(equipmentEvent.UserId, Is.EqualTo(userId));
            Assert.That(equipmentEvent.Name, Is.EqualTo(name));
            Assert.That(equipmentEvent.EquipmentType, Is.EqualTo(equipmentType));
            Assert.That(equipmentEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void EquipmentAddedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var equipmentEvent = new EquipmentAddedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(equipmentEvent.EquipmentId, Is.EqualTo(Guid.Empty));
            Assert.That(equipmentEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(equipmentEvent.Name, Is.EqualTo(string.Empty));
            Assert.That(equipmentEvent.EquipmentType, Is.EqualTo(EquipmentType.Cardio));
            Assert.That(equipmentEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void EquipmentAddedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var equipmentEvent = new EquipmentAddedEvent
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Equipment",
            EquipmentType = EquipmentType.Strength
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(equipmentEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void EquipmentAddedEvent_AllEquipmentTypes_CanBeSet()
    {
        // Arrange
        var equipmentTypes = new[]
        {
            EquipmentType.Cardio,
            EquipmentType.Strength,
            EquipmentType.Flexibility,
            EquipmentType.Accessory
        };

        // Act & Assert
        foreach (var type in equipmentTypes)
        {
            var equipmentEvent = new EquipmentAddedEvent
            {
                EquipmentId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Equipment",
                EquipmentType = type
            };

            Assert.That(equipmentEvent.EquipmentType, Is.EqualTo(type));
        }
    }

    [Test]
    public void EquipmentAddedEvent_IsImmutable()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Bike";
        var equipmentType = EquipmentType.Cardio;

        // Act
        var equipmentEvent = new EquipmentAddedEvent
        {
            EquipmentId = equipmentId,
            UserId = userId,
            Name = name,
            EquipmentType = equipmentType
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(equipmentEvent.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(equipmentEvent.UserId, Is.EqualTo(userId));
            Assert.That(equipmentEvent.Name, Is.EqualTo(name));
            Assert.That(equipmentEvent.EquipmentType, Is.EqualTo(equipmentType));
        });
    }

    [Test]
    public void EquipmentAddedEvent_EqualityByValue()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Equipment";
        var equipmentType = EquipmentType.Strength;
        var timestamp = DateTime.UtcNow;

        var event1 = new EquipmentAddedEvent
        {
            EquipmentId = equipmentId,
            UserId = userId,
            Name = name,
            EquipmentType = equipmentType,
            Timestamp = timestamp
        };

        var event2 = new EquipmentAddedEvent
        {
            EquipmentId = equipmentId,
            UserId = userId,
            Name = name,
            EquipmentType = equipmentType,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void EquipmentAddedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new EquipmentAddedEvent
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Equipment 1",
            EquipmentType = EquipmentType.Cardio
        };

        var event2 = new EquipmentAddedEvent
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Equipment 2",
            EquipmentType = EquipmentType.Strength
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void EquipmentAddedEvent_WithEmptyName_IsValid()
    {
        // Arrange & Act
        var equipmentEvent = new EquipmentAddedEvent
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "",
            EquipmentType = EquipmentType.Accessory
        };

        // Assert
        Assert.That(equipmentEvent.Name, Is.EqualTo(string.Empty));
    }
}
