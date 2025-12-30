// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core.Tests;

public class LetterTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLetter()
    {
        // Arrange
        var letterId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var subject = "Future Reflections";
        var content = "Dear future me...";
        var scheduledDate = DateTime.UtcNow.AddYears(1);

        // Act
        var letter = new Letter
        {
            LetterId = letterId,
            UserId = userId,
            Subject = subject,
            Content = content,
            ScheduledDeliveryDate = scheduledDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(letter.LetterId, Is.EqualTo(letterId));
            Assert.That(letter.UserId, Is.EqualTo(userId));
            Assert.That(letter.Subject, Is.EqualTo(subject));
            Assert.That(letter.Content, Is.EqualTo(content));
            Assert.That(letter.ScheduledDeliveryDate, Is.EqualTo(scheduledDate));
            Assert.That(letter.DeliveryStatus, Is.EqualTo(DeliveryStatus.Pending));
            Assert.That(letter.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsDelivered_WhenCalled_SetsStatusAndDates()
    {
        // Arrange
        var letter = new Letter { DeliveryStatus = DeliveryStatus.Pending };
        var beforeDelivery = DateTime.UtcNow;

        // Act
        letter.MarkAsDelivered();

        var afterDelivery = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(letter.DeliveryStatus, Is.EqualTo(DeliveryStatus.Delivered));
            Assert.That(letter.ActualDeliveryDate, Is.Not.Null);
            Assert.That(letter.ActualDeliveryDate, Is.GreaterThanOrEqualTo(beforeDelivery));
            Assert.That(letter.ActualDeliveryDate, Is.LessThanOrEqualTo(afterDelivery));
            Assert.That(letter.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void MarkAsRead_WhenCalled_SetsReadFlagAndDate()
    {
        // Arrange
        var letter = new Letter { HasBeenRead = false };
        var beforeReading = DateTime.UtcNow;

        // Act
        letter.MarkAsRead();

        var afterReading = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(letter.HasBeenRead, Is.True);
            Assert.That(letter.ReadDate, Is.Not.Null);
            Assert.That(letter.ReadDate, Is.GreaterThanOrEqualTo(beforeReading));
            Assert.That(letter.ReadDate, Is.LessThanOrEqualTo(afterReading));
            Assert.That(letter.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void IsDueForDelivery_WhenPendingAndScheduledDatePassed_ReturnsTrue()
    {
        // Arrange
        var letter = new Letter
        {
            DeliveryStatus = DeliveryStatus.Pending,
            ScheduledDeliveryDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = letter.IsDueForDelivery();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsDueForDelivery_WhenPendingAndScheduledDateInFuture_ReturnsFalse()
    {
        // Arrange
        var letter = new Letter
        {
            DeliveryStatus = DeliveryStatus.Pending,
            ScheduledDeliveryDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = letter.IsDueForDelivery();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsDueForDelivery_WhenAlreadyDelivered_ReturnsFalse()
    {
        // Arrange
        var letter = new Letter
        {
            DeliveryStatus = DeliveryStatus.Delivered,
            ScheduledDeliveryDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = letter.IsDueForDelivery();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void HasBeenRead_DefaultsToFalse()
    {
        // Arrange & Act
        var letter = new Letter();

        // Assert
        Assert.That(letter.HasBeenRead, Is.False);
    }

    [Test]
    public void DeliveryStatus_DefaultsToPending()
    {
        // Arrange & Act
        var letter = new Letter();

        // Assert
        Assert.That(letter.DeliveryStatus, Is.EqualTo(DeliveryStatus.Pending));
    }

    [Test]
    public void DeliverySchedules_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var letter = new Letter();

        // Assert
        Assert.That(letter.DeliverySchedules, Is.Not.Null);
        Assert.That(letter.DeliverySchedules, Is.Empty);
    }

    [Test]
    public void DeliverySchedules_CanAddSchedules_ReturnsCorrectCount()
    {
        // Arrange
        var letter = new Letter();
        var schedule = new DeliverySchedule { DeliveryScheduleId = Guid.NewGuid() };

        // Act
        letter.DeliverySchedules.Add(schedule);

        // Assert
        Assert.That(letter.DeliverySchedules.Count, Is.EqualTo(1));
        Assert.That(letter.DeliverySchedules.First(), Is.EqualTo(schedule));
    }

    [Test]
    public void ActualDeliveryDate_CanBeNull()
    {
        // Arrange & Act
        var letter = new Letter();

        // Assert
        Assert.That(letter.ActualDeliveryDate, Is.Null);
    }

    [Test]
    public void ReadDate_CanBeNull()
    {
        // Arrange & Act
        var letter = new Letter();

        // Assert
        Assert.That(letter.ReadDate, Is.Null);
    }
}
