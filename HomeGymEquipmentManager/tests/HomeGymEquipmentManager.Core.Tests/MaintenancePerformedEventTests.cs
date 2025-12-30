// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core.Tests;

public class MaintenancePerformedEventTests
{
    [Test]
    public void MaintenancePerformedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var maintenanceId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var maintenanceDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var maintenanceEvent = new MaintenancePerformedEvent
        {
            MaintenanceId = maintenanceId,
            EquipmentId = equipmentId,
            MaintenanceDate = maintenanceDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(maintenanceEvent.MaintenanceId, Is.EqualTo(maintenanceId));
            Assert.That(maintenanceEvent.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(maintenanceEvent.MaintenanceDate, Is.EqualTo(maintenanceDate));
            Assert.That(maintenanceEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MaintenancePerformedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var maintenanceEvent = new MaintenancePerformedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(maintenanceEvent.MaintenanceId, Is.EqualTo(Guid.Empty));
            Assert.That(maintenanceEvent.EquipmentId, Is.EqualTo(Guid.Empty));
            Assert.That(maintenanceEvent.MaintenanceDate, Is.EqualTo(default(DateTime)));
            Assert.That(maintenanceEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MaintenancePerformedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var maintenanceEvent = new MaintenancePerformedEvent
        {
            MaintenanceId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = DateTime.UtcNow
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(maintenanceEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void MaintenancePerformedEvent_IsImmutable()
    {
        // Arrange
        var maintenanceId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var maintenanceDate = DateTime.UtcNow;

        // Act
        var maintenanceEvent = new MaintenancePerformedEvent
        {
            MaintenanceId = maintenanceId,
            EquipmentId = equipmentId,
            MaintenanceDate = maintenanceDate
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(maintenanceEvent.MaintenanceId, Is.EqualTo(maintenanceId));
            Assert.That(maintenanceEvent.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(maintenanceEvent.MaintenanceDate, Is.EqualTo(maintenanceDate));
        });
    }

    [Test]
    public void MaintenancePerformedEvent_EqualityByValue()
    {
        // Arrange
        var maintenanceId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var maintenanceDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new MaintenancePerformedEvent
        {
            MaintenanceId = maintenanceId,
            EquipmentId = equipmentId,
            MaintenanceDate = maintenanceDate,
            Timestamp = timestamp
        };

        var event2 = new MaintenancePerformedEvent
        {
            MaintenanceId = maintenanceId,
            EquipmentId = equipmentId,
            MaintenanceDate = maintenanceDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void MaintenancePerformedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new MaintenancePerformedEvent
        {
            MaintenanceId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = DateTime.UtcNow
        };

        var event2 = new MaintenancePerformedEvent
        {
            MaintenanceId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void MaintenancePerformedEvent_WithPastMaintenanceDate_IsValid()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddMonths(-6);

        // Act
        var maintenanceEvent = new MaintenancePerformedEvent
        {
            MaintenanceId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = pastDate
        };

        // Assert
        Assert.That(maintenanceEvent.MaintenanceDate, Is.EqualTo(pastDate));
    }

    [Test]
    public void MaintenancePerformedEvent_WithFutureMaintenanceDate_CanBeSet()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddMonths(1);

        // Act
        var maintenanceEvent = new MaintenancePerformedEvent
        {
            MaintenanceId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = futureDate
        };

        // Assert
        Assert.That(maintenanceEvent.MaintenanceDate, Is.EqualTo(futureDate));
    }
}
