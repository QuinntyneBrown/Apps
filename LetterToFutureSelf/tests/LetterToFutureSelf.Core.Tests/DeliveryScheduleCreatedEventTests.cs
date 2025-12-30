// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core.Tests;

public class DeliveryScheduleCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var deliveryScheduleId = Guid.NewGuid();
        var letterId = Guid.NewGuid();
        var scheduledDateTime = DateTime.UtcNow.AddDays(30);
        var deliveryMethod = "Email";

        // Act
        var evt = new DeliveryScheduleCreatedEvent
        {
            DeliveryScheduleId = deliveryScheduleId,
            LetterId = letterId,
            ScheduledDateTime = scheduledDateTime,
            DeliveryMethod = deliveryMethod
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DeliveryScheduleId, Is.EqualTo(deliveryScheduleId));
            Assert.That(evt.LetterId, Is.EqualTo(letterId));
            Assert.That(evt.ScheduledDateTime, Is.EqualTo(scheduledDateTime));
            Assert.That(evt.DeliveryMethod, Is.EqualTo(deliveryMethod));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new DeliveryScheduleCreatedEvent
        {
            DeliveryScheduleId = Guid.NewGuid(),
            LetterId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void DeliveryScheduleId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new DeliveryScheduleCreatedEvent { DeliveryScheduleId = expectedId };

        // Assert
        Assert.That(evt.DeliveryScheduleId, Is.EqualTo(expectedId));
    }

    [Test]
    public void LetterId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedLetterId = Guid.NewGuid();

        // Act
        var evt = new DeliveryScheduleCreatedEvent { LetterId = expectedLetterId };

        // Assert
        Assert.That(evt.LetterId, Is.EqualTo(expectedLetterId));
    }

    [Test]
    public void ScheduledDateTime_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedDateTime = DateTime.UtcNow.AddMonths(3);

        // Act
        var evt = new DeliveryScheduleCreatedEvent { ScheduledDateTime = expectedDateTime };

        // Assert
        Assert.That(evt.ScheduledDateTime, Is.EqualTo(expectedDateTime));
    }

    [Test]
    public void DeliveryMethod_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedMethod = "In-App";

        // Act
        var evt = new DeliveryScheduleCreatedEvent { DeliveryMethod = expectedMethod };

        // Assert
        Assert.That(evt.DeliveryMethod, Is.EqualTo(expectedMethod));
    }
}
