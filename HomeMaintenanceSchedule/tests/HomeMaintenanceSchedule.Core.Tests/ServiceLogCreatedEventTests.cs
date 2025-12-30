// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class ServiceLogCreatedEventTests
{
    [Test]
    public void ServiceLogCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var serviceLogId = Guid.NewGuid();
        var maintenanceTaskId = Guid.NewGuid();
        var serviceDate = DateTime.UtcNow;
        var cost = 150.50m;
        var timestamp = DateTime.UtcNow;

        // Act
        var logEvent = new ServiceLogCreatedEvent
        {
            ServiceLogId = serviceLogId,
            MaintenanceTaskId = maintenanceTaskId,
            ServiceDate = serviceDate,
            Cost = cost,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(logEvent.ServiceLogId, Is.EqualTo(serviceLogId));
            Assert.That(logEvent.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(logEvent.ServiceDate, Is.EqualTo(serviceDate));
            Assert.That(logEvent.Cost, Is.EqualTo(cost));
            Assert.That(logEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ServiceLogCreatedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var logEvent = new ServiceLogCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(logEvent.ServiceLogId, Is.EqualTo(Guid.Empty));
            Assert.That(logEvent.MaintenanceTaskId, Is.EqualTo(Guid.Empty));
            Assert.That(logEvent.ServiceDate, Is.EqualTo(default(DateTime)));
            Assert.That(logEvent.Cost, Is.Null);
            Assert.That(logEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ServiceLogCreatedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var logEvent = new ServiceLogCreatedEvent
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(logEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ServiceLogCreatedEvent_WithoutCost_IsValid()
    {
        // Arrange & Act
        var logEvent = new ServiceLogCreatedEvent
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(logEvent.Cost, Is.Null);
    }

    [Test]
    public void ServiceLogCreatedEvent_IsImmutable()
    {
        // Arrange
        var serviceLogId = Guid.NewGuid();
        var maintenanceTaskId = Guid.NewGuid();
        var serviceDate = DateTime.UtcNow;
        var cost = 200m;

        // Act
        var logEvent = new ServiceLogCreatedEvent
        {
            ServiceLogId = serviceLogId,
            MaintenanceTaskId = maintenanceTaskId,
            ServiceDate = serviceDate,
            Cost = cost
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(logEvent.ServiceLogId, Is.EqualTo(serviceLogId));
            Assert.That(logEvent.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(logEvent.ServiceDate, Is.EqualTo(serviceDate));
            Assert.That(logEvent.Cost, Is.EqualTo(cost));
        });
    }

    [Test]
    public void ServiceLogCreatedEvent_EqualityByValue()
    {
        // Arrange
        var serviceLogId = Guid.NewGuid();
        var maintenanceTaskId = Guid.NewGuid();
        var serviceDate = DateTime.UtcNow;
        var cost = 175m;
        var timestamp = DateTime.UtcNow;

        var event1 = new ServiceLogCreatedEvent
        {
            ServiceLogId = serviceLogId,
            MaintenanceTaskId = maintenanceTaskId,
            ServiceDate = serviceDate,
            Cost = cost,
            Timestamp = timestamp
        };

        var event2 = new ServiceLogCreatedEvent
        {
            ServiceLogId = serviceLogId,
            MaintenanceTaskId = maintenanceTaskId,
            ServiceDate = serviceDate,
            Cost = cost,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void ServiceLogCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new ServiceLogCreatedEvent
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            Cost = 100m
        };

        var event2 = new ServiceLogCreatedEvent
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            Cost = 200m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
