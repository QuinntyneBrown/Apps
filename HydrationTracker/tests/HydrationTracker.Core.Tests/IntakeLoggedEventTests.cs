// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core.Tests;

public class IntakeLoggedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesIntakeLoggedEvent()
    {
        // Arrange
        var intakeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var beverageType = BeverageType.Water;
        var amountMl = 500m;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new IntakeLoggedEvent
        {
            IntakeId = intakeId,
            UserId = userId,
            BeverageType = beverageType,
            AmountMl = amountMl,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.IntakeId, Is.EqualTo(intakeId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BeverageType, Is.EqualTo(beverageType));
            Assert.That(evt.AmountMl, Is.EqualTo(amountMl));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new IntakeLoggedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.IntakeId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.BeverageType, Is.EqualTo(BeverageType.Water));
            Assert.That(evt.AmountMl, Is.EqualTo(0m));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var intakeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var beverageType = BeverageType.Tea;
        var amountMl = 300m;
        var timestamp = new DateTime(2025, 1, 1);

        var evt1 = new IntakeLoggedEvent
        {
            IntakeId = intakeId,
            UserId = userId,
            BeverageType = beverageType,
            AmountMl = amountMl,
            Timestamp = timestamp
        };

        var evt2 = new IntakeLoggedEvent
        {
            IntakeId = intakeId,
            UserId = userId,
            BeverageType = beverageType,
            AmountMl = amountMl,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var evt1 = new IntakeLoggedEvent
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Water,
            AmountMl = 500m,
            Timestamp = DateTime.UtcNow
        };

        var evt2 = new IntakeLoggedEvent
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Coffee,
            AmountMl = 250m,
            Timestamp = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new IntakeLoggedEvent
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Water,
            AmountMl = 500m,
            Timestamp = DateTime.UtcNow
        };

        // Act
        var modified = original with { AmountMl = 750m };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.IntakeId, Is.EqualTo(original.IntakeId));
            Assert.That(modified.UserId, Is.EqualTo(original.UserId));
            Assert.That(modified.BeverageType, Is.EqualTo(original.BeverageType));
            Assert.That(modified.AmountMl, Is.EqualTo(750m));
            Assert.That(modified.Timestamp, Is.EqualTo(original.Timestamp));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }

    [Test]
    public void BeverageType_AllTypes_CanBeSet()
    {
        // Arrange & Act & Assert
        Assert.DoesNotThrow(() => new IntakeLoggedEvent { BeverageType = BeverageType.Water });
        Assert.DoesNotThrow(() => new IntakeLoggedEvent { BeverageType = BeverageType.Tea });
        Assert.DoesNotThrow(() => new IntakeLoggedEvent { BeverageType = BeverageType.Coffee });
        Assert.DoesNotThrow(() => new IntakeLoggedEvent { BeverageType = BeverageType.Juice });
        Assert.DoesNotThrow(() => new IntakeLoggedEvent { BeverageType = BeverageType.Sports });
        Assert.DoesNotThrow(() => new IntakeLoggedEvent { BeverageType = BeverageType.Other });
    }

    [Test]
    public void AmountMl_DecimalValue_IsPreserved()
    {
        // Arrange
        var amount = 456.78m;
        var evt = new IntakeLoggedEvent { AmountMl = amount };

        // Assert
        Assert.That(evt.AmountMl, Is.EqualTo(amount));
    }

    [Test]
    public void IntakeId_CanBeSetAndRetrieved()
    {
        // Arrange
        var intakeId = Guid.NewGuid();
        var evt = new IntakeLoggedEvent { IntakeId = intakeId };

        // Assert
        Assert.That(evt.IntakeId, Is.EqualTo(intakeId));
    }

    [Test]
    public void UserId_CanBeSetAndRetrieved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var evt = new IntakeLoggedEvent { UserId = userId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void Timestamp_CanBeSetToSpecificTime()
    {
        // Arrange
        var specificTime = new DateTime(2025, 6, 15, 14, 30, 0);
        var evt = new IntakeLoggedEvent { Timestamp = specificTime };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(specificTime));
    }
}
