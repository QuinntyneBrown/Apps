// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core.Tests;

public class DeliveryScheduleTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDeliverySchedule()
    {
        // Arrange
        var deliveryScheduleId = Guid.NewGuid();
        var letterId = Guid.NewGuid();
        var scheduledDateTime = DateTime.UtcNow.AddDays(7);
        var deliveryMethod = "Email";

        // Act
        var schedule = new DeliverySchedule
        {
            DeliveryScheduleId = deliveryScheduleId,
            LetterId = letterId,
            ScheduledDateTime = scheduledDateTime,
            DeliveryMethod = deliveryMethod
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.DeliveryScheduleId, Is.EqualTo(deliveryScheduleId));
            Assert.That(schedule.LetterId, Is.EqualTo(letterId));
            Assert.That(schedule.ScheduledDateTime, Is.EqualTo(scheduledDateTime));
            Assert.That(schedule.DeliveryMethod, Is.EqualTo(deliveryMethod));
            Assert.That(schedule.IsActive, Is.True);
            Assert.That(schedule.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Deactivate_WhenCalled_SetsIsActiveFalseAndUpdatesTimestamp()
    {
        // Arrange
        var schedule = new DeliverySchedule { IsActive = true };
        var beforeDeactivation = DateTime.UtcNow;

        // Act
        schedule.Deactivate();

        var afterDeactivation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.IsActive, Is.False);
            Assert.That(schedule.UpdatedAt, Is.Not.Null);
            Assert.That(schedule.UpdatedAt, Is.GreaterThanOrEqualTo(beforeDeactivation));
            Assert.That(schedule.UpdatedAt, Is.LessThanOrEqualTo(afterDeactivation));
        });
    }

    [Test]
    public void IsActive_DefaultsToTrue()
    {
        // Arrange & Act
        var schedule = new DeliverySchedule();

        // Assert
        Assert.That(schedule.IsActive, Is.True);
    }

    [Test]
    public void DeliveryScheduleId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new DeliverySchedule();
        var expectedId = Guid.NewGuid();

        // Act
        schedule.DeliveryScheduleId = expectedId;

        // Assert
        Assert.That(schedule.DeliveryScheduleId, Is.EqualTo(expectedId));
    }

    [Test]
    public void LetterId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new DeliverySchedule();
        var expectedLetterId = Guid.NewGuid();

        // Act
        schedule.LetterId = expectedLetterId;

        // Assert
        Assert.That(schedule.LetterId, Is.EqualTo(expectedLetterId));
    }

    [Test]
    public void ScheduledDateTime_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new DeliverySchedule();
        var expectedDateTime = DateTime.UtcNow.AddMonths(6);

        // Act
        schedule.ScheduledDateTime = expectedDateTime;

        // Assert
        Assert.That(schedule.ScheduledDateTime, Is.EqualTo(expectedDateTime));
    }

    [Test]
    public void DeliveryMethod_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new DeliverySchedule();
        var expectedMethod = "SMS";

        // Act
        schedule.DeliveryMethod = expectedMethod;

        // Assert
        Assert.That(schedule.DeliveryMethod, Is.EqualTo(expectedMethod));
    }

    [Test]
    public void RecipientContact_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new DeliverySchedule();
        var expectedContact = "user@example.com";

        // Act
        schedule.RecipientContact = expectedContact;

        // Assert
        Assert.That(schedule.RecipientContact, Is.EqualTo(expectedContact));
    }

    [Test]
    public void RecipientContact_CanBeNull()
    {
        // Arrange
        var schedule = new DeliverySchedule();

        // Act
        schedule.RecipientContact = null;

        // Assert
        Assert.That(schedule.RecipientContact, Is.Null);
    }

    [Test]
    public void Letter_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new DeliverySchedule();
        var letter = new Letter { LetterId = Guid.NewGuid() };

        // Act
        schedule.Letter = letter;

        // Assert
        Assert.That(schedule.Letter, Is.EqualTo(letter));
    }

    [Test]
    public void UpdatedAt_DefaultsToNull()
    {
        // Arrange & Act
        var schedule = new DeliverySchedule();

        // Assert
        Assert.That(schedule.UpdatedAt, Is.Null);
    }
}
