// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core.Tests;

public class ReadingStartedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var readingProgressId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var startDate = new DateTime(2024, 6, 15, 10, 0, 0);

        // Act
        var eventData = new ReadingStartedEvent
        {
            ReadingProgressId = readingProgressId,
            ResourceId = resourceId,
            StartDate = startDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ReadingProgressId, Is.EqualTo(readingProgressId));
            Assert.That(eventData.ResourceId, Is.EqualTo(resourceId));
            Assert.That(eventData.StartDate, Is.EqualTo(startDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new ReadingStartedEvent
        {
            ReadingProgressId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow
        };

        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var readingProgressId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var startDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new ReadingStartedEvent
        {
            ReadingProgressId = readingProgressId,
            ResourceId = resourceId,
            StartDate = startDate,
            Timestamp = timestamp
        };

        var event2 = new ReadingStartedEvent
        {
            ReadingProgressId = readingProgressId,
            ResourceId = resourceId,
            StartDate = startDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new ReadingStartedEvent
        {
            ReadingProgressId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow
        };

        var event2 = new ReadingStartedEvent
        {
            ReadingProgressId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
