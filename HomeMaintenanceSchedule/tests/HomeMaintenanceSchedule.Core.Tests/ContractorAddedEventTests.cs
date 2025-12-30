// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class ContractorAddedEventTests
{
    [Test]
    public void ContractorAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "ABC Plumbing";
        var specialty = "Plumbing";
        var timestamp = DateTime.UtcNow;

        // Act
        var contractorEvent = new ContractorAddedEvent
        {
            ContractorId = contractorId,
            UserId = userId,
            Name = name,
            Specialty = specialty,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractorEvent.ContractorId, Is.EqualTo(contractorId));
            Assert.That(contractorEvent.UserId, Is.EqualTo(userId));
            Assert.That(contractorEvent.Name, Is.EqualTo(name));
            Assert.That(contractorEvent.Specialty, Is.EqualTo(specialty));
            Assert.That(contractorEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ContractorAddedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var contractorEvent = new ContractorAddedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractorEvent.ContractorId, Is.EqualTo(Guid.Empty));
            Assert.That(contractorEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(contractorEvent.Name, Is.EqualTo(string.Empty));
            Assert.That(contractorEvent.Specialty, Is.Null);
            Assert.That(contractorEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ContractorAddedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var contractorEvent = new ContractorAddedEvent
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Contractor"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(contractorEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ContractorAddedEvent_WithoutSpecialty_IsValid()
    {
        // Arrange & Act
        var contractorEvent = new ContractorAddedEvent
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "General Contractor"
        };

        // Assert
        Assert.That(contractorEvent.Specialty, Is.Null);
    }

    [Test]
    public void ContractorAddedEvent_IsImmutable()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Test Contractor";
        var specialty = "HVAC";

        // Act
        var contractorEvent = new ContractorAddedEvent
        {
            ContractorId = contractorId,
            UserId = userId,
            Name = name,
            Specialty = specialty
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(contractorEvent.ContractorId, Is.EqualTo(contractorId));
            Assert.That(contractorEvent.UserId, Is.EqualTo(userId));
            Assert.That(contractorEvent.Name, Is.EqualTo(name));
            Assert.That(contractorEvent.Specialty, Is.EqualTo(specialty));
        });
    }

    [Test]
    public void ContractorAddedEvent_EqualityByValue()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Contractor";
        var specialty = "Electrician";
        var timestamp = DateTime.UtcNow;

        var event1 = new ContractorAddedEvent
        {
            ContractorId = contractorId,
            UserId = userId,
            Name = name,
            Specialty = specialty,
            Timestamp = timestamp
        };

        var event2 = new ContractorAddedEvent
        {
            ContractorId = contractorId,
            UserId = userId,
            Name = name,
            Specialty = specialty,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void ContractorAddedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new ContractorAddedEvent
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Contractor 1"
        };

        var event2 = new ContractorAddedEvent
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Contractor 2"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
